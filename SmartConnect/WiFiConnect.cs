using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using NativeWifi;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace SmartConnect
{
    public class WiFiConnect
    {
        static readonly String filenameConfig = "config.json";

        public enum WiFiState
        {
            NoWirelessInterface=1,
            Disconnected,
            Disconnecting,
            Connecting,
            Connected
        }

        public enum NetLocation
        {
            Unknown=1,
            Foreign,
            Local
        }

        WiFiState state;
        public WiFiState State
        {
            get { return state; }
        }

        NetLocation location;
        public NetLocation Location
        {
            get { return location; }
        }

        Main frmMain = null;
        public Main MainForm
        {
            get { return frmMain; }
        }

        String lastSelectedSSID;
        public String LastSelectedSSID
        {
            set { lastSelectedSSID = value; }
            get { return lastSelectedSSID; }
        }

        ConcurrentDictionary<String, Boolean> dFlags = new ConcurrentDictionary<String, Boolean>();

        SCLog log;
        public SCLog Log
        {
            get { return log; }
        }

        WlanClient wClient;
        WlanClient.WlanInterface wlanIface;
        public WlanClient.WlanInterface Iface
        {
            get { return wlanIface; }
            set { wlanIface = value; }
        }

        long lastBytesReceived = 0;
        long lastBytesSent = 0;
        long lastTimeBytes = 0;
        long averageBandwidthInterval = 10;

        // need: add AP/SSID data to updater
        // need: add refresh AP list to NetStatusUpdater
        // need: add send network data thread

        ServerUpdater updaterServer;
        ErrorSender senderErrors;
        NetStatusUpdater updaterNetStatus;
        DataSender senderData;

        Thread updateServerThread=null;
        Thread updateNetStatusThread = null;

        Thread sendErrorThread = null;
        Thread sendDataThread = null;

        ConcurrentDictionary<String,SSID> localSSIDs;
        ConcurrentDictionary<String, int> dNetData;

        ConcurrentDictionary<String, String> dConfig;
        public String Setting(String key)
        {
            if (dFlags.ContainsKey(key)) return dFlags[key].ToString();
            else if (dConfig.ContainsKey(key)) return dConfig[key];
            else return "";
        }
        public void Setting(String key, String value)
        {
            try
            {
                if (dFlags.ContainsKey(key)) dFlags[key] = Convert.ToBoolean(value);
            }
            catch (FormatException ex)
            {
                log.Debug("Setting key = " + key + " and value = " + value + " but value is not Boolean, exact message is " + ex.Message);
            }
            if (dConfig.ContainsKey(key)) dConfig[key] = value;
        }
        public void Setting(String key, Boolean value)
        {
            if (dFlags.ContainsKey(key)) dFlags[key] = value;
            if (dConfig.ContainsKey(key)) dConfig[key] = value.ToString();
        }
        public void SetCertFile(String strCertFilename)
        {
            if (strCertFilename != "")
            {
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + strCertFilename)) dConfig["filenameServerCert"] = strCertFilename;
                else log.Error("SetCertFile: attempt to set cert filename before file exists");
            }
            else log.Error("SetCertFile: attempt to set blank cert filename");
        }

       

        ConcurrentDictionary<String, SSID> dSSIDs;
        public SSID GetSSID(String ssid)
        {
            return dSSIDs[ssid];
        }
        public String[] SSIDs
        {
            get { return dSSIDs.Keys.ToArray<String>(); }
        }
        
        ConcurrentDictionary<String, AP> dAPs;
        public AP GetAP(String ap)
        {
            return dAPs[ap];
        }
        public String[] APs
        {
            get { return dAPs.Keys.ToArray<String>(); }
        }

        // not thread safe
        List<SCLink> lLinks;
        public SCLink[] Links
        {
            get { lock (lLinks) { return lLinks.ToArray(); } }
        }

        public WiFiConnect(Main frmMain)
        {
            this.frmMain = frmMain;
            dSSIDs = new ConcurrentDictionary<string, SSID>();
            dAPs = new ConcurrentDictionary<string, AP>();
            lLinks = new List<SCLink>();
            dConfig = new ConcurrentDictionary<String, String>();
            localSSIDs = new ConcurrentDictionary<string,SSID>();
            dNetData = new ConcurrentDictionary<String, int>();
            wClient = new WlanClient();
            
            Load("all");

            try { Config8021X(); }
            catch (Exception ex) { log.Error("Error on WifiConnect constructor Config8021X(): " + ex.Message); }

            try { SetWirelessConnection(); }
            catch (Exception ex) { log.Error("Error on WifiConnect constructor SetWirelessConnection(): " + ex.Message); }

            int updateInterval=0, updateTimeout=300;

            String tmpUpdateInterval,tmpUpdateTimeout,tmpServerIP,tmpFilenameTemplate;
            try
            {
                if (dConfig.TryGetValue("updateInterval", out tmpUpdateInterval)) updateInterval = Convert.ToInt32(tmpUpdateInterval);
                if (dConfig.TryGetValue("serverTimeout", out tmpUpdateTimeout)) updateTimeout = Convert.ToInt32(tmpUpdateTimeout);
            }
            catch (FormatException ex) { log.Error("Error updateInterval in config is not a number, actual message: " + ex.Message); }

            updaterNetStatus = new NetStatusUpdater(20, this);
            updateNetStatusThread = new Thread(updaterNetStatus.Run);
            updateNetStatusThread.Start();

            if (dConfig.TryGetValue("serverIP", out tmpServerIP))
            {
                if(dConfig.TryGetValue("filenameTemplate", out tmpFilenameTemplate))
                {
                    updaterServer = new ServerUpdater(updateInterval, updateTimeout, tmpServerIP, tmpFilenameTemplate, this);
                    updateServerThread = new Thread(updaterServer.Run);
                }
                else log.Debug("WifiConnect constructor: filenameTemplate config value does not exist, skipping server updater");

                senderErrors = new ErrorSender(updateTimeout, tmpServerIP, this);
                sendErrorThread = new Thread(senderErrors.Run);

                senderData = new DataSender(updateTimeout, updateInterval, tmpServerIP, this);
                sendDataThread = new Thread(senderData.Run);

                bool tmpAutoUpdate = false, tmpSendErrors = false, tmpSendNetworkData = false;
                dFlags.TryGetValue("autoUpdate", out tmpAutoUpdate);
                dFlags.TryGetValue("sendErrors", out tmpSendErrors);
                dFlags.TryGetValue("sendNetworkData", out tmpSendNetworkData);

                if (tmpAutoUpdate && updateTimeout != 0) updateServerThread.Start();
                if (tmpSendErrors) sendErrorThread.Start();
                if (tmpSendNetworkData) sendDataThread.Start();
            }
            else log.Debug("WifiConnect constructor: serverIP config value does not exist, skipping server updater and senders");

        }



        public void Load(String element)
        {
            // clear out the data structures for new data in case this is a Reload rather than 1st time load
            
            if (element.Equals("config") || element.Equals("all"))
            {
                try
                {
                    dConfig.Clear();

                    if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + filenameConfig)) BuildConfigFile();
                    else
                    {
                        String jsonConfig = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + filenameConfig);
                        dConfig = JsonConvert.DeserializeObject<ConcurrentDictionary<String, String>>(jsonConfig);
                    }

                    // load log file
                    String tmpFilenameError="error.log", tmpFilenameDebug="debug.log", tmpBool;
                    bool tmpSendErrors=false, tmpEnableDebug = false;
                    dConfig.TryGetValue("filenameError", out tmpFilenameError);
                    dConfig.TryGetValue("filenameDebug", out tmpFilenameDebug);
                    if (dConfig.TryGetValue("sendErrors", out tmpBool)) tmpSendErrors = Convert.ToBoolean(tmpBool);
                    if (dConfig.TryGetValue("enableDebug", out tmpBool)) tmpEnableDebug = Convert.ToBoolean(tmpBool);

                    log = new SCLog(tmpFilenameError, tmpFilenameDebug, tmpSendErrors, tmpEnableDebug);
                }
                catch (Exception ex) { MessageBox.Show("Early Error before log availability: " + ex.Message); }
                
            }
            
            try
            {
                // load Config
                if (element.Equals("config") || element.Equals("all"))
                {
                    // merge in updated server config file
                    UpdateConfig();
                    // correct strange values
                    ValidateConfig();
                }
                // load Certs
                if (element.Equals("cert") || element.Equals("all"))
                {
                    // read in cert
                    UpdateCert();
                }
                // load Links
                if (element.Equals("link") || element.Equals("all"))
                {
                    lock (lLinks) lLinks.Clear();

                    String tmpFilenameLinks;
                    if (dConfig.TryGetValue("filenameLinks", out tmpFilenameLinks))
                    {
                        if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + tmpFilenameLinks))
                        {
                            // read in links file
                            String jsonLinks = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + tmpFilenameLinks);

                            // NEED: JSON format error handling
                            lock (lLinks) lLinks = JsonConvert.DeserializeObject<List<SCLink>>(jsonLinks);
                            //update UI with public lLinks access (lock included)
                            frmMain.TSSetLinks(Links);
                        }
                        else log.Error("Load: filenameLinks value does not exist in program directory");
                    }
                    else log.Debug("Load: trying to update links but no filenameLinks config value found");
                }
                // load APs
                if (element.Equals("ap") || element.Equals("all"))
                {
                    dAPs.Clear();
                    
                    String tmpFilenameAPs;
                    if (dConfig.TryGetValue("filenameAPs", out tmpFilenameAPs))
                    {
                        if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + tmpFilenameAPs))
                        {
                            String jsonAPs = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + tmpFilenameAPs);

                            // NEED: JSON format error handling
                            dAPs = JsonConvert.DeserializeObject<ConcurrentDictionary<String, AP>>(jsonAPs);
                        }
                        else log.Error("Load: filenameAPs value does not exist in program directory");
                    }
                    else log.Debug("Load: trying to update APs but no filenameAPs config value found");
                }
                // load SSIDs
                if (element.Equals("ssid") || element.Equals("all"))
                {
                    dSSIDs.Clear();

                    String tmpFilenameSSIDs;
                    if (dConfig.TryGetValue("filenameSSIDs", out tmpFilenameSSIDs))
                    {
                        if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + tmpFilenameSSIDs))
                        {
                            String jsonSSIDs = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + tmpFilenameSSIDs);

                            // NEED: JSON format error handling
                            dSSIDs = JsonConvert.DeserializeObject<ConcurrentDictionary<String, SSID>>(jsonSSIDs);
                            foreach (SSID ssid in dSSIDs.Values)
                            {
                                ssid.SetProfile();
                            }
                        }
                        else log.Error("Load: filenameSSIDs value does not exist in program directory");
                    }
                    else log.Debug("Load: trying to update SSIDs but no filenameSSIDs config value found");
                }
                // link SSIDs to APs
                if (element.Equals("ap") || element.Equals("all"))
                {
                    foreach (AP ap in dAPs.Values)
                    {
                        ap.LinkSSIDs(this);
                    }
                }
            }
            catch (Exception ex) { log.Error("Load: generic catch: " + ex.Message); }

        }

        private void BuildConfigFile()
        {
            // build essential config file values
            dConfig["smartConnect"] = "False";
            dConfig["defaultUseOneX"] = "True";
            dConfig["mode"] = "basic";
            dConfig["version"] = "0";
            dConfig["defaultNonBroadcast"] = "False";
            dConfig["filenameServerCert"] = "root.cer";
            dConfig["autoOverwrite"] = "False";
            dConfig["updateInterval"] = "0";
            dConfig["serverIP"] = "127.0.0.1";
            dConfig["averageBandwidthInterval"] = "10";
            dConfig["sendErrors"] = "True";
            dConfig["defaultEncryption"] = "AES";
            dConfig["wifiPing"] = "300";
            dConfig["username"] = "";
            dConfig["internalLocationName"] = "SmartConnect Network";
            dConfig["runOnStartup"] = "False";
            dConfig["filenameDebug"] = "debug.log";
            dConfig["internalNetworkName"] = "NoNet";
            dConfig["disableLinks"] = "True";
            dConfig["filenameLinks"] = "links.json";
            dConfig["vpnPing"] = "600";
            dConfig["filenameAPs"] = "APs.json";
            dConfig["serverTimeout"] = "120";
            dConfig["vpnConnect"] = "False";
            dConfig["lastName"] = "";
            dConfig["filenameTemplate"] = "template.json";
            dConfig["disableBandwidth"] = "False";
            dConfig["filenameError"] = "error.log";
            dConfig["firstName"] = "";
            dConfig["autoConnect"] = "False";
            dConfig["filenameSSIDs"] = "SSIDs.json";
            dConfig["autoUpdate"] = "True";
            dConfig["defaultAuthentication"] = "WPA2";
            dConfig["sendNetworkData"] = "True";
            dConfig["enableDebug"] = "False";

            Save();
        }

        // update the Config file with the latest values from the server config file
        public void UpdateConfig()
        {
            String tmpFilenameTemplate;
            if(dConfig.TryGetValue("filenameTemplate", out tmpFilenameTemplate))
            {
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + tmpFilenameTemplate))
                {
                    String jsonTemplate = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + tmpFilenameTemplate);

                    // NEED: JSON format error handling
                    ConcurrentDictionary<String, String> dTemplate = JsonConvert.DeserializeObject<ConcurrentDictionary<String, String>>(jsonTemplate);

                    String tmpOldVersion="0", tmpNewVersion="0";
                    double oldVersion=0, newVersion=0;
                    bool tmpAutoOverwrite=true;
                    dFlags.TryGetValue("autoOverwrite", out tmpAutoOverwrite);

                    try { if (dTemplate.TryGetValue("version", out tmpNewVersion)) newVersion = Convert.ToDouble(tmpNewVersion); }
                    catch (FormatException ex) { log.Error("UpdateConfig: new template config version value is not a double"); }

                    try { if (dConfig.TryGetValue("version", out tmpOldVersion)) oldVersion = Convert.ToDouble(tmpOldVersion); }
                    catch (FormatException ex) { log.Error("UpdateConfig: old config version value is not a double"); }

                    if (newVersion > oldVersion)
                    {
                        log.Debug("UpdateConfig: new version is really newer, updating config");

                        if (tmpAutoOverwrite)
                        {
                            foreach (String key in dTemplate.Keys)
                            {
                                dConfig[key] = dTemplate[key];
                            }
                        }
                        else
                        {
                            String[] fOverwrites = { "updateInterval", "serverTimeout", "serverIP", "vpnPing", "version", "wifiPing", "filenameServerCert" };
                            foreach (String field in fOverwrites)
                            {
                                dConfig[field] = dTemplate[field];
                            }

                        }
                        Save();
                    }
                    else log.Debug("UpdateConfig: new version is not really newer, skipping update");
                }
                else log.Error("UpdateConfig: file specified in filenameTemplate config value does not exist, skipping update");
            }
            else log.Debug("UpdateConfig: filenameTemplate config value does not exist, skipping update");
        }

        public void ValidateConfig()
        {
            String[] fFlags = { "autoConnect", "smartConnect", "vpnConnect", "autoUpdate", "enableDebug", "disableLinks", "disableBandwidth",
                                  "runOnStartup", "sendErrors", "sendNetworkData","autoOverwrite","defaultUseOneX","defaultNonBroadcast" };
            
            foreach (String key in fFlags)
            {
                Boolean value = false;
                String tmpValue;
                if (dConfig.TryGetValue(key,out tmpValue))
                {
                    try { value = Convert.ToBoolean(tmpValue); }
                    catch (FormatException ex) { log.Debug("ValidateConfig: " + key + " exists, but value is not boolean, exact message is " + ex.Message); }
                }
                else log.Debug("ValidateConfig: " + key + " does not exist in config file");
                dFlags.TryAdd(key, value);
            }

            String tmpUpdateInterval;
            if (dConfig.TryGetValue("updateInterval",out tmpUpdateInterval))
            {
                try { int interval = Convert.ToInt32(tmpUpdateInterval); }
                catch (FormatException ex)
                {
                    log.Debug("ValidateConfig: invalid value for updateInterval setting value to 0; exact message is " + ex.Message);
                    dConfig["updateInterval"] = "0";
                }
            }
            else
            {
                log.Debug("ValidateConfig: updateInterval config value not found using 0");
                dConfig["updateInterval"] = "0";
            }

            String tmpServerTimeout;
            if (dConfig.TryGetValue("serverTimeout",out tmpServerTimeout))
            {
                try { int interval = Convert.ToInt32(tmpServerTimeout); }
                catch (FormatException ex)
                {
                    log.Debug("ValidateConfig: invalid value for serverTimeout setting value to 120; exact message is " + ex.Message);
                    dConfig["serverTimeout"] = "120";
                }
            }
            else
            {
                log.Debug("ValidateConfig: serverTimeout config value not found using 120");
                dConfig["serverTimeout"] = "120";
            }

            String tmpAverageBandwidthInterval;
            if (dConfig.TryGetValue("averageBandwidthInterval", out tmpAverageBandwidthInterval))
            {
                try { averageBandwidthInterval = Convert.ToInt64(tmpAverageBandwidthInterval); }
                catch (FormatException ex)
                {
                    log.Debug("ValidateConfig: invalid value for averageBandwidthInterval, setting value to 10 (minutes); exact message is " + ex.Message);
                    dConfig["averageBandwidthInterval"] = "10";
                }
            }
            else
            {
                log.Debug("ValidateConfig: averageBandwidthInterval config value not found using 10");
                dConfig["averageBandwidthInterval"] = "10";
            }
        }

        public String GetAPNameOrMacString(String apMAC)
        {
            if (dAPs.ContainsKey(apMAC)) return dAPs[apMAC].GetListString();
            else return apMAC;
        }

        public void SetNetData(ConcurrentDictionary<String, int> newNetData)
        {
            lock (dNetData) dNetData = newNetData;
        }

        public String GetJsonNetData()
        {
            String jsonNetData = "";
            lock (dNetData) jsonNetData = JsonConvert.SerializeObject(dNetData, Formatting.Indented);
            return jsonNetData;
        }

        public void UpdateCert()
        {
            bool foundCert = false;
            String tmpFilenameServerCert = "";
            if (dConfig.TryGetValue("filenameServerCert", out tmpFilenameServerCert))
            {
                try
                {
                    // check root server cert is trusted
                    X509Certificate2 serverCert = new X509Certificate2(X509Certificate2.CreateFromCertFile(AppDomain.CurrentDomain.BaseDirectory + tmpFilenameServerCert));
                    //err: file not found

                    X509Store trustedRootCAs = new X509Store(StoreName.Root, StoreLocation.CurrentUser);
                    //err: insufficient privleges?
                    trustedRootCAs.Open(OpenFlags.ReadWrite);

                    foreach (X509Certificate2 cert in (X509Certificate2Collection)trustedRootCAs.Certificates)
                    {
                        if (cert.Equals(serverCert)) { foundCert = true; }
                    }
                    // if not add it
                    if (!foundCert) { trustedRootCAs.Add(serverCert); }

                    trustedRootCAs.Close();
                }
                catch (Exception ex) { log.Error("UpdateCert: Error: " + ex.Message); }
            }
            else { log.Debug("UpdateCert: config file does not have filenameServerCert set"); }

        }

        public void Config8021X()
        {

            // for each SSID setup 802.1x
            
            foreach (WlanClient.WlanInterface wlanIface in wClient.Interfaces)
            {
                Dictionary<String, SSID> tmpSSIDs = new Dictionary<string, SSID>(dSSIDs);
                foreach (Wlan.WlanProfileInfo profileInfo in wlanIface.GetProfiles())
                {
                    String strXML = wlanIface.GetProfileXml(profileInfo.profileName);
                    XDocument xdocProfile = XDocument.Parse(strXML);
                    XNamespace xNS = xdocProfile.Root.GetDefaultNamespace();
                    String strSSIDName = xdocProfile.Element(xNS + "WLANProfile").Element(xNS + "SSIDConfig").Element(xNS + "SSID").Element(xNS + "name").Value;
                    XElement xAuthEnc = xdocProfile.Element(xNS + "WLANProfile").Element(xNS + "MSM").Element(xNS + "security").Element(xNS + "authEncryption");
                    String strAuth = xAuthEnc.Element(xNS + "authentication").Value;
                    String strEnc = xAuthEnc.Element(xNS + "encryption").Value;
                    Boolean b1X = Convert.ToBoolean(xAuthEnc.Element(xNS + "useOneX").Value);
                    String strSharedKey = "";
                    if (!b1X)
                    {
                        XElement xSharedKey = xdocProfile.Element(xNS + "WLANProfile").Element(xNS + "MSM").Element(xNS + "security").Element(xNS + "sharedKey");
                        if (xSharedKey != null)
                        {
                            if (Convert.ToBoolean(xSharedKey.Element(xNS + "protected").Value))
                            {
                                strSharedKey = SCUtility.HexStr2String(xSharedKey.Element(xNS + "keyMaterial").Value);
                            }
                            else
                            {
                                strSharedKey = xSharedKey.Element(xNS + "keyMaterial").Value;
                            }
                        }
                        else
                        {
                            strSharedKey = "";
                        }
                    }

                    if(!localSSIDs.ContainsKey(profileInfo.profileName)) 
                        localSSIDs.TryAdd(profileInfo.profileName,new SSID(strSSIDName,profileInfo.profileName,"","",true,strAuth,strEnc,b1X,strSharedKey,"",xdocProfile));

                    Boolean addProfile = true;
                    
                    
                    if (tmpSSIDs.ContainsKey(strSSIDName))
                    {
                        SSID found = tmpSSIDs[strSSIDName];
                        if ((found.Authentication.Equals(strAuth) && found.Encryption.Equals(strEnc)))
                        {
                            addProfile = false;
                            if(!found.UseOneX.Equals(b1X))
                            {
                                addProfile = true;
                                // del old profile
                                //wlanIface.DeleteProfile(profileInfo.profileName);
                            }
                            else if (!b1X) 
                            {
                                if (!(found.SharedKey.Equals(strSharedKey)))
                                {
                                    addProfile = true;
                                    //del old profile
                                    //wlanIface.DeleteProfile(profileInfo.profileName);
                                }
                            }
                        }

                        if (!addProfile)
                        {
                            tmpSSIDs.Remove(strSSIDName);
                        }
                    }

                }

                // add all needed profiles to this wlan interface
                foreach( SSID ssid in tmpSSIDs.Values )
                {
                    wlanIface.SetProfile(Wlan.WlanProfileFlags.AllUser, ssid.Profile, true);
                    localSSIDs.TryAdd(ssid.Name,ssid);
                }
            }
        }

        public void SetWirelessConnection()
        {
            WlanClient.WlanInterface iface = null;
            String configIface = "";
            bool bConfigIface = dConfig.TryGetValue("wirelessInterface",out configIface);

            lock (wClient)
            {
                foreach (WlanClient.WlanInterface wIface in wClient.Interfaces)
                {
                    if (iface == null)
                    {
                        iface = wIface;
                    }
                    else if (bConfigIface)
                    {
                        if (wIface.InterfaceName == configIface) iface = wIface;
                    }
                    else
                    {
                        if (wIface.InterfaceName == "Wireless Network Connection") iface = wIface;
                    }
                }
            }
            wlanIface = iface;
            if (wlanIface != null) lock (wlanIface) { wlanIface.WlanNotification += WlanEventHandler; }
            SetState();
        }

        public void SetState()
        {
            String strState = "";
            if (wlanIface == null)
            {
                state = WiFiState.NoWirelessInterface;
                strState = "WiFi Not Available";
            }
            else
            {
                lock (wlanIface)
                {
                    switch (wlanIface.InterfaceState)
                    {
                        case Wlan.WlanInterfaceState.Connected:
                            state = WiFiState.Connected;
                            strState = "Connected";
                            break;
                        case Wlan.WlanInterfaceState.Disconnected:
                            state = WiFiState.Disconnected;
                            strState = "Disconnected";
                            break;
                        case Wlan.WlanInterfaceState.Disconnecting:
                            state = WiFiState.Disconnecting;
                            strState = "Disconnecting";
                            break;
                        case Wlan.WlanInterfaceState.NotReady:
                            state = WiFiState.NoWirelessInterface;
                            strState = "WiFi Not Available";
                            break;
                        case Wlan.WlanInterfaceState.Associating:
                        case Wlan.WlanInterfaceState.Authenticating:
                            state = WiFiState.Connecting;
                            strState = "Connecting";
                            break;
                    }
                }
            }

            if (state == WiFiState.Connected)
            {
                SetBandwidth(wlanIface.NetworkInterface.GetIPStatistics().BytesReceived, wlanIface.NetworkInterface.GetIPStatistics().BytesSent);
            }
            else
            {
                SetBandwidth(-1, -1);
            }

            frmMain.TSSetStatus(strState);

        }

        public void SetLocation(NetLocation loc)
        {
            if (state == WiFiState.NoWirelessInterface)
            {
                location = NetLocation.Unknown;
            }
            else
            {
                location = loc;
            }
            String strLoc = "";
            switch (location)
            {
                case NetLocation.Unknown:
                    strLoc = "Unknown - WiFi Not Available";
                    break;
                case NetLocation.Foreign:
                    strLoc = "Not at " + Setting("internalLocationName");
                    break;
                case NetLocation.Local:
                    strLoc = Setting("internalLocationName");
                    break;
            }
            frmMain.TSSetLocation(strLoc);
        }

        private void SetBandwidth(long recieved, long sent)
        {
            String strSent = "";
            String strRecieved = "";
            String strSentAvg = "";
            String strRecievedAvg = "";

            if ((recieved < 0) || (sent < 0) || (wlanIface == null))
            {
                lastBytesReceived = 0;
                lastBytesSent = 0;
                lastTimeBytes = 0;
            }
            else
            {
                Thread.Sleep(50);
                
                long recievedNew = wlanIface.NetworkInterface.GetIPStatistics().BytesReceived;
                long sentNew = wlanIface.NetworkInterface.GetIPStatistics().BytesSent;
                strRecieved = SCUtility.BytesDisplayString(((double)(recievedNew - recieved)) / 0.05,false) + "/s";
                strSent = SCUtility.BytesDisplayString(((double)(sentNew - sent)) / 0.05, false) + "/s";
                
                long timeNow = (System.Diagnostics.Stopwatch.GetTimestamp()) / (TimeSpan.TicksPerMillisecond);
                
                if (lastTimeBytes == 0) lastTimeBytes = timeNow - 50;
                if (lastBytesReceived == 0) lastBytesReceived = recieved;
                if (lastBytesSent == 0) lastBytesSent = sent;
                
                double diffSecs = ((double)(timeNow - lastTimeBytes)) / 1000;
                strRecievedAvg = SCUtility.BytesDisplayString(((double)(recievedNew - lastBytesReceived)) / diffSecs, false) + "/s";
                strSentAvg = SCUtility.BytesDisplayString(((double)(sentNew - lastBytesSent)) / diffSecs, false) + "/s";
 
            }

            frmMain.TSSetBytesR(strRecieved);
            frmMain.TSSetBytesRAvg(strRecievedAvg);
            frmMain.TSSetBytesS(strSent);
            frmMain.TSSetBytesSAvg(strSentAvg);

        }

        public void ConnectOrDisconnect()
        {
            if (wlanIface != null)
            {
                if (wlanIface.InterfaceState == Wlan.WlanInterfaceState.Disconnected ||
                    wlanIface.InterfaceState == Wlan.WlanInterfaceState.Disconnecting)
                {
                    Connect("", null);
                }
                else
                {
                    wlanIface.Disconnect();
                }
            }
        }

        public void Connect(String ssid, String[] bss)
        {
            if (wlanIface != null)
            {
                if (!(wlanIface.InterfaceState == Wlan.WlanInterfaceState.Disconnected ||
                        wlanIface.InterfaceState == Wlan.WlanInterfaceState.Disconnecting)) wlanIface.Disconnect();

                String tmpSelectedSSID = frmMain.TSGetSelectedSSID();
                if (tmpSelectedSSID == null || tmpSelectedSSID.Equals("")) ssid = "";
                else ssid = tmpSelectedSSID.Substring(0, tmpSelectedSSID.IndexOf("(") - 1);
                if (ssid.Equals("")) ssid = lastSelectedSSID;

                if (!ssid.Equals(""))
                {
                    if (ssid.IndexOf("(") > 0) ssid = ssid.Substring(0, ssid.IndexOf("(") - 1);
                    String profileName = "";
                    if (localSSIDs.ContainsKey(ssid))
                    {
                        profileName = localSSIDs[ssid].ProfileName;
                    }
                    else
                    {
                        foreach (SSID item in localSSIDs.Values)
                        {
                            if (item.Name == ssid) profileName = item.ProfileName;
                        }
                        if (profileName.Equals(""))
                        {
                            // add new profile
                        }
                    }
                    if (profileName == null) profileName = ssid;
                    if (!profileName.Equals(""))
                    {
                        frmMain.TSSetConnectButton("Connecting...");
                        if (bss != null && bss.Length > 0)
                        {
                            byte[][] bssMacs = new byte[bss.Length][];
                            int i = 0;
                            foreach (string mac in bss)
                            {
                                bssMacs[i++] = SCUtility.MAC2Bytes(mac);
                            }
                            wlanIface.ConnectBSS(Wlan.WlanConnectionMode.Profile, Wlan.Dot11BssType.Any, bssMacs, profileName);
                        }
                        else
                        {
                            wlanIface.Connect(Wlan.WlanConnectionMode.Profile, Wlan.Dot11BssType.Any, profileName);
                        }

                    }
                }
                else
                {
                    //wlanIface.Connect(Wlan.WlanConnectionMode.Auto, Wlan.Dot11BssType.Any, "");
                    // find first visible network with configured profile
                }
            }

            else log.Error("Error connecting/disconnecting from wireless network: Wireless Interface not found");
        }

        public String GetPostSessionData()
        {
            String postData = "";
            String connectedSSID = "";
            String connectedAP = "";
            String ip = "";
            String connectedTime = "";
            String os = Environment.OSVersion.ToString();
            String mac = "";

            if (wlanIface != null)
            {
                lock (wlanIface)
                {
                    mac = wlanIface.NetworkInterface.GetPhysicalAddress().ToString();

                    if (wlanIface.InterfaceState == Wlan.WlanInterfaceState.Connected)
                    {
                        connectedSSID = Encoding.ASCII.GetString(wlanIface.CurrentConnection.wlanAssociationAttributes.dot11Ssid.SSID).Replace("\0", "");
                        connectedAP = wlanIface.CurrentConnection.wlanAssociationAttributes.Dot11Bssid.ToString();
                        connectedTime = wlanIface.NetworkInterface.GetIPProperties().UnicastAddresses[0].DhcpLeaseLifetime.ToString();
                        foreach (System.Net.NetworkInformation.UnicastIPAddressInformation addr in wlanIface.NetworkInterface.GetIPProperties().UnicastAddresses)
                        {
                            if (addr.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            {
                                ip = addr.Address.ToString();
                            }
                        }
                    }
                }
                postData = "mac=" + WebUtility.UrlEncode(mac) + "&ip=" + WebUtility.UrlEncode(ip) + "&os=" + WebUtility.UrlEncode(os) + "&connected_ssid=" +
                    WebUtility.UrlEncode(connectedSSID) + "&connected_ap=" + WebUtility.UrlEncode(connectedAP) +
                    "&connected_time=" + WebUtility.UrlEncode(connectedTime);
            }
            return postData;
        }

        public void WlanEventHandler(Wlan.WlanNotificationData eventData)
        {
            switch (eventData.notificationSource)
            {
                case Wlan.WlanNotificationSource.ACM:
                    switch ((Wlan.WlanNotificationCodeAcm)eventData.notificationCode)
                    {
                        case Wlan.WlanNotificationCodeAcm.ConnectionAttemptFail:
                            log.Debug("ConnectionAttemptFail");
                            break;
                        case Wlan.WlanNotificationCodeAcm.ConnectionComplete:
                            log.Debug("ConnectionComplete");
                            break;
                        case Wlan.WlanNotificationCodeAcm.ConnectionStart:
                            state = WiFiState.Connecting;
                            updaterNetStatus.Update();
                            log.Debug("ConnectionStart");
                            break;
                        case Wlan.WlanNotificationCodeAcm.Disconnected:
                            state = WiFiState.Disconnected;
                            updaterNetStatus.Update();
                            log.Debug("ACM.Disconnected");
                            break;
                        case Wlan.WlanNotificationCodeAcm.Disconnecting:
                            state = WiFiState.Disconnecting;
                            updaterNetStatus.Update();
                            log.Debug("Disconnecting");
                            break;
                        case Wlan.WlanNotificationCodeAcm.InterfaceRemoval:
                            state = WiFiState.NoWirelessInterface;
                            updaterNetStatus.Update();
                            log.Debug("InterfaceRemoval");
                            break;

                    }
                    break;
                case Wlan.WlanNotificationSource.MSM:
                    switch ((Wlan.WlanNotificationCodeMsm)eventData.notificationCode)
                    {
                        case Wlan.WlanNotificationCodeMsm.AdapterOperationModeChange:
                            log.Debug("AdapterOperationModeChange");
                            break;
                        case Wlan.WlanNotificationCodeMsm.AdapterRemoval:
                           state = WiFiState.NoWirelessInterface;
                            updaterNetStatus.Update();
                            log.Debug("AdapterRemoval");
                            break;
                        case Wlan.WlanNotificationCodeMsm.SignalQualityChange:
                            log.Debug("SignalQualityChange");
                            break;
                        case Wlan.WlanNotificationCodeMsm.Associated:
                            log.Debug("Associated");
                            break;
                        case Wlan.WlanNotificationCodeMsm.Associating:
                            log.Debug("Associating");
                            break;
                        case Wlan.WlanNotificationCodeMsm.Authenticating:
                            log.Debug("Authenticating");
                            break;
                        case Wlan.WlanNotificationCodeMsm.Connected:
                            state = WiFiState.Connected;
                            updaterNetStatus.Update();
                            log.Debug("MSM.Connected");
                            break;
                        case Wlan.WlanNotificationCodeMsm.Disassociating:
                            log.Debug("Disassociating");
                            break;
                        case Wlan.WlanNotificationCodeMsm.Disconnected:
                            state = WiFiState.Disconnected;
                            updaterNetStatus.Update();
                            log.Debug("MSM.Disconnected");
                            break;
                    }
                    break;
            }
        }

        public void Save()
        {
            try
            {
                string jsonConfig = JsonConvert.SerializeObject(dConfig, Formatting.Indented);

                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + filenameConfig, jsonConfig);
            }
            catch (Exception ex)
            {
                log.Error("WiFiConnect.Save: " + ex.Message);
            }
        }

        public void Close()
        {
            Save();

            if (updateServerThread != null)
            {
                if(updateServerThread.IsAlive) updateServerThread.Abort();
            }
            if (sendErrorThread != null)
            {
                if (sendErrorThread.IsAlive) sendErrorThread.Abort();
            }
            if (updateNetStatusThread != null)
            {
                if (updateNetStatusThread.IsAlive) updateNetStatusThread.Abort();
            }
            if (sendDataThread != null)
            {
                if (sendDataThread.IsAlive) sendDataThread.Abort();
            }
        }

        public void Update()
        {
            if(updaterServer != null) updaterServer.Update();
        }

    }

}
