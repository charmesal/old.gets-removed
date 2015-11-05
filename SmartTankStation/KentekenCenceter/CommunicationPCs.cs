using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace CarCenter
{
    class CommunicationPCs
    {
        private Fuelstation fuelstation;
        private TrafficMessageService.TrafficMessageClient myTrafficMessageProxy;
        public int SendTo { get; set; }
        public int RetrieveFrom { get; set; }

        public CommunicationPCs(Fuelstation fuelstation)
        {
            this.fuelstation = fuelstation;

            myTrafficMessageProxy = new CarCenter.TrafficMessageService.TrafficMessageClient();
        }

        public Car AskNewCarFuelType(string licenseplate)
        {
            string messageRetrieve = "";
            myTrafficMessageProxy.SendMessage(licenseplate, SendTo);
            while (messageRetrieve.Length <= 0 && !messageRetrieve.StartsWith("$"))
            {
                messageRetrieve = myTrafficMessageProxy.RetrieveMessage(RetrieveFrom);
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
    }
}
