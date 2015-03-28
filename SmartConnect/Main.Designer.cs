namespace SmartConnect
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.bSettings = new System.Windows.Forms.Button();
            this.bMin = new System.Windows.Forms.Button();
            this.bClose = new System.Windows.Forms.Button();
            this.bConnect = new System.Windows.Forms.Button();
            this.lblLocationLabel = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.lblStatusLabel = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlBandwidth = new System.Windows.Forms.Panel();
            this.lblAvgDown = new System.Windows.Forms.Label();
            this.lblAvgDownLabel = new System.Windows.Forms.Label();
            this.lblAvgUp = new System.Windows.Forms.Label();
            this.lblAvgUpLabel = new System.Windows.Forms.Label();
            this.lblAvg = new System.Windows.Forms.Label();
            this.lblNow = new System.Windows.Forms.Label();
            this.lblDown = new System.Windows.Forms.Label();
            this.lblDownLabel = new System.Windows.Forms.Label();
            this.lblUp = new System.Windows.Forms.Label();
            this.lblUpLabel = new System.Windows.Forms.Label();
            this.lblBandwidth = new System.Windows.Forms.Label();
            this.pnlAdvanced = new System.Windows.Forms.Panel();
            this.lblSSID = new System.Windows.Forms.Label();
            this.cbSSID = new System.Windows.Forms.ComboBox();
            this.cbAP = new System.Windows.Forms.ComboBox();
            this.bAnother = new System.Windows.Forms.Button();
            this.lblAP = new System.Windows.Forms.Label();
            this.pnlLinks = new System.Windows.Forms.FlowLayoutPanel();
            this.notifyIconMain = new System.Windows.Forms.NotifyIcon(this.components);
            this.pnlTitle.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.pnlBandwidth.SuspendLayout();
            this.pnlAdvanced.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTitle
            // 
            this.pnlTitle.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlTitle.BackgroundImage")));
            this.pnlTitle.Controls.Add(this.bSettings);
            this.pnlTitle.Controls.Add(this.bMin);
            this.pnlTitle.Controls.Add(this.bClose);
            this.pnlTitle.Location = new System.Drawing.Point(4, 1);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(322, 70);
            this.pnlTitle.TabIndex = 0;
            this.pnlTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlTitle_MouseDown);
            this.pnlTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlTitle_MouseMove);
            this.pnlTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlTitle_MouseUp);
            // 
            // bSettings
            // 
            this.bSettings.BackColor = System.Drawing.Color.White;
            this.bSettings.FlatAppearance.BorderSize = 0;
            this.bSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bSettings.Image = ((System.Drawing.Image)(resources.GetObject("bSettings.Image")));
            this.bSettings.Location = new System.Drawing.Point(289, 37);
            this.bSettings.Name = "bSettings";
            this.bSettings.Size = new System.Drawing.Size(30, 30);
            this.bSettings.TabIndex = 1;
            this.bSettings.UseVisualStyleBackColor = false;
            this.bSettings.Click += new System.EventHandler(this.bSettings_Click);
            // 
            // bMin
            // 
            this.bMin.BackColor = System.Drawing.Color.White;
            this.bMin.FlatAppearance.BorderSize = 0;
            this.bMin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bMin.Image = ((System.Drawing.Image)(resources.GetObject("bMin.Image")));
            this.bMin.Location = new System.Drawing.Point(256, 1);
            this.bMin.Name = "bMin";
            this.bMin.Size = new System.Drawing.Size(30, 30);
            this.bMin.TabIndex = 1;
            this.bMin.UseVisualStyleBackColor = false;
            this.bMin.Click += new System.EventHandler(this.bMin_Click);
            // 
            // bClose
            // 
            this.bClose.BackColor = System.Drawing.Color.White;
            this.bClose.FlatAppearance.BorderSize = 0;
            this.bClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bClose.Image = ((System.Drawing.Image)(resources.GetObject("bClose.Image")));
            this.bClose.Location = new System.Drawing.Point(290, 1);
            this.bClose.Name = "bClose";
            this.bClose.Size = new System.Drawing.Size(30, 30);
            this.bClose.TabIndex = 1;
            this.bClose.UseVisualStyleBackColor = false;
            this.bClose.Click += new System.EventHandler(this.bClose_Click);
            // 
            // bConnect
            // 
            this.bConnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(255)))));
            this.bConnect.Font = new System.Drawing.Font("Comic Sans MS", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bConnect.ForeColor = System.Drawing.Color.White;
            this.bConnect.Location = new System.Drawing.Point(4, 112);
            this.bConnect.Name = "bConnect";
            this.bConnect.Size = new System.Drawing.Size(322, 62);
            this.bConnect.TabIndex = 1;
            this.bConnect.Text = "Connect to AUCA Wireless";
            this.bConnect.UseVisualStyleBackColor = false;
            this.bConnect.Click += new System.EventHandler(this.bConnect_Click);
            // 
            // lblLocationLabel
            // 
            this.lblLocationLabel.AutoSize = true;
            this.lblLocationLabel.Font = new System.Drawing.Font("Lucida Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocationLabel.Location = new System.Drawing.Point(2, 84);
            this.lblLocationLabel.Name = "lblLocationLabel";
            this.lblLocationLabel.Size = new System.Drawing.Size(79, 17);
            this.lblLocationLabel.TabIndex = 2;
            this.lblLocationLabel.Text = "Location: ";
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Font = new System.Drawing.Font("Lucida Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocation.Location = new System.Drawing.Point(93, 84);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(111, 17);
            this.lblLocation.TabIndex = 3;
            this.lblLocation.Text = "AUCA Campus";
            // 
            // lblStatusLabel
            // 
            this.lblStatusLabel.AutoSize = true;
            this.lblStatusLabel.Font = new System.Drawing.Font("Lucida Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusLabel.Location = new System.Drawing.Point(3, 177);
            this.lblStatusLabel.Name = "lblStatusLabel";
            this.lblStatusLabel.Size = new System.Drawing.Size(63, 17);
            this.lblStatusLabel.TabIndex = 4;
            this.lblStatusLabel.Text = "Status: ";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Lucida Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(96, 177);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(113, 17);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.Text = "Not connected";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.pnlBandwidth);
            this.flowLayoutPanel1.Controls.Add(this.pnlAdvanced);
            this.flowLayoutPanel1.Controls.Add(this.pnlLinks);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(-1, 198);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(328, 376);
            this.flowLayoutPanel1.TabIndex = 25;
            // 
            // pnlBandwidth
            // 
            this.pnlBandwidth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBandwidth.Controls.Add(this.lblAvgDown);
            this.pnlBandwidth.Controls.Add(this.lblAvgDownLabel);
            this.pnlBandwidth.Controls.Add(this.lblAvgUp);
            this.pnlBandwidth.Controls.Add(this.lblAvgUpLabel);
            this.pnlBandwidth.Controls.Add(this.lblAvg);
            this.pnlBandwidth.Controls.Add(this.lblNow);
            this.pnlBandwidth.Controls.Add(this.lblDown);
            this.pnlBandwidth.Controls.Add(this.lblDownLabel);
            this.pnlBandwidth.Controls.Add(this.lblUp);
            this.pnlBandwidth.Controls.Add(this.lblUpLabel);
            this.pnlBandwidth.Controls.Add(this.lblBandwidth);
            this.pnlBandwidth.Location = new System.Drawing.Point(3, 3);
            this.pnlBandwidth.Name = "pnlBandwidth";
            this.pnlBandwidth.Size = new System.Drawing.Size(325, 67);
            this.pnlBandwidth.TabIndex = 28;
            // 
            // lblAvgDown
            // 
            this.lblAvgDown.AutoSize = true;
            this.lblAvgDown.Location = new System.Drawing.Point(257, 46);
            this.lblAvgDown.Name = "lblAvgDown";
            this.lblAvgDown.Size = new System.Drawing.Size(65, 17);
            this.lblAvgDown.TabIndex = 34;
            this.lblAvgDown.Text = "999 KB/s";
            // 
            // lblAvgDownLabel
            // 
            this.lblAvgDownLabel.AutoSize = true;
            this.lblAvgDownLabel.Font = new System.Drawing.Font("Lucida Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvgDownLabel.Location = new System.Drawing.Point(198, 46);
            this.lblAvgDownLabel.Name = "lblAvgDownLabel";
            this.lblAvgDownLabel.Size = new System.Drawing.Size(68, 17);
            this.lblAvgDownLabel.TabIndex = 33;
            this.lblAvgDownLabel.Text = "Down - ";
            // 
            // lblAvgUp
            // 
            this.lblAvgUp.AutoSize = true;
            this.lblAvgUp.Font = new System.Drawing.Font("Lucida Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvgUp.Location = new System.Drawing.Point(121, 46);
            this.lblAvgUp.Name = "lblAvgUp";
            this.lblAvgUp.Size = new System.Drawing.Size(80, 17);
            this.lblAvgUp.TabIndex = 32;
            this.lblAvgUp.Text = "999 KB/s,";
            // 
            // lblAvgUpLabel
            // 
            this.lblAvgUpLabel.AutoSize = true;
            this.lblAvgUpLabel.Font = new System.Drawing.Font("Lucida Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvgUpLabel.Location = new System.Drawing.Point(85, 46);
            this.lblAvgUpLabel.Name = "lblAvgUpLabel";
            this.lblAvgUpLabel.Size = new System.Drawing.Size(46, 17);
            this.lblAvgUpLabel.TabIndex = 31;
            this.lblAvgUpLabel.Text = "Up - ";
            // 
            // lblAvg
            // 
            this.lblAvg.AutoSize = true;
            this.lblAvg.Font = new System.Drawing.Font("Lucida Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvg.Location = new System.Drawing.Point(11, 46);
            this.lblAvg.Name = "lblAvg";
            this.lblAvg.Size = new System.Drawing.Size(75, 17);
            this.lblAvg.TabIndex = 30;
            this.lblAvg.Text = "Average: ";
            // 
            // lblNow
            // 
            this.lblNow.AutoSize = true;
            this.lblNow.Font = new System.Drawing.Font("Lucida Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNow.Location = new System.Drawing.Point(11, 25);
            this.lblNow.Name = "lblNow";
            this.lblNow.Size = new System.Drawing.Size(45, 17);
            this.lblNow.TabIndex = 29;
            this.lblNow.Text = "Now:";
            // 
            // lblDown
            // 
            this.lblDown.AutoSize = true;
            this.lblDown.Location = new System.Drawing.Point(257, 25);
            this.lblDown.Name = "lblDown";
            this.lblDown.Size = new System.Drawing.Size(65, 17);
            this.lblDown.TabIndex = 28;
            this.lblDown.Text = "999 KB/s";
            // 
            // lblDownLabel
            // 
            this.lblDownLabel.AutoSize = true;
            this.lblDownLabel.Font = new System.Drawing.Font("Lucida Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDownLabel.Location = new System.Drawing.Point(198, 25);
            this.lblDownLabel.Name = "lblDownLabel";
            this.lblDownLabel.Size = new System.Drawing.Size(68, 17);
            this.lblDownLabel.TabIndex = 27;
            this.lblDownLabel.Text = "Down - ";
            // 
            // lblUp
            // 
            this.lblUp.AutoSize = true;
            this.lblUp.Font = new System.Drawing.Font("Lucida Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUp.Location = new System.Drawing.Point(121, 25);
            this.lblUp.Name = "lblUp";
            this.lblUp.Size = new System.Drawing.Size(80, 17);
            this.lblUp.TabIndex = 26;
            this.lblUp.Text = "999 KB/s,";
            // 
            // lblUpLabel
            // 
            this.lblUpLabel.AutoSize = true;
            this.lblUpLabel.Font = new System.Drawing.Font("Lucida Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpLabel.Location = new System.Drawing.Point(85, 25);
            this.lblUpLabel.Name = "lblUpLabel";
            this.lblUpLabel.Size = new System.Drawing.Size(46, 17);
            this.lblUpLabel.TabIndex = 25;
            this.lblUpLabel.Text = "Up - ";
            // 
            // lblBandwidth
            // 
            this.lblBandwidth.AutoSize = true;
            this.lblBandwidth.Font = new System.Drawing.Font("Lucida Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBandwidth.Location = new System.Drawing.Point(2, 4);
            this.lblBandwidth.Name = "lblBandwidth";
            this.lblBandwidth.Size = new System.Drawing.Size(93, 17);
            this.lblBandwidth.TabIndex = 24;
            this.lblBandwidth.Text = "Bandwidth: ";
            // 
            // pnlAdvanced
            // 
            this.pnlAdvanced.Controls.Add(this.lblSSID);
            this.pnlAdvanced.Controls.Add(this.cbSSID);
            this.pnlAdvanced.Controls.Add(this.cbAP);
            this.pnlAdvanced.Controls.Add(this.bAnother);
            this.pnlAdvanced.Controls.Add(this.lblAP);
            this.pnlAdvanced.Location = new System.Drawing.Point(3, 76);
            this.pnlAdvanced.Name = "pnlAdvanced";
            this.pnlAdvanced.Size = new System.Drawing.Size(325, 102);
            this.pnlAdvanced.TabIndex = 25;
            // 
            // lblSSID
            // 
            this.lblSSID.AutoSize = true;
            this.lblSSID.Font = new System.Drawing.Font("Lucida Sans", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSSID.Location = new System.Drawing.Point(3, 8);
            this.lblSSID.Name = "lblSSID";
            this.lblSSID.Size = new System.Drawing.Size(50, 20);
            this.lblSSID.TabIndex = 13;
            this.lblSSID.Text = "SSID:";
            // 
            // cbSSID
            // 
            this.cbSSID.FormattingEnabled = true;
            this.cbSSID.Location = new System.Drawing.Point(108, 6);
            this.cbSSID.Name = "cbSSID";
            this.cbSSID.Size = new System.Drawing.Size(211, 24);
            this.cbSSID.TabIndex = 12;
            this.cbSSID.SelectionChangeCommitted += new System.EventHandler(this.cbSSID_SelectionChangeCommitted);
            // 
            // cbAP
            // 
            this.cbAP.FormattingEnabled = true;
            this.cbAP.Location = new System.Drawing.Point(107, 68);
            this.cbAP.Name = "cbAP";
            this.cbAP.Size = new System.Drawing.Size(211, 24);
            this.cbAP.TabIndex = 16;
            this.cbAP.SelectionChangeCommitted += new System.EventHandler(this.cbAP_SelectionChangeCommitted);
            // 
            // bAnother
            // 
            this.bAnother.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(255)))));
            this.bAnother.Font = new System.Drawing.Font("Lucida Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bAnother.ForeColor = System.Drawing.Color.White;
            this.bAnother.Location = new System.Drawing.Point(2, 36);
            this.bAnother.Name = "bAnother";
            this.bAnother.Size = new System.Drawing.Size(322, 26);
            this.bAnother.TabIndex = 14;
            this.bAnother.Text = "Connect to a Different Access Point";
            this.bAnother.UseVisualStyleBackColor = false;
            // 
            // lblAP
            // 
            this.lblAP.AutoSize = true;
            this.lblAP.Font = new System.Drawing.Font("Lucida Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAP.Location = new System.Drawing.Point(3, 72);
            this.lblAP.Name = "lblAP";
            this.lblAP.Size = new System.Drawing.Size(104, 17);
            this.lblAP.TabIndex = 15;
            this.lblAP.Text = "Access Point:";
            // 
            // pnlLinks
            // 
            this.pnlLinks.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlLinks.Location = new System.Drawing.Point(3, 184);
            this.pnlLinks.Name = "pnlLinks";
            this.pnlLinks.Size = new System.Drawing.Size(321, 192);
            this.pnlLinks.TabIndex = 27;
            // 
            // notifyIconMain
            // 
            this.notifyIconMain.BalloonTipText = "SmartConnect";
            this.notifyIconMain.BalloonTipTitle = "SmartConnect";
            this.notifyIconMain.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIconMain.Icon")));
            this.notifyIconMain.Text = "SmartConnect";
            this.notifyIconMain.Visible = true;
            this.notifyIconMain.Click += new System.EventHandler(this.notifyIconMain_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(329, 577);
            this.ControlBox = false;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblStatusLabel);
            this.Controls.Add(this.lblLocation);
            this.Controls.Add(this.lblLocationLabel);
            this.Controls.Add(this.bConnect);
            this.Controls.Add(this.pnlTitle);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Main_Load);
            this.Resize += new System.EventHandler(this.Main_Resize);
            this.pnlTitle.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.pnlBandwidth.ResumeLayout(false);
            this.pnlBandwidth.PerformLayout();
            this.pnlAdvanced.ResumeLayout(false);
            this.pnlAdvanced.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.Button bClose;
        private System.Windows.Forms.Button bMin;
        private System.Windows.Forms.Button bSettings;
        private System.Windows.Forms.Button bConnect;
        private System.Windows.Forms.Label lblLocationLabel;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.Label lblStatusLabel;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel pnlLinks;
        private System.Windows.Forms.Panel pnlAdvanced;
        private System.Windows.Forms.Label lblSSID;
        private System.Windows.Forms.ComboBox cbSSID;
        private System.Windows.Forms.ComboBox cbAP;
        private System.Windows.Forms.Button bAnother;
        private System.Windows.Forms.Label lblAP;
        private System.Windows.Forms.Panel pnlBandwidth;
        private System.Windows.Forms.Label lblAvgDown;
        private System.Windows.Forms.Label lblAvgDownLabel;
        private System.Windows.Forms.Label lblAvgUp;
        private System.Windows.Forms.Label lblAvgUpLabel;
        private System.Windows.Forms.Label lblAvg;
        private System.Windows.Forms.Label lblNow;
        private System.Windows.Forms.Label lblDown;
        private System.Windows.Forms.Label lblDownLabel;
        private System.Windows.Forms.Label lblUp;
        private System.Windows.Forms.Label lblUpLabel;
        private System.Windows.Forms.Label lblBandwidth;
        private System.Windows.Forms.NotifyIcon notifyIconMain;
    }
}

