using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartConnect
{
    public partial class Main : Form
    {
        private Boolean dragging = false;
        private Point currPos;

        protected WiFiConnect wifiConnect;

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            wifiConnect = new WiFiConnect(this);
            
            LoadVisible();

        }

        public void TSSetLinks(SCLink[] aLinks)
        {
            if (pnlLinks.InvokeRequired) pnlLinks.Invoke(new Action<SCLink[]>(TSSetLinks), new Object[] { aLinks });
            else
            {
                foreach (Control c in pnlLinks.Controls)
                {
                    if (c.GetType().ToString().Equals("System.Windows.Forms.LinkLabel")) pnlLinks.Controls.Remove(c);
                }
                // loop through each link
                int i = 0;
                foreach (SCLink item in aLinks)
                {
                    //create a new label for the link
                    LinkLabel ll = new LinkLabel();
                    // text is the name or message that will be displayed
                    ll.Text = item.Text;
                    // this adds the url data to the message that will open the browser page when clicked
                    ll.Links.Add(new LinkLabel.Link(0, ll.Text.Length, item.Link));
                    // this adds a handler to the LinkLabel to run the URL just like opening something thru explorer
                    ll.LinkClicked += new LinkLabelLinkClickedEventHandler(eventHandlerLinkLabelClicked);
                    // each label needs a unique name
                    ll.Name = "llbl" + i++;
                    // sets the width to the max length, otherwise the label will be locked at the short default width
                    ll.Width = 300;
                    // add the new link label to the appropriate FlowLayoutPanel
                    pnlLinks.Controls.Add(ll);
                }
            }
        }

        public void TSSetConnectButton(String mesg)
        {
            if (bConnect.InvokeRequired) bConnect.Invoke(new Action<String>(TSSetConnectButton), new Object[] { mesg });
            else bConnect.Text = mesg;
        }

        public void TSSetSSIDs(String[] aSSIDs, int connected)
        {
            if (cbSSID.InvokeRequired) cbSSID.Invoke(new Action<String[], int>(TSSetSSIDs),new Object[] {aSSIDs,connected});
            else
            {
                if (connected == -1) connected = cbSSID.SelectedIndex;
                cbSSID.Items.Clear();
                cbSSID.Items.AddRange(aSSIDs);
                cbSSID.SelectedIndex = connected;

            }
        }

        public void TSSetAPs(String[] aAPs, int connected)
        {
            if (cbAP.InvokeRequired) cbAP.Invoke(new Action<String[], int>(TSSetAPs), new Object[] { aAPs, connected });
            else
            {
                if (connected == -1) connected = cbAP.SelectedIndex;
                cbAP.Items.Clear();
                cbAP.Items.AddRange(aAPs);
                cbAP.SelectedIndex = connected;

            }
        }

        public String TSGetSelectedSSID()
        {
            if (cbSSID.InvokeRequired) return (String)(cbSSID.Invoke(new Func<String>(TSGetSelectedSSID)));
            else
            {
                if (cbSSID.SelectedItem != null) return cbSSID.SelectedItem.ToString();
                else return "";
            }
        }

        public String TSGetSelectedAP()
        {
            if (cbAP.InvokeRequired) return (String)(cbAP.Invoke(new Func<String>(TSGetSelectedAP)));
            else
            {
                if (cbAP.SelectedItem != null) return cbAP.SelectedItem.ToString();
                else return "";
            }
        }

        private void LoadVisible()
        {
            Boolean bNoLinks = Convert.ToBoolean(wifiConnect.Setting("disableLinks"));
            Boolean bNoBandwidth = Convert.ToBoolean(wifiConnect.Setting("disableBandwidth"));
            Boolean bAdvanced = wifiConnect.Setting("mode").Equals("advanced");

            int height = 570;

            pnlLinks.Visible = !bNoLinks;
            pnlBandwidth.Visible = !bNoBandwidth;
            pnlAdvanced.Visible = bAdvanced;

            if (bNoLinks) height -= 192;
            if (bNoBandwidth) height -= 67;
            if (!bAdvanced) height -= 110;

            Height = height;
        }

        private void bClose_Click(object sender, EventArgs e)
        {
            wifiConnect.Close();
            Application.Exit();
        }

        private void bMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void bSettings_Click(object sender, EventArgs e)
        {
            frmSettings winSettings = new frmSettings(wifiConnect);
            winSettings.ShowDialog();
            LoadVisible();
        }

        private void pnlTitle_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            currPos = e.Location;
        }

        private void pnlTitle_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void pnlTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point pNew = e.Location;
                Point pRel = this.PointToScreen(pNew);
                this.Location = new Point(pRel.X - currPos.X, pRel.Y - currPos.Y);
            }
        }

        private void eventHandlerLinkLabelClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }

        private void flowLayoutPanelBottom_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlLinks_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Main_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                notifyIconMain.Visible = true;
                notifyIconMain.ShowBalloonTip(500);
                this.Hide();
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                notifyIconMain.Visible = false;
            }
        }

        private void notifyIconMain_Click(object sender, EventArgs e)
        {
            this.Show();
            this.ShowInTaskbar = true;
            this.WindowState = FormWindowState.Normal;
        }

        private void bConnect_Click(object sender, EventArgs e)
        {
            wifiConnect.Connect("");
        }

        private void cbSSID_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbSSID.SelectedItem != null)
            {
                String tmpSelectedSSID = cbSSID.SelectedItem.ToString();
                tmpSelectedSSID = tmpSelectedSSID.Substring(0,tmpSelectedSSID.IndexOf("(")-1);
                wifiConnect.LastSelectedSSID = tmpSelectedSSID;
            }
        }
    }
}
