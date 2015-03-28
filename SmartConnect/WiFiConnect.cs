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

namespace SmartConnect
{
    public class WiFiConnect
    {
        static readonly String filenameConfig = "config.json";

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
            Config8021X();

            int updateInterval=0, updateTimeout=300;

            updateInterval = Convert.ToInt32(dConfig["updateInterval"]);
            updateTimeout = Convert.ToInt32(dConfig["serverTimeout"]);

            updaterNetStatus = new NetStatusUpdater(20, this);

            updateNetStatusThread = new Thread(updaterNetStatus.Run);

            updateNetStatusThread.Start();
            
            updaterServer = new ServerUpdater(updateInterval, updateTimeout, dConfig["serverIP"], dConfig["filenameTemplate"], this);
            
            updateServerThread = new Thread(updaterServer.Run);

            if (dFlags["autoUpdate"] && updateTimeout != 0)
            {
                updateServerThread.Start();
            }

            senderErrors = new ErrorSender(updateTimeout, dConfig["serverIP"], this);

            sendErrorThread = new Thread(senderErrors.Run);

            if (dFlags["sendErrors"])
            {
                sendErrorThread.Start();
            }

            senderData = new DataSender(updateTimeout, updateInterval, dConfig["serverIP"], this);

            sendDataThread = new Thread(senderData.Run);
            if (dFlags["sendNetworkData"])
            {
                sendDataThread.Start();
            }
 

        }



        public void Load(String element)
        {
            // clear out the data structures for new data in case this is a Reload rather than 1st time load
            
            if (element.Equals("config") || element.Equals("all"))
            {
                try
                {
                    dConfig.Clear();

                    String jsonConfig = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + filenameConfig);

                    dConfig = JsonConvert.DeserializeObject<ConcurrentDictionary<String, String>>(jsonConfig);

                    // load log file
                    log = new SCLog(dConfig["filenameError"], dConfig["filenameDebug"],
                        Convert.ToBoolean(dConfig["sendErrors"]), Convert.ToBoolean(dConfig["enableDebug"]));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Early Error before log availability: " + ex.Message);
                }
                
            }
            
            try
            {
                if (element.Equals("config") || element.Equals("all"))
                {
                    ValidateConfig();
                    // merge in updated server config file
                    UpdateConfig();
                }
                if (element.Equals("link") || element.Equals("all"))
                {
                    lock (lLinks) lLinks.Clear();

                    // read in links file
                    String jsonLinks = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + dConfig["filenameLinks"]);

                    // NEED: JSON format error handling
                    lock (lLinks) lLinks = JsonConvert.DeserializeObject<List<SCLink>>(jsonLinks);
                    //update UI
                    frmMain.TSSetLinks(Links);
                }
                if (element.Equals("ap") || element.Equals("all"))
                {
                    dAPs.Clear();
                    // NEED: file error handling
                    String jsonAPs = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + dConfig["filenameAPs"]);

                    // NEED: JSON format error handling
                    dAPs = JsonConvert.DeserializeObject<ConcurrentDictionary<String, AP>>(jsonAPs);
                }
                if (element.Equals("ssid") || element.Equals("all"))
                {
                    dSSIDs.Clear();
                    // NEED: file error handling
                    String jsonSSIDs = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + dConfig["filenameSSIDs"]);

                    // NEED: JSON format error handling
                    dSSIDs = JsonConvert.DeserializeObject<ConcurrentDictionary<String, SSID>>(jsonSSIDs);
                    foreach (SSID ssid in dSSIDs.Values)
                    {
                        ssid.SetProfile();
                    }
                }
                if (element.Equals("ap") || element.Equals("all"))
                {
                    foreach (AP ap in dAPs.Values)
                    {
                        ap.LinkSSIDs(this);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }

        }

        // update the Config file with the latest values from the server config file
        public void UpdateConfig()
        {
            // NEED: file error handling
            String jsonTemplate = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + dConfig["filenameTemplate"]);

            // NEED: JSON format error handling
            ConcurrentDictionary<String, String> dTemplate = JsonConvert.DeserializeObject<ConcurrentDictionary<String, String>>(jsonTemplate);

            if (Convert.ToDouble(dTemplate["version"]) > Convert.ToDouble(dConfig["version"]))
            {
                if (dFlags["autoOverwrite"])
                {
                    foreach (String key in dTemplate.Keys)
                    {
                        dConfig[key] = dTemplate[key];
                    }
                }
                else
                {
                    String[] fOverwrites = { "updateInterval", "serverTimeout", "serverIP", "vpnPing", "version", "wifiPing" };
                    foreach (String field in fOverwrites)
                    {
                        dConfig[field] = dTemplate[field];
                    }

                }
                Save();
            }
            
        }

        public void ValidateConfig()
        {
            String[] fFlags = { "autoConnect", "smartConnect", "vpnConnect", "autoUpdate", "enableDebug", "disableLinks", "runOnStartup", "sendErrors", "sendNetworkData","autoOverwrite" };
            
            foreach (String key in fFlags)
            {
                Boolean value = false;
                if (dConfig.ContainsKey(key))
                {
                    try
                    {
                        value = Convert.ToBoolean(dConfig[key]);
                    }
                    catch (FormatException ex)
                    {
                        log.Debug("ValidateConfig: " + key + " exists, but value is not boolean, exact message is " + ex.Message);
                    }
                }
                else
                {
                    log.Debug("ValidateConfig: " + key + " does not exist in config file");
                }
                dFlags.TryAdd(key, value);
            }

            if (dConfig.ContainsKey("updateInterval"))
            {
                try
                {
                    int interval = Convert.ToInt32(dConfig["updateInterval"]);
                }
                catch (FormatException ex)
                {
                    log.Debug("ValidateConfig: invalid value for updateInterval setting value to 0; exact message is " + ex.Message);
                    dConfig["updateInterval"] = "0";
                }
            }
            else
            {
                dConfig["updateInterval"] = "0";
            }

            if (dConfig.ContainsKey("serverTimeout"))
            {
                try
                {
                    int interval = Convert.ToInt32(dConfig["serverTimeout"]);
                }
                catch (FormatException ex)
                {
                    log.Debug("ValidateConfig: invalid value for serverTimeout setting value to 120; exact message is " + ex.Message);
                    dConfig["serverTimeout"] = "120";
                }
            }
            else
            {
                dConfig["serverTimeout"] = "120";
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

        public void Config8021X()
        {
            bool foundCert = false;

            // check root server cert is trusted
            X509Certificate2 serverCert = new X509Certificate2(X509Certificate2.CreateFromCertFile(AppDomain.CurrentDomain.BaseDirectory + dConfig["filenameServerCert"]));
            //err: file not found

            X509Store trustedRootCAs = new X509Store(StoreName.Root, StoreLocation.CurrentUser);
            //err: insufficient privleges?
            trustedRootCAs.Open(OpenFlags.ReadWrite);
            
            X509Certificate2Collection arrRootCerts = (X509Certificate2Collection)trustedRootCAs.Certificates;

            foreach (X509Certificate2 cert in arrRootCerts)
            {
                if (cert.Equals(serverCert)) { foundCert = true; }
            }
            // if not add it
            if (!foundCert) { trustedRootCAs.Add(serverCert); }
            
            trustedRootCAs.Close();

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

        public WlanClient.WlanInterface GetWirelessConnection()
        {
            WlanClient.WlanInterface iface = null;
            lock (wClient)
            {
                foreach (WlanClient.WlanInterface wlanIface in wClient.Interfaces)
                {
                    if (iface == null)
                    {
                        iface = wlanIface;
                    }
                    else
                    {
                        if (wlanIface.InterfaceName == "Wireless Network Connection") iface = wlanIface;
                    }
                }
            }
            return iface;
        }

        public void ConnectOrDisconnect()
        {
            WlanClient.WlanInterface iface = GetWirelessConnection();
            if (iface != null)
            {
                if (iface.InterfaceState == Wlan.WlanInterfaceState.Disconnected ||
                    iface.InterfaceState == Wlan.WlanInterfaceState.Disconnecting)
                {
                    Connect("", null);
                }
                else
                {
                    iface.Disconnect();
                }
            }
        }

        public void Connect(String ssid, String[] bss)
        {
            WlanClient.WlanInterface iface = GetWirelessConnection();
            if (iface != null)
            {
                    if (!(iface.InterfaceState == Wlan.WlanInterfaceState.Disconnected ||
                        iface.InterfaceState == Wlan.WlanInterfaceState.Disconnecting)) iface.Disconnect();

                    String tmpSelectedSSID = frmMain.TSGetSelectedSSID();
                    ssid = tmpSelectedSSID.Substring(0, tmpSelectedSSID.IndexOf("(") - 1);
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
                            iface.ConnectSynchronouslyBSS(Wlan.WlanConnectionMode.Profile, Wlan.Dot11BssType.Any, bssMacs, profileName, 5000);
                        }
                        else
                        {
                            iface.ConnectSynchronously(Wlan.WlanConnectionMode.Profile, Wlan.Dot11BssType.Any, profileName, 5000);
                        }

                    }
                }
                updaterNetStatus.Update();
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

            WlanClient.WlanInterface iface = GetWirelessConnection();

            lock (iface)
            {
                mac = iface.NetworkInterface.GetPhysicalAddress().ToString();

                if (iface.InterfaceState == Wlan.WlanInterfaceState.Connected)
                {
                    connectedSSID = Encoding.ASCII.GetString(iface.CurrentConnection.wlanAssociationAttributes.dot11Ssid.SSID).Replace("\0", "");
                    connectedAP = iface.CurrentConnection.wlanAssociationAttributes.Dot11Bssid.ToString();
                    connectedTime = iface.NetworkInterface.GetIPProperties().UnicastAddresses[0].DhcpLeaseLifetime.ToString();
                    foreach(System.Net.NetworkInformation.UnicastIPAddressInformation addr in iface.NetworkInterface.GetIPProperties().UnicastAddresses)
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

            return postData;
        }

        public void Save()
        {
            try
            {
                string jsonConfig = JsonConvert.SerializeObject(dConfig, Formatting.Indented);

                System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + filenameConfig, jsonConfig);
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
            updaterServer.Update();
        }

    }

}
