using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;


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
            String strURL = "http://" + serverIP + "/send_errors.php";
            urlSend = new Uri(strURL);
            
        }

        public void run()
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
                       send(jsonErrors);
                       Monitor.Exit(main.Log.ErrorQueueNotEmpty);
                   }
               }

           }
        }

        public void send(String jsonErrors)
        {
            if (!sendErrors.IsBusy)
            {
                String safeJsonErrors = WebUtility.UrlEncode("json=" + jsonErrors);
                String results = sendErrors.UploadString(urlSend, safeJsonErrors);
                try
                {
                    int success = Convert.ToInt32(results);
                    main.Log.DequeueErrors(success);
                } 
                catch (FormatException ex)
                {
                    main.Log.error(ex.Message);
                }
            }
        }

    }
}
