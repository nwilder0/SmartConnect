namespace SmartConnect
{
    partial class frmSettings
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
            this.cbMode = new System.Windows.Forms.ComboBox();
            this.lblMode = new System.Windows.Forms.Label();
            this.chkAuto = new System.Windows.Forms.CheckBox();
            this.chkSmart = new System.Windows.Forms.CheckBox();
            this.flowPnlAdv = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlAdv = new System.Windows.Forms.Panel();
            this.chkDisableBandwidth = new System.Windows.Forms.CheckBox();
            this.chkDisableLinks = new System.Windows.Forms.CheckBox();
            this.lblDisable = new System.Windows.Forms.Label();
            this.lblServerIP = new System.Windows.Forms.Label();
            this.txtServerIP = new System.Windows.Forms.TextBox();
            this.chkSendConnectionData = new System.Windows.Forms.CheckBox();
            this.chkSendErrors = new System.Windows.Forms.CheckBox();
            this.chkRunOnStart = new System.Windows.Forms.CheckBox();
            this.chkEnableDebug = new System.Windows.Forms.CheckBox();
            this.bUpdate = new System.Windows.Forms.Button();
            this.chkAutoUpdate = new System.Windows.Forms.CheckBox();
            this.chkAutoVPN = new System.Windows.Forms.CheckBox();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.bOK = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtFirst = new System.Windows.Forms.TextBox();
            this.txtLast = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblFirst = new System.Windows.Forms.Label();
            this.lblLast = new System.Windows.Forms.Label();
            this.flowPnlAdv.SuspendLayout();
            this.pnlAdv.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbMode
            // 
            this.cbMode.FormattingEnabled = true;
            this.cbMode.Items.AddRange(new object[] {
            "Basic",
            "Advanced"});
            this.cbMode.Location = new System.Drawing.Point(55, 15);
            this.cbMode.Name = "cbMode";
            this.cbMode.Size = new System.Drawing.Size(95, 24);
            this.cbMode.TabIndex = 2;
            this.cbMode.SelectedIndexChanged += new System.EventHandler(this.cbMode_SelectedIndexChanged);
            // 
            // lblMode
            // 
            this.lblMode.AutoSize = true;
            this.lblMode.Font = new System.Drawing.Font("Lucida Sans", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMode.Location = new System.Drawing.Point(4, 18);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(54, 16);
            this.lblMode.TabIndex = 3;
            this.lblMode.Text = "Mode: ";
            // 
            // chkAuto
            // 
            this.chkAuto.AutoSize = true;
            this.chkAuto.Location = new System.Drawing.Point(173, 4);
            this.chkAuto.Name = "chkAuto";
            this.chkAuto.Size = new System.Drawing.Size(111, 21);
            this.chkAuto.TabIndex = 4;
            this.chkAuto.Text = "AutoConnect";
            this.chkAuto.UseVisualStyleBackColor = true;
            // 
            // chkSmart
            // 
            this.chkSmart.AutoSize = true;
            this.chkSmart.Location = new System.Drawing.Point(173, 32);
            this.chkSmart.Name = "chkSmart";
            this.chkSmart.Size = new System.Drawing.Size(119, 21);
            this.chkSmart.TabIndex = 5;
            this.chkSmart.Text = "SmartConnect";
            this.chkSmart.UseVisualStyleBackColor = true;
            // 
            // flowPnlAdv
            // 
            this.flowPnlAdv.Controls.Add(this.pnlAdv);
            this.flowPnlAdv.Controls.Add(this.pnlButtons);
            this.flowPnlAdv.Location = new System.Drawing.Point(0, 142);
            this.flowPnlAdv.Name = "flowPnlAdv";
            this.flowPnlAdv.Size = new System.Drawing.Size(308, 249);
            this.flowPnlAdv.TabIndex = 6;
            // 
            // pnlAdv
            // 
            this.pnlAdv.Controls.Add(this.chkDisableBandwidth);
            this.pnlAdv.Controls.Add(this.chkDisableLinks);
            this.pnlAdv.Controls.Add(this.lblDisable);
            this.pnlAdv.Controls.Add(this.lblServerIP);
            this.pnlAdv.Controls.Add(this.txtServerIP);
            this.pnlAdv.Controls.Add(this.chkSendConnectionData);
            this.pnlAdv.Controls.Add(this.chkSendErrors);
            this.pnlAdv.Controls.Add(this.chkRunOnStart);
            this.pnlAdv.Controls.Add(this.chkEnableDebug);
            this.pnlAdv.Controls.Add(this.bUpdate);
            this.pnlAdv.Controls.Add(this.chkAutoUpdate);
            this.pnlAdv.Controls.Add(this.chkAutoVPN);
            this.pnlAdv.Location = new System.Drawing.Point(3, 3);
            this.pnlAdv.Name = "pnlAdv";
            this.pnlAdv.Size = new System.Drawing.Size(305, 205);
            this.pnlAdv.TabIndex = 0;
            // 
            // chkDisableBandwidth
            // 
            this.chkDisableBandwidth.AutoSize = true;
            this.chkDisableBandwidth.Location = new System.Drawing.Point(177, 65);
            this.chkDisableBandwidth.Name = "chkDisableBandwidth";
            this.chkDisableBandwidth.Size = new System.Drawing.Size(122, 21);
            this.chkDisableBandwidth.TabIndex = 11;
            this.chkDisableBandwidth.Text = "Bandwidth Info";
            this.chkDisableBandwidth.UseVisualStyleBackColor = true;
            // 
            // chkDisableLinks
            // 
            this.chkDisableLinks.AutoSize = true;
            this.chkDisableLinks.Location = new System.Drawing.Point(67, 65);
            this.chkDisableLinks.Name = "chkDisableLinks";
            this.chkDisableLinks.Size = new System.Drawing.Size(103, 21);
            this.chkDisableLinks.TabIndex = 4;
            this.chkDisableLinks.Text = "Quick Links";
            this.chkDisableLinks.UseVisualStyleBackColor = true;
            // 
            // lblDisable
            // 
            this.lblDisable.AutoSize = true;
            this.lblDisable.Location = new System.Drawing.Point(7, 65);
            this.lblDisable.Name = "lblDisable";
            this.lblDisable.Size = new System.Drawing.Size(59, 17);
            this.lblDisable.TabIndex = 10;
            this.lblDisable.Text = "Disable:";
            // 
            // lblServerIP
            // 
            this.lblServerIP.AutoSize = true;
            this.lblServerIP.Location = new System.Drawing.Point(11, 178);
            this.lblServerIP.Name = "lblServerIP";
            this.lblServerIP.Size = new System.Drawing.Size(111, 17);
            this.lblServerIP.TabIndex = 9;
            this.lblServerIP.Text = "AUCA Server IP:";
            // 
            // txtServerIP
            // 
            this.txtServerIP.Location = new System.Drawing.Point(129, 176);
            this.txtServerIP.Name = "txtServerIP";
            this.txtServerIP.Size = new System.Drawing.Size(149, 22);
            this.txtServerIP.TabIndex = 8;
            // 
            // chkSendConnectionData
            // 
            this.chkSendConnectionData.AutoSize = true;
            this.chkSendConnectionData.Location = new System.Drawing.Point(11, 148);
            this.chkSendConnectionData.Name = "chkSendConnectionData";
            this.chkSendConnectionData.Size = new System.Drawing.Size(209, 21);
            this.chkSendConnectionData.TabIndex = 7;
            this.chkSendConnectionData.Text = "Send Network Data to AUCA";
            this.chkSendConnectionData.UseVisualStyleBackColor = true;
            // 
            // chkSendErrors
            // 
            this.chkSendErrors.AutoSize = true;
            this.chkSendErrors.Location = new System.Drawing.Point(11, 120);
            this.chkSendErrors.Name = "chkSendErrors";
            this.chkSendErrors.Size = new System.Drawing.Size(163, 21);
            this.chkSendErrors.TabIndex = 6;
            this.chkSendErrors.Text = "Send Errors to AUCA";
            this.chkSendErrors.UseVisualStyleBackColor = true;
            // 
            // chkRunOnStart
            // 
            this.chkRunOnStart.AutoSize = true;
            this.chkRunOnStart.Location = new System.Drawing.Point(11, 91);
            this.chkRunOnStart.Name = "chkRunOnStart";
            this.chkRunOnStart.Size = new System.Drawing.Size(129, 21);
            this.chkRunOnStart.TabIndex = 5;
            this.chkRunOnStart.Text = "Run On Startup";
            this.chkRunOnStart.UseVisualStyleBackColor = true;
            this.chkRunOnStart.CheckedChanged += new System.EventHandler(this.chkRunOnStart_CheckedChanged);
            // 
            // chkEnableDebug
            // 
            this.chkEnableDebug.AutoSize = true;
            this.chkEnableDebug.Location = new System.Drawing.Point(11, 38);
            this.chkEnableDebug.Name = "chkEnableDebug";
            this.chkEnableDebug.Size = new System.Drawing.Size(148, 21);
            this.chkEnableDebug.TabIndex = 3;
            this.chkEnableDebug.Text = "Enable Debug Log";
            this.chkEnableDebug.UseVisualStyleBackColor = true;
            // 
            // bUpdate
            // 
            this.bUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(255)))));
            this.bUpdate.ForeColor = System.Drawing.Color.White;
            this.bUpdate.Location = new System.Drawing.Point(161, 5);
            this.bUpdate.Name = "bUpdate";
            this.bUpdate.Size = new System.Drawing.Size(111, 28);
            this.bUpdate.TabIndex = 2;
            this.bUpdate.Text = "Update Now";
            this.bUpdate.UseVisualStyleBackColor = false;
            this.bUpdate.Click += new System.EventHandler(this.bUpdate_Click);
            // 
            // chkAutoUpdate
            // 
            this.chkAutoUpdate.AutoSize = true;
            this.chkAutoUpdate.Location = new System.Drawing.Point(11, 8);
            this.chkAutoUpdate.Name = "chkAutoUpdate";
            this.chkAutoUpdate.Size = new System.Drawing.Size(109, 21);
            this.chkAutoUpdate.TabIndex = 1;
            this.chkAutoUpdate.Text = "Auto Update";
            this.chkAutoUpdate.UseVisualStyleBackColor = true;
            // 
            // chkAutoVPN
            // 
            this.chkAutoVPN.AutoSize = true;
            this.chkAutoVPN.Location = new System.Drawing.Point(144, 91);
            this.chkAutoVPN.Name = "chkAutoVPN";
            this.chkAutoVPN.Size = new System.Drawing.Size(143, 21);
            this.chkAutoVPN.TabIndex = 0;
            this.chkAutoVPN.Text = "VPN AutoConnect";
            this.chkAutoVPN.UseVisualStyleBackColor = true;
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.bOK);
            this.pnlButtons.Controls.Add(this.bCancel);
            this.pnlButtons.Location = new System.Drawing.Point(3, 214);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(305, 34);
            this.pnlButtons.TabIndex = 8;
            // 
            // bOK
            // 
            this.bOK.BackColor = System.Drawing.Color.LightBlue;
            this.bOK.Location = new System.Drawing.Point(43, 4);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(75, 27);
            this.bOK.TabIndex = 0;
            this.bOK.Text = "Save";
            this.bOK.UseVisualStyleBackColor = false;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // bCancel
            // 
            this.bCancel.BackColor = System.Drawing.Color.LightBlue;
            this.bCancel.Location = new System.Drawing.Point(184, 3);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 27);
            this.bCancel.TabIndex = 1;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = false;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(87, 60);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(213, 22);
            this.txtUsername.TabIndex = 7;
            // 
            // txtFirst
            // 
            this.txtFirst.Location = new System.Drawing.Point(87, 87);
            this.txtFirst.Name = "txtFirst";
            this.txtFirst.Size = new System.Drawing.Size(213, 22);
            this.txtFirst.TabIndex = 8;
            // 
            // txtLast
            // 
            this.txtLast.Location = new System.Drawing.Point(87, 113);
            this.txtLast.Name = "txtLast";
            this.txtLast.Size = new System.Drawing.Size(213, 22);
            this.txtLast.TabIndex = 9;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(7, 62);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(83, 17);
            this.lblUsername.TabIndex = 10;
            this.lblUsername.Text = "AUCA User:";
            // 
            // lblFirst
            // 
            this.lblFirst.AutoSize = true;
            this.lblFirst.Location = new System.Drawing.Point(7, 89);
            this.lblFirst.Name = "lblFirst";
            this.lblFirst.Size = new System.Drawing.Size(80, 17);
            this.lblFirst.TabIndex = 11;
            this.lblFirst.Text = "First Name:";
            // 
            // lblLast
            // 
            this.lblLast.AutoSize = true;
            this.lblLast.Location = new System.Drawing.Point(7, 115);
            this.lblLast.Name = "lblLast";
            this.lblLast.Size = new System.Drawing.Size(80, 17);
            this.lblLast.TabIndex = 12;
            this.lblLast.Text = "Last Name:";
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(312, 394);
            this.ControlBox = false;
            this.Controls.Add(this.txtLast);
            this.Controls.Add(this.txtFirst);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.flowPnlAdv);
            this.Controls.Add(this.chkSmart);
            this.Controls.Add(this.chkAuto);
            this.Controls.Add(this.cbMode);
            this.Controls.Add(this.lblMode);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.lblLast);
            this.Controls.Add(this.lblFirst);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "frmSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.flowPnlAdv.ResumeLayout(false);
            this.pnlAdv.ResumeLayout(false);
            this.pnlAdv.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbMode;
        private System.Windows.Forms.Label lblMode;
        private System.Windows.Forms.CheckBox chkAuto;
        private System.Windows.Forms.CheckBox chkSmart;
        private System.Windows.Forms.FlowLayoutPanel flowPnlAdv;
        private System.Windows.Forms.Panel pnlAdv;
        private System.Windows.Forms.Button bUpdate;
        private System.Windows.Forms.CheckBox chkAutoUpdate;
        private System.Windows.Forms.CheckBox chkAutoVPN;
        private System.Windows.Forms.CheckBox chkDisableLinks;
        private System.Windows.Forms.CheckBox chkEnableDebug;
        private System.Windows.Forms.Label lblServerIP;
        private System.Windows.Forms.TextBox txtServerIP;
        private System.Windows.Forms.CheckBox chkSendConnectionData;
        private System.Windows.Forms.CheckBox chkSendErrors;
        private System.Windows.Forms.CheckBox chkRunOnStart;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtFirst;
        private System.Windows.Forms.TextBox txtLast;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblFirst;
        private System.Windows.Forms.Label lblLast;
        private System.Windows.Forms.CheckBox chkDisableBandwidth;
        private System.Windows.Forms.Label lblDisable;
    }
}