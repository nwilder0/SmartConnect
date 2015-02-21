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
        WebClient checkForUpdates;
        ConcurrentDictionary<String, String> dConfig;
        Uri urlUpdate;
        WiFiConnect main;

        public Updater(int updateInterval, int timeout, String serverIP, String filenameTemplate, WiFiConnect main)
        {
            this.updateInterval = updateInterval;
            this.timeout = timeout;
            this.serverIP = serverIP;
            this.filenameTemplate = filenameTemplate;
            dConfig = new ConcurrentDictionary<string, string>();

            loadConfigTemplate();

            checkForUpdates = new WebClient();
            checkForUpdates.DownloadStringCompleted += checkForUpdates_DownloadStringCompleted;
            String strURL = "http://" + serverIP + "/config.php";
            urlUpdate = new Uri(strURL);
            
        }

        void checkForUpdates_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try {
                Exception ex = e.Error;
                if (ex is WebException)
                {
                    MessageBox.Show("Unable to reach server for updates - canceling further updates \n" + ex.Message);
                    updateInterval = 0;
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

                        save();

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
                    MessageBox.Show(ex.InnerException.Message);

                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void loadConfigTemplate()
        {
            dConfig.Clear();
            String jsonConfig = "";

            // load the json config template
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + filenameTemplate))
            {
                jsonConfig = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + filenameTemplate);

                dConfig = JsonConvert.DeserializeObject<ConcurrentDictionary<String, String>>(jsonConfig);
                if (!dConfig.ContainsKey("version")) dConfig["version"] = "0";
                validateConfig();
            }
            else
            {
                dConfig["version"] = "0";
            }
        }

        private void validateConfig()
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

        public void run()
        {
            if (updateInterval == 0)
            {
                update();
            }
            else
            {
                for (; ; )
                {
                    if (updateInterval == 0)
                    {
                        break;
                    }
                    update();
                    Thread.Sleep(updateInterval * 60 * 1000);
                }
            }
        }

        public void update()
        {
            if (!checkForUpdates.IsBusy) checkForUpdates.DownloadStringAsync(urlUpdate);
        }

        public void save()
        {
            string jsonConfig = JsonConvert.SerializeObject(dConfig, Formatting.Indented);

            System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + filenameTemplate, jsonConfig);
        }

        public void mergeConfig()
        {

        }
    }
}
