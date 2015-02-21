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
        }

        String mac;
        public String MAC
        {
            get { return mac; }
        }

        int currentClients;
        public int CurrentClients
        {
            get { return currentClients; }
        }

        int maxClients;
        public int MaxClients
        {
            get { return maxClients; }
        }

        Boolean isLockable;
        public Boolean IsLockable
        {
            get { return isLockable; }
        }

        String[] SSIDs;

        ConcurrentDictionary<String, SSID> dSSIDs = new ConcurrentDictionary<string, SSID>();

        public AP(String name, String mac, int currentClients, int maxClients, Boolean isLockable, String[] SSIDs)
        {
            this.name = name;
            this.mac = mac;
            this.currentClients = currentClients;
            this.maxClients = maxClients;
            this.isLockable = isLockable;
            this.SSIDs = SSIDs;
            
        }

        public String GetListString()
        {
            return (name + " (" + mac + ")");

        }

        public void LinkSSIDs(WiFiConnect controller)
        {
            foreach (String ssidName in SSIDs)
            {
                try
                {
                    dSSIDs[ssidName] = controller.getSSID(ssidName);
                }
                catch (KeyNotFoundException)
                {

                }
            }
        }
    }
}
