using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace SmartConnect
{
    public partial class frmSettings : Form
    {
        WiFiConnect wifiConnect;

        public frmSettings(WiFiConnect wifiConnect)
        {
            InitializeComponent();
            this.wifiConnect = wifiConnect;
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            if (cbMode.SelectedIndex == 1) wifiConnect.Setting("mode", "advanced");
            else wifiConnect.Setting("mode", "basic");

            wifiConnect.Setting("autoConnect",chkAuto.Checked);
            wifiConnect.Setting("smartConnect",chkSmart.Checked);
            wifiConnect.Setting("vpnConnect",chkAutoVPN.Checked);
            wifiConnect.Setting("autoUpdate",chkAutoUpdate.Checked);
            wifiConnect.Setting("enableDebug", chkEnableDebug.Checked);
            wifiConnect.Setting("disableLinks",chkDisableLinks.Checked);
            wifiConnect.Setting("disableBandwidth", chkDisableBandwidth.Checked);

            wifiConnect.Setting("runOnStartup", chkRunOnStart.Checked);
            wifiConnect.Setting("sendErrors",chkSendErrors.Checked);
            wifiConnect.Setting("sendNetworkData",chkSendConnectionData.Checked);

            wifiConnect.Setting("serverIP",txtServerIP.Text);

            wifiConnect.Setting("firstName",txtFirst.Text);
            wifiConnect.Setting("lastName",txtLast.Text);
            wifiConnect.Setting("username",txtUsername.Text);

            this.Close();
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            String mode = wifiConnect.Setting("mode");
            if (mode.Equals("advanced"))
            {
                cbMode.SelectedIndex = 1;
                Height = 400;
                pnlAdv.Visible = true;

            }
            else
            {
                cbMode.SelectedIndex = 0;
                pnlAdv.Visible = false;
                Height = 200;
            }

            chkAuto.Checked = Convert.ToBoolean(wifiConnect.Setting("autoConnect"));
            chkSmart.Checked = Convert.ToBoolean(wifiConnect.Setting("smartConnect"));
            chkAutoVPN.Checked = Convert.ToBoolean(wifiConnect.Setting("vpnConnect"));
            chkAutoUpdate.Checked = Convert.ToBoolean(wifiConnect.Setting("autoUpdate"));
            chkEnableDebug.Checked = Convert.ToBoolean(wifiConnect.Setting("enableDebug"));
            chkDisableLinks.Checked = Convert.ToBoolean(wifiConnect.Setting("disableLinks"));
            chkDisableBandwidth.Checked = Convert.ToBoolean(wifiConnect.Setting("disableBandwidth"));
            chkRunOnStart.Checked = Convert.ToBoolean(wifiConnect.Setting("runOnStartup"));
            chkSendErrors.Checked = Convert.ToBoolean(wifiConnect.Setting("sendErrors"));
            chkSendConnectionData.Checked = Convert.ToBoolean(wifiConnect.Setting("sendNetworkData"));

            txtServerIP.Text = wifiConnect.Setting("serverIP");

            txtFirst.Text = wifiConnect.Setting("firstName");
            txtLast.Text = wifiConnect.Setting("lastName");
            txtUsername.Text = wifiConnect.Setting("username");

        }

        private void cbMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbMode.SelectedIndex==1)
            {
                Height = 400;
                pnlAdv.Visible = true;

            }
            else
            {
                pnlAdv.Visible = false;
                Height = 200;
            }
        }

        private void bUpdate_Click(object sender, EventArgs e)
        {
            wifiConnect.Update();
        }

        private void chkRunOnStart_CheckedChanged(object sender, EventArgs e)
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (chkRunOnStart.Checked)
            {
                // add reg key
                rk.SetValue(Application.ProductName, Application.ExecutablePath.ToString() + " /StartMinimized");
            }
            else
            {
                // remove reg key
                rk.DeleteValue(Application.ProductName, false);
            }
        }
    }
}
