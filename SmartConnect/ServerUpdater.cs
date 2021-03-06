﻿using System;
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
    class ServerUpdater
    {
        int timeout,updateInterval;
        String serverIP;
        String filenameTemplate;
        String baseUpdateURL;
        String lastCertFile;
        WebClient checkForConfigUpdates, checkForSSIDUpdates, checkForAPUpdates, checkForLinkUpdates, checkForCertUpdates;
        ConcurrentDictionary<String, String> dConfig;
        ConcurrentDictionary<String, SSID> dSSID;
        ConcurrentDictionary<String, AP> dAP;
        List<SCLink> lLink;
        Uri urlConfigUpdate, urlSSIDUpdate, urlAPUpdate, urlLinkUpdate, urlCertUpdate;
        WiFiConnect main;

        public ServerUpdater(int updateInterval, int timeout, String serverIP, String filenameTemplate, WiFiConnect main)
        {
            this.main = main;
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
            checkForCertUpdates = new WebClient();
            checkForCertUpdates.DownloadDataCompleted += CheckForCertUpdates_DownloadDataCompleted;
            baseUpdateURL = "http://" + serverIP + "/update.php?file=";
            urlConfigUpdate = new Uri(baseUpdateURL + "config");
            urlAPUpdate = new Uri(baseUpdateURL + "ap");
            urlSSIDUpdate = new Uri(baseUpdateURL + "ssid");
            urlLinkUpdate = new Uri(baseUpdateURL + "link");
            
        }

        void CheckForCertUpdates_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            try
            {
                Exception ex = e.Error;
                if (ex is WebException)
                {
                    main.Log.Error("Cert Update: Unable to reach server for updates - " + ex.Message);
                }
                else
                {
                    byte[] bytesCert = e.Result;
                    if (bytesCert.Length > 0)
                    {
                        File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + lastCertFile, bytesCert);
                        main.SetCertFile(lastCertFile);
                        main.Load("cert");
                    }
                }

            }
            catch (Exception ex)
            {
                if (ex is WebException || ex is System.Reflection.TargetInvocationException)
                {
                    main.Log.Error("Link Update: Unable to reach server for updates - " + ex.InnerException.Message);
                }
                else
                {
                    main.Log.Error("Link Update: non-connection error - " + ex.Message);
                }
            }

        }

        void CheckForLinkUpdates_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                Exception ex = e.Error;
                if (ex is WebException)
                {
                    main.Log.Error("Link Update: Unable to reach server for updates - " + ex.Message);
                }
                else
                {
                    String jsonResult = e.Result;
                    if (!jsonResult.Trim().Equals(""))
                    {
                        List<SCLink> lResult = JsonConvert.DeserializeObject<List<SCLink>>(jsonResult);
                        string jsonString = JsonConvert.SerializeObject(lResult, Formatting.Indented);
                        File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + main.Setting("filenameLinks"), jsonString);
                        main.Load("link");
                    }
                }
                
            }
            catch (Exception ex)
            {
                if (ex is WebException || ex is System.Reflection.TargetInvocationException)
                {
                    main.Log.Error("Link Update: Unable to reach server for updates - " + ex.InnerException.Message);
                }
                else
                {
                    main.Log.Error("Link Update: non-connection error - " + ex.Message);
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
                    main.Log.Error("AP Update: Unable to reach server for updates - " + ex.Message);
                }
                else
                {
                    String jsonResult = e.Result;
                    if (!jsonResult.Trim().Equals(""))
                    {
                        Dictionary<String, AP> dResult = JsonConvert.DeserializeObject<Dictionary<String, AP>>(jsonResult);
                        string jsonString = JsonConvert.SerializeObject(dResult, Formatting.Indented);
                        File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + main.Setting("filenameAPs"), jsonString);
                        main.Load("ap");
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex is WebException || ex is System.Reflection.TargetInvocationException)
                {
                    main.Log.Error("AP Update: Unable to reach server for updates - " + ex.InnerException.Message);
                }
                else
                {
                    main.Log.Error("AP Update: non-connection error - " + ex.Message);
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
                    if (main != null) main.Log.Error("SSID Update: Unable to reach server for updates - " + ex.Message);
                }
                else
                {
                    String jsonResult = e.Result;
                    if (!jsonResult.Trim().Equals(""))
                    {
                        Dictionary<String, SSID> dResult = JsonConvert.DeserializeObject<Dictionary<String, SSID>>(jsonResult);
                        string jsonString = JsonConvert.SerializeObject(dResult, Formatting.Indented);
                        File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + main.Setting("filenameSSIDs"), jsonString);
                        main.Load("ssid");
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex is WebException || ex is System.Reflection.TargetInvocationException)
                {
                    if(main!=null) main.Log.Error("SSID Update: Unable to reach server for updates - " + ex.InnerException.Message);
                }
                else
                {
                    if(main!=null) main.Log.Error("SSID Update: Unable to reach server for updates - " + ex.Message);
                }
            }
        }

        void CheckForConfigUpdates_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try {
                Exception ex = e.Error;
                if (ex is WebException)
                {
                    main.Log.Error("Unable to reach server for updates - canceling further updates \n" + ex.Message);
                    //updateInterval = 0;
                }
                else
                {
                    lock (dConfig)
                    {
                        String jsonResult = e.Result;
                        if (!jsonResult.Trim().Equals(""))
                        {
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

                            dConfig = new ConcurrentDictionary<string, string>(dResult);

                            String strCertFile = "";

                            Save();

                            if (dConfig.TryGetValue("filenameServerCert",out strCertFile))
                            {
                                if (strCertFile != "")
                                {
                                    if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + strCertFile))
                                    {
                                        urlCertUpdate = new Uri(baseUpdateURL + "cert&name=" + strCertFile);
                                        if (!checkForCertUpdates.IsBusy) checkForCertUpdates.DownloadStringAsync(urlCertUpdate);
                                    }
                                    else
                                    {
                                        main.Log.Debug("Config update: certificate file exists locally, skipping download");
                                    }
                                }
                            }


                            if (preEmpt)
                            {
                                frmUpdate mbUpdate = new frmUpdate();
                                mbUpdate.ShowDialog();
                            }

                        }                        
                    }
                }
            } catch (Exception ex) {
                if (ex is WebException || ex is System.Reflection.TargetInvocationException)
                {
                    main.Log.Error(ex.InnerException.Message);

                }
                else
                {
                    main.Log.Error(ex.Message);
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
                jsonConfig = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + filenameTemplate);

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
            try
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
            finally
            {
                Stop();
            }
        }

        public void Update()
        {
            if ((main.Iface != null) && (main.State == WiFiConnect.WiFiState.Connected) && (main.Location == WiFiConnect.NetLocation.Local))
            {
                if (!checkForConfigUpdates.IsBusy) checkForConfigUpdates.DownloadStringAsync(urlConfigUpdate);
                if (!checkForSSIDUpdates.IsBusy) checkForSSIDUpdates.DownloadStringAsync(urlSSIDUpdate);
                if (!checkForAPUpdates.IsBusy) checkForAPUpdates.DownloadStringAsync(urlAPUpdate);
                if (!checkForLinkUpdates.IsBusy) checkForLinkUpdates.DownloadStringAsync(urlLinkUpdate);
            }
        }

        public void Save()
        {
            string jsonConfig = JsonConvert.SerializeObject(dConfig, Formatting.Indented);

            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + filenameTemplate, jsonConfig);
        }

        public void Stop()
        {
            if (checkForAPUpdates.IsBusy) checkForAPUpdates.CancelAsync();
            if (checkForSSIDUpdates.IsBusy) checkForSSIDUpdates.CancelAsync();
            if (checkForLinkUpdates.IsBusy) checkForLinkUpdates.CancelAsync();
            if (checkForConfigUpdates.IsBusy) checkForConfigUpdates.CancelAsync();
            if (checkForCertUpdates.IsBusy) checkForCertUpdates.CancelAsync();
        }

    }
}
