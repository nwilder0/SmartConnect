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
    class ErrorSender : Sender
    {
        String jsonErrors;

        public ErrorSender(int timeout, String serverIP, WiFiConnect main) :
            base(timeout, 0, serverIP, main, Sender.URLStart + serverIP + "/log.php")
        { jsonErrors = ""; }

        override public void Run()
        {
            try
            {
                for (; ; )
                {
                    jsonErrors = main.Log.GetQueuedErrorsJson();
                    if (jsonErrors.Equals(""))
                    {
                        if (Monitor.TryEnter(main.Log.ErrorQueueNotEmpty))
                        {
                            Monitor.Wait(main.Log.ErrorQueueNotEmpty);
                        }
                    }
                    else
                    {
                        if (Monitor.TryEnter(main.Log.ErrorQueueNotEmpty))
                        {
                            Send();
                            Monitor.Exit(main.Log.ErrorQueueNotEmpty);
                        }
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
                String postData = "json=" + WebUtility.UrlEncode(jsonErrors) + "&type=error";
                postData += "&" + main.GetPostSessionData();

                String results = "";

                try
                {
                    sendClient.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    results = sendClient.UploadString(urlSend, postData);
                }
                catch (WebException ex)
                {
                    main.Log.Error("ErrorSender: Send - " + ex.Message);
                }
                try
                {
                    int success = Convert.ToInt32(results);
                }
                catch (FormatException ex)
                {
                    main.Log.Error(ex.Message + " and: " + results);
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
