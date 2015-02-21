using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartConnect
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            bool startMinimized = false;
            for (int i = 0; i != args.Length; ++i)
            {
                if (args[i] == "/StartMinimized")
                {
                    startMinimized = true;
                }
            }

            Main fMain = new Main();
            if (startMinimized)
            {
                fMain.WindowState = FormWindowState.Minimized;
                fMain.ShowInTaskbar = false;
            }
            else
            {
                fMain.WindowState = FormWindowState.Normal;
                fMain.ShowInTaskbar = true;
            }
            Application.Run(fMain);
        }
    }
}
