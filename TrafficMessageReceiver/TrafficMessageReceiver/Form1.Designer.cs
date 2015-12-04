namespace TrafficMessageReceiver2
{
    partial class Form1
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
            this.messageLbl = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ServerLbl = new System.Windows.Forms.Label();
            this.Lbl = new System.Windows.Forms.Label();
            this.timerRetrive = new System.Windows.Forms.Timer(this.components);
            this.lbCars = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // messageLbl
            // 
            this.messageLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.messageLbl.Location = new System.Drawing.Point(82, 136);
            this.messageLbl.Name = "messageLbl";
            this.messageLbl.Size = new System.Drawing.Size(444, 23);
            this.messageLbl.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "message";
            // 
            // ServerLbl
            // 
            this.ServerLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ServerLbl.Location = new System.Drawing.Point(82, 21);
            this.ServerLbl.Name = "ServerLbl";
            this.ServerLbl.Size = new System.Drawing.Size(444, 23);
            this.ServerLbl.TabIndex = 2;
            // 
            // Lbl
            // 
            this.Lbl.AutoSize = true;
            this.Lbl.Location = new System.Drawing.Point(27, 22);
            this.Lbl.Name = "Lbl";
            this.Lbl.Size = new System.Drawing.Size(36, 13);
            this.Lbl.TabIndex = 1;
            this.Lbl.Text = "server";
            // 
            // timerRetrive
            // 
            this.timerRetrive.Interval = 10;
            this.timerRetrive.Tick += new System.EventHandler(this.timerRetrive_Tick);
            // 
            // lbCars
            // 
            this.lbCars.FormattingEnabled = true;
            this.lbCars.Location = new System.Drawing.Point(82, 183);
            this.lbCars.Name = "lbCars";
            this.lbCars.Size = new System.Drawing.Size(444, 95);
            this.lbCars.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(256, 91);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "40-NZ-SP";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 300);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lbCars);
            this.Controls.Add(this.Lbl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ServerLbl);
            this.Controls.Add(this.messageLbl);
            this.Name = "Form1";
            this.Text = "TrafficMessageReceiver";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label messageLbl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label ServerLbl;
        private System.Windows.Forms.Label Lbl;
        private System.Windows.Forms.Timer timerRetrive;
        private System.Windows.Forms.ListBox lbCars;
        private System.Windows.Forms.Button button1;
    }
}

