using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;

namespace SmartConnect
{
    class DataSender : Sender
    {

        public DataSender(int timeout, int interval, String serverIP, WiFiConnect main) : 
            base(timeout, interval, serverIP, main, Sender.URLStart + serverIP + "/log.php")
        { }

        override public void Run()
        {
            try
            {
                for (; ; )
                {
                    Send();
                    if (interval == 0)
                    {
                        break;
                    }
                    else
                    {
                        Thread.Sleep(interval * 1000);
                    }

                }
            }
            finally
            {
                Stop();
            }
        }

        override public void Send()
        {
            if (!sendClient.IsBusy)
            {
                String netData = main.GetJsonNetData();
                if (netData.Equals(""))
                {
                    Thread.Sleep(5000);
                    netData = main.GetJsonNetData();
                }

                String postData = "json=" + WebUtility.UrlEncode(netData) + "&type=data";
                postData += "&" + main.GetPostSessionData();
                
                String results = "";

                try
                {
                    sendClient.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    results = sendClient.UploadString(urlSend, postData);
                }
                catch (WebException ex)
                {
                    main.Log.Error("DataSender: Send - " + ex.Message);
                }
                try
                {
                    int success = Convert.ToInt32(results);
                    main.Log.Debug(success.ToString() + " lines of network data sent to the server.");
                } 
                catch (FormatException ex)
                {
                    main.Log.Error(ex.Message + ": results = " + results);
                }
            }
        }
    }
}
