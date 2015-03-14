using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace SmartConnect
{
    class Sender
    {
        public static String URLStart = "http://";
        public static String URLStartSSL = "https://";
        public static String URLEnd = "/";
        protected int timeout;
        protected int interval;
        protected String serverIP;
        protected WebClient sendClient;
        protected Uri urlSend;
        protected WiFiConnect main;

        public Sender(int timeout, int interval, String serverIP, WiFiConnect main, String strURL)
        {
            this.timeout = timeout;
            this.interval = interval;
            this.serverIP = serverIP;
            this.main = main;

            sendClient = new WebClient();
            if(strURL.Equals("")) strURL = URLStart + serverIP + URLEnd;
            urlSend = new Uri(strURL);
        }

        virtual public void Run()
        {

        }

        virtual public void Send()
        {

        }

        public void Stop()
        {
            if (sendClient.IsBusy) sendClient.CancelAsync();
        }
    }
}
