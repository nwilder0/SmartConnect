using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Threading;

namespace SmartConnect
{
    public class SCLog
    {
        const String datePattern = @"M/d/yyyy hh:mm:ss tt";

        StreamWriter fileError, fileDebug;

        ConcurrentQueue<String> qError;

        Object errorQueueNotEmpty = new Object();
        public Object ErrorQueueNotEmpty
        {
            get { return errorQueueNotEmpty; }
        }

        Boolean sendErrors, enableDebug;

        public SCLog(String filenameError, String filenameDebug, Boolean sendErrors, Boolean enableDebug)
        {
            this.sendErrors = sendErrors;
            this.enableDebug = enableDebug;

            fileError = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + filenameError);
            fileDebug = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + filenameDebug);
            fileError.AutoFlush = true;
            fileDebug.AutoFlush = true;


            qError = new ConcurrentQueue<String>();

            /*
            qError.Enqueue("blah1");
            qError.Enqueue("blah2");
            qError.Enqueue("blah blah blah3");

            String jError = JsonConvert.SerializeObject(qError, Formatting.Indented);
            MessageBox.Show(jError);
             */
             
        }

        public void error(String mesg)
        {
            try
            {
                DateTime now = DateTime.Now;
                mesg = now.ToString(datePattern) + ": " + mesg;
                lock(fileError) fileError.WriteLine(mesg);

                if (sendErrors)
                {
                    qError.Enqueue(mesg);
                    if (Monitor.TryEnter(errorQueueNotEmpty, 1000))
                    {
                        Monitor.PulseAll(errorQueueNotEmpty);
                        Monitor.Exit(errorQueueNotEmpty);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in logging: " + ex.Message);
            }
        }

        public void debug(String mesg)
        {
            if (enableDebug)
            {
                DateTime now = DateTime.Now;
                mesg = now.ToString(datePattern) + ": " + mesg;
                lock(fileDebug) fileDebug.WriteLine(mesg);
            }
        }

        public String getQueuedErrors()
        {
            if (qError.Count != 0)
            {
                return JsonConvert.SerializeObject(qError.ToArray(), Formatting.Indented);
            }
            else
            {
                return "";
            }
        }

        public void DequeueErrors(int num)
        {
            if(num>0) {
                String mesg;
                int i = 0;
                while(num>0)
                {
                    i++;
                    if (qError.TryDequeue(out mesg)) num--;
                    if (i > 10000) break;
                }
                if(num>0) error("DequeueErrors: Failure with Dequeuing sent errors");
            }
        }
    }
}
