using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartConnect
{
    public class AP
    {
        String name;
        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        String mac;
        public String MAC
        {
            get { return mac; }
            set { mac = value; }
        }

        int currentClients;
        public int CurrentClients
        {
            get { return currentClients; }
            set { currentClients = value; }
        }

        int maxClients;
        public int MaxClients
        {
            get { return maxClients; }
            set { maxClients = value; }
        }

        Boolean isLockable;
        public Boolean IsLockable
        {
            get { return isLockable; }
            set { isLockable = value; }
        }

        String[] aSSIDs;
        public String[] SSIDs
        {
            get { return aSSIDs; }
            set { aSSIDs = value; }
        }

        ConcurrentDictionary<String, SSID> dSSIDs = new ConcurrentDictionary<string, SSID>();

        public AP(String name, String mac, int currentClients, int maxClients, Boolean isLockable, String[] aSSIDs)
        {
            this.name = name;
            this.mac = mac;
            this.currentClients = currentClients;
            this.maxClients = maxClients;
            this.isLockable = isLockable;
            this.aSSIDs = aSSIDs;
            
        }

        public String GetListString()
        {
            return (name + " (" + mac + ")");

        }

        public void LinkSSIDs(WiFiConnect controller)
        {
            if (aSSIDs != null)
            {
                foreach (String ssidName in aSSIDs)
                {
                    try
                    {
                        dSSIDs[ssidName] = controller.GetSSID(ssidName);
                    }
                    catch (KeyNotFoundException ex)
                    {
                        controller.Log.Error("LinkSSIDs: KeyNotFound - " + ex.Message);
                    }
                }
            }
        }
    }
}
