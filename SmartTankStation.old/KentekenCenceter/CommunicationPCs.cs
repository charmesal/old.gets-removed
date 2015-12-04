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

namespace CarCenter
{
    class CommunicationPCs
    {
        private Fuelstation fuelstation;
        private TrafficMessageService.TrafficMessageClient myTrafficMessageProxy;
        private int sendTo;
        private int retrieveFrom;



        public CommunicationPCs(Fuelstation fuelstation)
        {
            this.fuelstation = fuelstation;

            sendTo = 1;
            retrieveFrom = 2;

            string serveradress = ShowIpDialog(); 
            

            myTrafficMessageProxy = new CarCenter.TrafficMessageService.TrafficMessageClient();

            try
            {
                EndpointAddress endPointAddress = new EndpointAddress("http://" + serveradress + "/MessageService");
                myTrafficMessageProxy.Endpoint.Address = endPointAddress;

                //check of er een connectie is
                myTrafficMessageProxy.GetServerName();
            }
            catch (Exception)
            {
                Environment.Exit(0);
            }
            
        }

        public Car AskNewCarFuelType(string licenseplate)
        {
            string messageRetrieve = "";
            myTrafficMessageProxy.SendMessage(licenseplate, sendTo);
            while (messageRetrieve.Length <= 0 && !messageRetrieve.StartsWith("$"))
            {
                messageRetrieve = myTrafficMessageProxy.RetrieveMessage(retrieveFrom);
            }
                
            string line = messageRetrieve;
                string[] data = line.Split(',');
                TypeOfFuel fueltype = TypeOfFuel.Unknown;
                if (data[2] == "Petrol")
                {
                    fueltype = TypeOfFuel.Petrol;
                }
                else if (data[2] == "Diesel")
                {
                    fueltype = TypeOfFuel.Diesel;
                }
                else if (data[2] == "LPG")
                {
                    fueltype = TypeOfFuel.LPG;
                }

                foreach (Owner owner in fuelstation.Owners)
                {
                    if (owner.Name == data[5])
                    {
                        Car car = new Car(data[1], fueltype, data[3], Convert.ToDouble(data[4]), owner);
                        return car;
                    }
                }
                Car carwithnoowner = new Car(data[1], fueltype, data[3], Convert.ToDouble(data[4]));
                return carwithnoowner;                          
        }

        public string ShowIpDialog()
        {
            IpDialog Dialog = new IpDialog();
            string returner = "";
          if (Dialog.ShowDialog() == DialogResult.OK)
            {
                
                returner = Dialog.tbIP.Text;
                Dialog.Dispose();
                return returner;
            }
            return "";
        }
    }
}
