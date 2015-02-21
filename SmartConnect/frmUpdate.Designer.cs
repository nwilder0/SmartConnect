namespace SmartConnect
{
    partial class frmUpdate
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.bNow = new System.Windows.Forms.Button();
            this.bLater = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.Black;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.ForeColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point(19, 7);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(202, 66);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "A new program update has been downloaded.  This update will be applied the next t" +
    "ime the program restarts.";
            // 
            // bNow
            // 
            this.bNow.BackColor = System.Drawing.SystemColors.Control;
            this.bNow.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bNow.Location = new System.Drawing.Point(11, 79);
            this.bNow.Name = "bNow";
            this.bNow.Size = new System.Drawing.Size(97, 29);
            this.bNow.TabIndex = 1;
            this.bNow.Text = "Restart Now";
            this.bNow.UseVisualStyleBackColor = false;
            this.bNow.Click += new System.EventHandler(this.bNow_Click);
            // 
            // bLater
            // 
            this.bLater.BackColor = System.Drawing.SystemColors.Control;
            this.bLater.Location = new System.Drawing.Point(120, 80);
            this.bLater.Name = "bLater";
            this.bLater.Size = new System.Drawing.Size(104, 28);
            this.bLater.TabIndex = 2;
            this.bLater.Text = "Restart Later";
            this.bLater.UseVisualStyleBackColor = false;
            this.bLater.Click += new System.EventHandler(this.bLater_Click);
            // 
            // frmUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(239, 117);
            this.ControlBox = false;
            this.Controls.Add(this.bLater);
            this.Controls.Add(this.bNow);
            this.Controls.Add(this.textBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button bNow;
        private System.Windows.Forms.Button bLater;

    }
}