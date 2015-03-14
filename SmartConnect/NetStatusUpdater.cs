using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NativeWifi;
using System.Windows.Forms;
using System.Threading;

namespace SmartConnect
{
    class NetStatusUpdater
    {
        WiFiConnect main;
        int updateInterval;

        public NetStatusUpdater(int updateInterval, WiFiConnect main)
        {
            this.updateInterval = updateInterval;
            this.main = main;
        }

        public void Run()
        {
            for (; ; )
            {
                Update();
                if (updateInterval == 0)
                {
                    break;
                }
                else
                {
                    Thread.Sleep(updateInterval * 1000);
                }

            }
        }

        public void Update()
        {
            ConcurrentDictionary<String, int> dNetData = new ConcurrentDictionary<String, int>();
            Dictionary<String,int> blueSSIDs = new Dictionary<String,int>();
            String[] arrSSIDs = main.SSIDs;
            foreach (String ssid in arrSSIDs)
            {
                blueSSIDs.Add(ssid, 0);
            }
            Dictionary<String, int> greenSSIDs = new Dictionary<String, int>();
            Dictionary<String,int> redSSIDs = new Dictionary<String,int>();

            Dictionary<String, int> blueAPs = new Dictionary<String, int>();
            String[] arrAPs = main.APs;
            foreach (String ap in arrAPs)
            {
                blueAPs.Add(ap, 0);
            }
            Dictionary<String, int> greenAPs = new Dictionary<String, int>();
            Dictionary<String, int> redAPs = new Dictionary<String, int>();

            Wlan.WlanAvailableNetwork[] networks;
            Wlan.WlanBssEntry[] aps=null;
            Wlan.WlanBssEntry[] apsAll = null;

            String connectedSSID = "";

            String connectedAP = "";

            WlanClient.WlanInterface iface = main.GetWirelessConnection();
            lock (iface)
            {
                if (iface.InterfaceState == Wlan.WlanInterfaceState.Connected)
                {
                    Wlan.Dot11Ssid current = iface.CurrentConnection.wlanAssociationAttributes.dot11Ssid;
                    Boolean securityEnabled = iface.CurrentConnection.wlanSecurityAttributes.securityEnabled;
                    Wlan.Dot11BssType bssType = iface.CurrentConnection.wlanAssociationAttributes.dot11BssType;
                    connectedAP = SCUtility.Bytes2MAC(iface.CurrentConnection.wlanAssociationAttributes.dot11Bssid);
                    connectedSSID = Encoding.ASCII.GetString(current.SSID).Replace("\0", "");
                    aps = iface.GetNetworkBssList(current,bssType,securityEnabled);
                }

                networks = iface.GetAvailableNetworkList(0);
                apsAll = iface.GetNetworkBssList();
                if (aps == null) aps = apsAll;
                
            }

            foreach (Wlan.WlanAvailableNetwork network in networks)
            {
                Wlan.Dot11Ssid ssid = network.dot11Ssid;
                int signal = (int)network.wlanSignalQuality;
                String strSSID = Encoding.ASCII.GetString(ssid.SSID).Replace("\0", "");
                if (blueSSIDs.ContainsKey(strSSID))
                {
                    blueSSIDs.Remove(strSSID);
                    if (!greenSSIDs.ContainsKey(strSSID)) greenSSIDs.Add(strSSID, signal);
                }
                else if(!redSSIDs.ContainsKey(strSSID)) redSSIDs.Add(strSSID,signal);
            }

            foreach (Wlan.WlanBssEntry bss in aps)
            {
                Wlan.Dot11Ssid ssid = bss.dot11Ssid;
                String strSSID = Encoding.ASCII.GetString(ssid.SSID).Replace("\0", "");
                String apMAC = SCUtility.Bytes2MAC(bss.dot11Bssid);
                int signalStrength = SCUtility.RSSI2SignalPercent(bss.rssi);
                if (blueAPs.ContainsKey(apMAC))
                {
                    blueAPs.Remove(apMAC);
                    if (!greenAPs.ContainsKey(apMAC)) greenAPs.Add(apMAC, signalStrength);
                }
                else if (!redAPs.ContainsKey(apMAC + " (" + strSSID + ")")) redAPs.Add(apMAC + " (" + strSSID + ")", signalStrength);
            }

            foreach (Wlan.WlanBssEntry bss in apsAll)
            {
                Wlan.Dot11Ssid ssid = bss.dot11Ssid;
                String strSSID = Encoding.ASCII.GetString(ssid.SSID).Replace("\0", "");
                String apMAC = SCUtility.Bytes2MAC(bss.dot11Bssid);
                int signalStrength = SCUtility.RSSI2SignalPercent(bss.rssi);
                dNetData[apMAC] = bss.rssi;
            }
            main.SetNetData(dNetData);

            List<String> lSSIDs = new List<String>();
            List<String> lAPs = new List<String>();
            int i = 0;
            int connectedSSIDIndex = -1;
            int connectedAPIndex = -1;

            foreach (KeyValuePair<String, int> item in greenSSIDs.OrderByDescending(key => key.Value))
            {
                String listName = item.Key;
                if (item.Key == connectedSSID)
                {
                    connectedSSIDIndex = i;
                    listName += " (Connected)";
                }
                else listName += " (Visible)"; 
                
                lSSIDs.Add(listName);
                i++;
            }

            foreach (KeyValuePair<String, int> item in blueSSIDs.OrderByDescending(key => key.Value))
            {
                String listName = item.Key;
                if (item.Key == connectedSSID)
                {
                    connectedSSIDIndex = i;
                    listName += " (Connected)";
                }
                else listName += " (Not Visible)";

                lSSIDs.Add(listName);
                i++;
            }

            String internalNetworkName = main.Setting("internalNetworkName");
            foreach (KeyValuePair<String, int> item in redSSIDs.OrderByDescending(key => key.Value))
            {
                String listName = item.Key;
                if (item.Key == connectedSSID)
                {
                    connectedSSIDIndex = i;
                    listName += " (Connected, Non-" + internalNetworkName + " Network)";
                }
                else listName += " (Non-" + internalNetworkName + " Network)";

                lSSIDs.Add(listName);
                i++;
            }

            i = 0;
            foreach (KeyValuePair<String, int> item in greenAPs.OrderByDescending(key => key.Value))
            {
                String listName = main.GetAPNameOrMacString(item.Key);
                if (item.Key == connectedAP)
                {
                    connectedAPIndex = i;
                    listName += " (Connected)";
                }
                else listName += " (Visible)";
                
                lAPs.Add(listName);
                i++;
            }

            foreach (KeyValuePair<String, int> item in blueAPs.OrderByDescending(key => key.Value))
            {
                String listName = main.GetAPNameOrMacString(item.Key);
                if (item.Key == connectedAP)
                {
                    connectedAPIndex = i;
                    listName += " (Connected)";
                }
                else listName += " (Not Visible)";

                lAPs.Add(listName);
                i++;
            }

            foreach (KeyValuePair<String, int> item in redAPs.OrderByDescending(key => key.Value))
            {
                String listName = item.Key;
                if ((item.Key).Substring(0, item.Key.IndexOf("(") - 1) == connectedAP)
                {
                    connectedAPIndex = i;
                    listName += " (Connected)";
                }
                else listName += " (Non-" + internalNetworkName + " Network)";

                lAPs.Add(listName);
                i++;
            }
            
            String buttonText = "";
            if (connectedSSID.Equals(""))
            {
                if (greenSSIDs.Count > 0)
                    buttonText = "Connect to " + internalNetworkName + " Wireless";
                else
                    buttonText = "(" + internalNetworkName + " Wireless not found.)" + Environment.NewLine + "Connect to Another Network";
            }
            else
            {
                if (connectedSSIDIndex < (greenSSIDs.Count + blueSSIDs.Count))
                    buttonText = "Disconnect from " + internalNetworkName + " Wireless";
                else
                    buttonText = "Disconnect from Non-" + internalNetworkName + " Wireless";
            }
            main.MainForm.TSSetConnectButton(buttonText);
            
            main.MainForm.TSSetSSIDs(lSSIDs.ToArray(), connectedSSIDIndex);
            main.MainForm.TSSetAPs(lAPs.ToArray(), connectedAPIndex);
            main.LastSelectedSSID = connectedSSID;

        }
    }
}
