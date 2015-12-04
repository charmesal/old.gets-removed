using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.ServiceModel;

namespace TrafficMessageReceiver2
{
    public partial class Form1 : Form
    {
        // proxy om de TrafficMessageService te gebruiken

        private TrafficMessageService.TrafficMessageClient myTrafficMessageProxy;
        private int sendTo;
        private int retrieveFrom;
        
        public Form1()
        {
            InitializeComponent();

            sendTo = 2;
            retrieveFrom = 1;

            string serveradress = ShowIpDialog();
            
            myTrafficMessageProxy = new TrafficMessageReceiver2.TrafficMessageService.TrafficMessageClient();

            EndpointAddress endPointAddress = new EndpointAddress("http://" + serveradress + "/MessageService");

            myTrafficMessageProxy.Endpoint.Address = endPointAddress;

            // code om de servernaam label serverLbl zichtbaar te maken
            try
            {
                ServerLbl.Text = myTrafficMessageProxy.GetServerName();
            }
            catch 
            {
                MessageBox.Show("ask your local ICT specialist what to do.", "Wrong Ip adress");
                System.Environment.Exit(0);
            }

            timerRetrive.Start();
        } 

       private void timerRetrive_Tick(object sender, EventArgs e)
       {
        string incomingmessage = myTrafficMessageProxy.RetrieveMessage(retrieveFrom);
        if (incomingmessage.Length > 0)
           {
            messageLbl.Text = incomingmessage;
            string[] splitstring = incomingmessage.Split('-'); 
  
                        if (splitstring.Length == 3)
                        {
                            myTrafficMessageProxy.SendMessage(" ," + GetCarFromRDW(incomingmessage), sendTo);
                        }
                 
           }
        }

        public string ShowIpDialog()
        {
            IpDialog Dialog = new IpDialog();
            string returner = "";
                 if (Dialog.ShowDialog(this) == DialogResult.OK)
                 {
                // Read the contents of testDialog's TextBox.
                returner = Dialog.tbIP.Text;
                Dialog.Dispose();
                return returner;
                 }              
            return "";      
        }

        public string GetCarFromRDW(string licenseplate)
        {
            string RawNumberPlate = licenseplate.Replace("-","");
            string htmlcontent = ParseUrl("http://kentekendb.nl/" + RawNumberPlate);

            if (htmlcontent == "not found" )
            {
                return htmlcontent;
            }

            string[] separator1 = new string[] { "Brandstof:" };
            string[] separator2 = new string[] { "width: 40%" };
            string[] htmlsplit1 = htmlcontent.Split(separator1, StringSplitOptions.None);
            string[] htmlsplit2 = htmlsplit1[1].Split(separator2, StringSplitOptions.None);
            string[] htmlsplit3 = htmlsplit2[1].Split('<');
            string[] htmlsplit4 = htmlsplit3[0].Split('>');

            return htmlsplit4[1];
        }

        public string ParseUrl(string url)
        {
            WebClient wc = new WebClient();
            try
            {
                return wc.DownloadString(url);
            }
            catch   
            {
                return "not found";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(GetCarFromRDW("11-KTF-6"));
        }
    }
}
    