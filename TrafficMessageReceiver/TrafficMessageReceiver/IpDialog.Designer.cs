namespace TrafficMessageReceiver2
{
    partial class IpDialog
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
            this.tbIP = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.lblIp = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbIP
            // 
            this.tbIP.Location = new System.Drawing.Point(12, 32);
            this.tbIP.Name = "tbIP";
            this.tbIP.Size = new System.Drawing.Size(201, 20);
            this.tbIP.TabIndex = 0;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(240, 13);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(70, 39);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lblIp
            // 
            this.lblIp.AutoSize = true;
            this.lblIp.Location = new System.Drawing.Point(14, 13);
            this.lblIp.Name = "lblIp";
            this.lblIp.Size = new System.Drawing.Size(202, 13);
            this.lblIp.TabIndex = 2;
            this.lblIp.Text = "Type IP adress from Server and press OK\r\n";
            // 
            // IpDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(317, 68);
            this.Controls.Add(this.lblIp);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.tbIP);
            this.KeyPreview = true;
            this.Name = "IpDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IPdialog";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.IpDialog_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.IpDialog_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblIp;
        public System.Windows.Forms.TextBox tbIP;
    }
}