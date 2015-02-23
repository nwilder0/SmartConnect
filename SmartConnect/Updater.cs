using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Timers;
using System.Threading;
using System.Windows.Forms;

namespace SmartConnect
{
    class Updater
    {
        int timeout,updateInterval;
        String serverIP;
        String filenameTemplate;
        WebClient checkForConfigUpdates, checkForSSIDUpdates, checkForAPUpdates, checkForLinkUpdates;
        ConcurrentDictionary<String, String> dConfig;
        ConcurrentDictionary<String, SSID> dSSID;
        ConcurrentDictionary<String, AP> dAP;
        List<SCLink> lLink;
        Uri urlConfigUpdate, urlSSIDUpdate, urlAPUpdate, urlLinkUpdate;
        WiFiConnect main;

        public Updater(int updateInterval, int timeout, String serverIP, String filenameTemplate, WiFiConnect main)
        {
            this.updateInterval = updateInterval;
            this.timeout = timeout;
            this.serverIP = serverIP;
            this.filenameTemplate = filenameTemplate;
            dConfig = new ConcurrentDictionary<string, string>();

            LoadConfigTemplate();

            checkForConfigUpdates = new WebClient();
            checkForConfigUpdates.DownloadStringCompleted += CheckForConfigUpdates_DownloadStringCompleted;
            checkForSSIDUpdates = new WebClient();
            checkForSSIDUpdates.DownloadStringCompleted += CheckForSSIDUpdates_DownloadStringCompleted;
            checkForAPUpdates = new WebClient();
            checkForAPUpdates.DownloadStringCompleted += CheckForAPUpdates_DownloadStringCompleted;
            checkForLinkUpdates = new WebClient();
            checkForLinkUpdates.DownloadStringCompleted += CheckForLinkUpdates_DownloadStringCompleted;
            String strURL = "http://" + serverIP + "/update.php?file=";
            urlConfigUpdate = new Uri(strURL+"config");
            urlAPUpdate = new Uri(strURL + "ap");
            urlSSIDUpdate = new Uri(strURL + "ssid");
            urlLinkUpdate = new Uri(strURL + "link");
            
        }

        void CheckForLinkUpdates_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                Exception ex = e.Error;
                if (ex is WebException)
                {
                    main.Log.error("Link Update: Unable to reach server for updates - " + ex.Message);
                }
                else
                {
                    String jsonResult = e.Result;
                    List<SCLink> lResult = JsonConvert.DeserializeObject<List<SCLink>>(jsonResult);
                    string jsonString = JsonConvert.SerializeObject(lResult, Formatting.Indented);
                    System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + main.Setting("filenameLinks"), jsonString);
                    main.Load("link");
                }
                
            }
            catch (Exception ex)
            {
                if (ex is WebException || ex is System.Reflection.TargetInvocationException)
                {
                    main.Log.error("Link Update: Unable to reach server for updates - " + ex.InnerException.Message);
                }
                else
                {
                    main.Log.error("Link Update: non-connection error - " + ex.Message);
                }
            }
        }

        void CheckForAPUpdates_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                Exception ex = e.Error;
                if (ex is WebException)
                {
                    main.Log.error("AP Update: Unable to reach server for updates - " + ex.Message);
                }
                else
                {
                    String jsonResult = e.Result;
                    Dictionary<String, AP> dResult = JsonConvert.DeserializeObject<Dictionary<String, AP>>(jsonResult);
                    string jsonString = JsonConvert.SerializeObject(dConfig, Formatting.Indented);
                    System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + main.Setting("filenameAPs"), jsonString);
                    main.Load("ap");
                }
            }
            catch (Exception ex)
            {
                if (ex is WebException || ex is System.Reflection.TargetInvocationException)
                {
                    main.Log.error("AP Update: Unable to reach server for updates - " + ex.InnerException.Message);
                }
                else
                {
                    main.Log.error("AP Update: non-connection error - " + ex.Message);
                }
            }
        }
        void CheckForSSIDUpdates_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                Exception ex = e.Error;
                if (ex is WebException)
                {
                    main.Log.error("SSID Update: Unable to reach server for updates - " + ex.Message);
                }
                else
                {
                    String jsonResult = e.Result;
                    Dictionary<String, SSID> dResult = JsonConvert.DeserializeObject<Dictionary<String, SSID>>(jsonResult);
                    string jsonString = JsonConvert.SerializeObject(dConfig, Formatting.Indented);
                    System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + main.Setting("filenameSSIDs"), jsonString);
                    main.Load("ssid");
                }
            }
            catch (Exception ex)
            {
                if (ex is WebException || ex is System.Reflection.TargetInvocationException)
                {
                    main.Log.error("SSID Update: Unable to reach server for updates - " + ex.InnerException.Message);
                }
                else
                {
                    main.Log.error("SSID Update: Unable to reach server for updates - " + ex.Message);
                }
            }
        }

        void CheckForConfigUpdates_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try {
                Exception ex = e.Error;
                if (ex is WebException)
                {
                    main.Log.error("Unable to reach server for updates - canceling further updates \n" + ex.Message);
                    //updateInterval = 0;
                }
                else
                {
                    lock (dConfig)
                    {
                        String jsonResult = e.Result;

                        Dictionary<String, String> dResult = JsonConvert.DeserializeObject<Dictionary<String, String>>(jsonResult);

                        Boolean preEmpt = false;

                        if (dResult.ContainsKey("version"))
                        {
                            if (Convert.ToInt32(dResult["version"]) > Convert.ToInt32(dConfig["version"])) preEmpt = true;
                        }
                        else
                        {
                            dResult["version"] = "0";
                        }

                        dConfig = new ConcurrentDictionary<string,string>(dResult);

                        Save();

                        if (preEmpt)
                        {
                            frmUpdate mbUpdate = new frmUpdate();
                            mbUpdate.ShowDialog();
                        }

                        
                    }
                }
            } catch (Exception ex) {
                if (ex is WebException || ex is System.Reflection.TargetInvocationException)
                {
                    main.Log.error(ex.InnerException.Message);

                }
                else
                {
                    main.Log.error(ex.Message);
                }
            }
        }

        private void LoadConfigTemplate()
        {
            dConfig.Clear();
            String jsonConfig = "";

            // load the json config template
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + filenameTemplate))
            {
                jsonConfig = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + filenameTemplate);

                dConfig = JsonConvert.DeserializeObject<ConcurrentDictionary<String, String>>(jsonConfig);
                if (!dConfig.ContainsKey("version")) dConfig["version"] = "0";
                ValidateConfig();
            }
            else
            {
                dConfig["version"] = "0";
            }
        }

        private void ValidateConfig()
        {
            if (dConfig.ContainsKey("version"))
            {
                // make sure it's an integer
            }
            else
            {
                dConfig["version"] = "0";
            }

        }

        public void Run()
        {
            if (updateInterval == 0)
            {
                Update();
            }
            else
            {
                for (; ; )
                {
                    if (updateInterval == 0)
                    {
                        break;
                    }
                    Update();
                    Thread.Sleep(updateInterval * 60 * 1000);
                }
            }
        }

        public void Update()
        {
            if (!checkForConfigUpdates.IsBusy) checkForConfigUpdates.DownloadStringAsync(urlConfigUpdate);
            if (!checkForSSIDUpdates.IsBusy) checkForSSIDUpdates.DownloadStringAsync(urlSSIDUpdate);
            if (!checkForAPUpdates.IsBusy) checkForAPUpdates.DownloadStringAsync(urlAPUpdate);
            if (!checkForLinkUpdates.IsBusy) checkForLinkUpdates.DownloadStringAsync(urlLinkUpdate);
        }

        public void Save()
        {
            string jsonConfig = JsonConvert.SerializeObject(dConfig, Formatting.Indented);

            System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + filenameTemplate, jsonConfig);
        }

    }
}
