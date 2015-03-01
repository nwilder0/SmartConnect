using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;

namespace SmartConnect
{
    class DataSender
    {
        int timeout;
        int updateInterval;
        String serverIP;
        WebClient sendData;
        Uri urlSend;
        WiFiConnect main;

        public DataSender(int timeout, int updateInterval, String serverIP, WiFiConnect main)
        {
            this.timeout = timeout;
            this.updateInterval = updateInterval;
            this.serverIP = serverIP;
            this.main = main;
            
            sendData = new WebClient();
            String strURL = "http://" + serverIP + "/log.php";
            urlSend = new Uri(strURL);
            
        }

        public void Run()
        {
           for (; ; )
           {
               Send();
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

        public void Send()
        {
            if (!sendData.IsBusy)
            {
                String netData = main.GetNetData();
                if (netData.Equals(""))
                {
                    Thread.Sleep(5000);
                    netData = main.GetNetData();
                }

                String postData = "json=" + WebUtility.UrlEncode(netData) + "&type=data";
                postData += "&" + main.GetPostSessionData();
                
                String results = "";

                try
                {
                    sendData.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    results = sendData.UploadString(urlSend, postData);
                }
                catch (WebException ex)
                {
                    main.Log.error("DataSender: Send - " + ex.Message);
                }
                try
                {
                    int success = Convert.ToInt32(results);
                    main.Log.debug(success.ToString() + " lines of network data sent to the server.");
                } 
                catch (FormatException ex)
                {
                    main.Log.error(ex.Message + ": results = " + results);
                }
            }
        }
    }
}
