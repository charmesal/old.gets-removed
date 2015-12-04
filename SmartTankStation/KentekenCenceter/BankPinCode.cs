using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarCenter
{
    public partial class BankPinCode : Form
    {
        public string PinCode;
        public BankPinCode()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (lblPin.Text == "****")
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnPin1_Click(object sender, EventArgs e)
        {
            if (lblPin.Text != "****")
            {
                PinCode += "1";
                lblPin.Text = lblPin.Text + "*";
            }
        }

        private void btnPin2_Click(object sender, EventArgs e)
        {
            if (lblPin.Text != "****")
            {
                PinCode += "2";
                lblPin.Text = lblPin.Text + "*";
            }
        }

        private void btnPin3_Click(object sender, EventArgs e)
        {
            if (lblPin.Text != "****")
            {
                PinCode += "3";
                lblPin.Text = lblPin.Text + "*";
            }
        }

        private void btnPin4_Click(object sender, EventArgs e)
        {
            if (lblPin.Text != "****")
            {
                PinCode += "4";
                lblPin.Text = lblPin.Text + "*";
            }
        }

        private void btnPin5_Click(object sender, EventArgs e)
        {
            if (lblPin.Text != "****")
            {
                PinCode += "5";
                lblPin.Text = lblPin.Text + "*";
            }
        }

        private void btnPin6_Click(object sender, EventArgs e)
        {
            if (lblPin.Text != "****")
            {
                PinCode += "6";
                lblPin.Text = lblPin.Text + "*";
            }
        }

        private void btnPin7_Click(object sender, EventArgs e)
        {
            if (lblPin.Text != "****")
            {
                PinCode += "7";
                lblPin.Text = lblPin.Text + "*";
            }
        }

        private void btnPin8_Click(object sender, EventArgs e)
        {
            if (lblPin.Text != "****")
            {
                PinCode += "8";
                lblPin.Text = lblPin.Text + "*";
            }
        }

        private void btnPin9_Click(object sender, EventArgs e)
        {
            if (lblPin.Text != "****")
            {
                PinCode += "9";
                lblPin.Text = lblPin.Text + "*";
            }
        }

        private void btnPin0_Click(object sender, EventArgs e)
        {
            if (lblPin.Text != "****")
            {
                PinCode += "0";
                lblPin.Text = lblPin.Text + "*";
            }
        }

        private void btnPinC_Click(object sender, EventArgs e)
        {
            ClearPinCode();
        }

        private void ClearPinCode()
        {
            PinCode = "";
            lblPin.Text = "";
        }
    }
}
