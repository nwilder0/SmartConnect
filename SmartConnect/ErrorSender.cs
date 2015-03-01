using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using Newtonsoft.Json;


namespace SmartConnect
{
    class ErrorSender
    {
        int timeout;
        String serverIP;
        WebClient sendErrors;
        Uri urlSend;
        WiFiConnect main;

        public ErrorSender(int timeout, String serverIP, WiFiConnect main)
        {
            this.timeout = timeout;
            this.serverIP = serverIP;
            this.main = main;
            
            sendErrors = new WebClient();
            String strURL = "http://" + serverIP + "/log.php";
            urlSend = new Uri(strURL);
            
        }

        public void Run()
        {
           for (; ; )
           {
               String jsonErrors = main.Log.getQueuedErrors();
               if (jsonErrors.Equals(""))
               {
                   if (Monitor.TryEnter(main.Log.ErrorQueueNotEmpty))
                   {
                       Monitor.Wait(main.Log.ErrorQueueNotEmpty);
                   }
               }
               else
               {
                   if(Monitor.TryEnter(main.Log.ErrorQueueNotEmpty))
                   {
                       Send(jsonErrors);
                       Monitor.Exit(main.Log.ErrorQueueNotEmpty);
                   }
               }

           }
        }

        public void Send(String jsonErrors)
        {
            if (!sendErrors.IsBusy)
            {
                String postData = "json=" + WebUtility.UrlEncode(jsonErrors) + "&type=error";
                postData += "&" + main.GetPostSessionData();

                String results = "";

                try
                {
                    sendErrors.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    results = sendErrors.UploadString(urlSend, postData);
                }
                catch (WebException ex)
                {
                    main.Log.error("ErrorSender: Send - " + ex.Message);
                }
                try
                {
                    int success = Convert.ToInt32(results);
                }
                catch (FormatException ex)
                {
                    main.Log.error(ex.Message + " and: " + results);
                }
                finally
                {
                    List<String> lErrors = JsonConvert.DeserializeObject<List<String>>(jsonErrors);
                    main.Log.DequeueErrors(lErrors.Count);
                }
            }
        }

    }
}
