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
    public partial class Form1 : Form
    {
        Fuelstation fuelstation;
        CommunicationArduino arduino1;
        CommunicationArduino arduino2;

        CommunicationPCs pcconnection;
        string pincodestring;
        bool pincodeBool = false;

        public Form1()
        {
            InitializeComponent();
        
            fuelstation = new Fuelstation();
            pcconnection = new CommunicationPCs(fuelstation);
            fuelstation.setPC(pcconnection);
            arduino1 = new CommunicationArduino(fuelstation, "COM12");
            arduino2 = new CommunicationArduino(fuelstation, "COM13");
            fuelstation.setArduinos(arduino1, arduino2);
            cbSendTo.SelectedIndex = 0;
            cbRetrieveFrom.SelectedIndex = 1;

            foreach (Car caritem in fuelstation.AllCars)
            {
                listBoxCars.Items.Add(caritem);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            arduino1.CloseConnection();
            arduino2.CloseConnection();
        }

        private void cbSendTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            pcconnection.SendTo = cbSendTo.SelectedIndex + 1;
        }

        private void cbRetrieveFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            pcconnection.RetrieveFrom = cbRetrieveFrom.SelectedIndex + 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(fuelstation.GetFuelType("CX-TV-46").ToString());
            listBoxCars.Items.Clear();
            foreach (Car caritem in fuelstation.AllCars)
            {
                listBoxCars.Items.Add(caritem);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(fuelstation.GetFuelType("BZ-XW-62").ToString());
            listBoxCars.Items.Clear();
            foreach (Car caritem in fuelstation.AllCars)
            {
                listBoxCars.Items.Add(caritem);
            }
        }

        public void ToggleVisibility()
        {
                btnPin0.Visible = !btnPin0.Visible;
                btnPin1.Visible = !btnPin1.Visible; 
                btnPin2.Visible = !btnPin2.Visible; 
                btnPin3.Visible = !btnPin3.Visible; 
                btnPin4.Visible = !btnPin4.Visible; 
                btnPin5.Visible = !btnPin5.Visible;
                btnPin6.Visible = !btnPin6.Visible;
                btnPin7.Visible = !btnPin7.Visible;
                btnPin8.Visible = !btnPin8.Visible;
                btnPin9.Visible = !btnPin9.Visible;
                btnPinC.Visible = !btnPinC.Visible;
                btnPinOK.Visible = !btnPinOK.Visible;
                lblPin.Visible = !lblPin.Visible;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ToggleVisibility();
            PincodeMethod();
        }

        public void PincodeMethod()
        {
            pincodeBool = true;
        }

        private void btnPin1_Click(object sender, EventArgs e)
        {
            if (pincodeBool && lblPin.Text != "****")
            {
                pincodestring += "1";
                lblPin.Text = lblPin.Text + "*";
            }
        }

        private void btnPin2_Click(object sender, EventArgs e)
        {
            if (pincodeBool && lblPin.Text != "****")
            {
                pincodestring += "2";
                lblPin.Text = lblPin.Text + "*";
            }
        }

        private void btnPin3_Click(object sender, EventArgs e)
        {
            if (pincodeBool && lblPin.Text != "****")
            {
                pincodestring += "3";
                lblPin.Text = lblPin.Text + "*";
            }
        }

        private void btnPin4_Click(object sender, EventArgs e)
        {
            if (pincodeBool && lblPin.Text != "****")
            {
                pincodestring += "4";
                lblPin.Text = lblPin.Text + "*";
            }
        }

        private void btnPin5_Click(object sender, EventArgs e)
        {
            if (pincodeBool && lblPin.Text != "****")
            {
                pincodestring += "5";
                lblPin.Text = lblPin.Text + "*";
            }
        }

        private void btnPin6_Click(object sender, EventArgs e)
        {
            if (pincodeBool && lblPin.Text != "****")
            {
                pincodestring += "6";
                lblPin.Text = lblPin.Text + "*";
            }
        }

        private void btnPin7_Click(object sender, EventArgs e)
        {
            if (pincodeBool && lblPin.Text != "****")
            {
                pincodestring += "7";
                lblPin.Text = lblPin.Text + "*";
            }
        }

        private void btnPin8_Click(object sender, EventArgs e)
        {
            if (pincodeBool && lblPin.Text != "****")
            {
                pincodestring += "8";
                lblPin.Text = lblPin.Text + "*";
            }
        }

        private void btnPin9_Click(object sender, EventArgs e)
        {
            if (pincodeBool && lblPin.Text != "****")
            {
                pincodestring += "9";
                lblPin.Text = lblPin.Text + "*";
            }
        }

        private void btnPin0_Click(object sender, EventArgs e)
        {
            if (pincodeBool && lblPin.Text != "****")
            {
                pincodestring += "0";
                lblPin.Text = lblPin.Text + "*";
            }
        }

        private void btnPinC_Click(object sender, EventArgs e)
        {
            if (pincodeBool)
            {
                pincodestring = "";
                lblPin.Text = "";
            }
        }

        private void btnPinOK_Click(object sender, EventArgs e)
        {
            if (pincodeBool && lblPin.Text == "****")
            {
                lblPin.Text = "";
                if(CheckPinCode(pincodestring))
                {
                 //   fuelstation.pay();
                }
            }
        }     
 

        public int getPinFromForm()
        {
            int pin = 0;
            return pin;
        }


        private bool CheckPinCode(string pincode)
        {
            string pin = pincode;
                foreach (Bankaccount bankaccount in fuelstation.Bankaccounts)
                {
                    if (pin == bankaccount.Pincode)
                    {
                        return true;
                    }
                }
            return false;
        }

        private void btnDummyTest_Click(object sender, EventArgs e)
        {
            fuelstation.Pay("GL-09-PQ", 12m);
        }
    }
}
