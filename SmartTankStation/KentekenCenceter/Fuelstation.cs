using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace CarCenter
{
    class Fuelstation
    {
        public List<Bankaccount> Bankaccounts { get; private set; }
        public List<Owner> Owners { get; private set; }
        public List<Car> AllCars { get; private set; }

        private CommunicationPCs pc;

        private CommunicationArduino ard1;
        private CommunicationArduino ard2;

        public Fuelstation()
        {
            Owners = new List<Owner>();
            Bankaccounts = new List<Bankaccount>();
            AllCars = new List<Car>();


            UpdateFromTextDatabase();
        }

        public void setPC(CommunicationPCs pc)
        {
            this.pc = pc;
        }

        public void setArduinos(CommunicationArduino arduino1, CommunicationArduino arduino2)
        {
            ard1 = arduino1;
            ard2 = arduino2;
        }

        public void sendSerialMsg(int whichArduino, String message)
        {
            if (whichArduino == 1)
            {
                ard1.SendMessage(message);
            }
            else if (whichArduino == 2)
            {
                ard2.SendMessage(message);
            }
        }

        public TypeOfFuel GetFuelType(string licenseplate)
        {
            foreach (Car caritem in AllCars)
            {
                if (caritem.Licenseplate == licenseplate)
                {
                    return caritem.Fueltype;
                }
            }

            Car car = pc.AskNewCarFuelType(licenseplate);
            AllCars.Add(car);
            return car.Fueltype;
        }

        public Owner getOwner(string licensePlate)
        {
            foreach (Owner owner in Owners)
            {
                foreach (Car car in owner.OwnedCars)
                {
                    if (car.Licenseplate == licensePlate)
                    {
                        return owner;
                    }
                }
            }
            return null;
        }
        public bool checkPin(string pinCode, Owner owner)
        {
            if (owner.Bankaccount.Pincode == pinCode)
            {
                return true;
            }
            return false;
        }

        public decimal CalculatePrice(string licencePlate, decimal amountOfFuel)
        {
            decimal PetrolPrice = 1.60m;
            decimal DieselPrice = 1.28m;
            decimal LPGPRice = 0.77m;
            decimal price = 0;
            TypeOfFuel fuelType = GetFuelType(licencePlate);
            switch (fuelType)
            {
                case TypeOfFuel.Petrol:
                    price = amountOfFuel * PetrolPrice;
                    break;
                case TypeOfFuel.Diesel:
                    price = amountOfFuel * DieselPrice;
                    break;
                case TypeOfFuel.LPG:
                    price = amountOfFuel * LPGPRice;
                    break;
            }
            return price;
        }

        public void Pay(string licencePlate, decimal amountOfFuel)
        {
            decimal PayAmount = CalculatePrice(licencePlate, amountOfFuel);
            Owner owner = getOwner(licencePlate);
            string AccountNumber = getOwner(licencePlate).Bankaccount.AccountNumber;
            if (owner != null)
            {

                string pinCode = getPinCodeFromUser(owner, PayAmount);
                if (checkPin(pinCode, owner))
                {
                    if (owner.Bankaccount.Pay(PayAmount))
                    {
                        MessageBox.Show(String.Format("Pincode correct\n\nCurrent balance: {0}", owner.Bankaccount.Balance));
                    }
                    else
                    {
                        MessageBox.Show("Not enough balance. Please go inside and pay with cash or else......\n\n\n balance:" + owner.Bankaccount.Balance + "\nPayAmount: " + PayAmount);
                    }
                }
                else
                {
                    MessageBox.Show("pincode incorrect");
                    Pay(licencePlate, amountOfFuel);
                }
            }
        }


        private string getPinCodeFromUser(Owner owner, decimal amountToPay)
        {
            // Create and display an instance of the dialog box
            BankPinCode dlg = new BankPinCode();
            string pinCode = "";

            // Show the dialog and determine the state of the 
            // DialogResult property for the form.
            dlg.lblBalance.Text = "Current Balance: " + owner.Bankaccount.Balance.ToString() + "\n Price: " + amountToPay.ToString();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                pinCode = dlg.PinCode;
                dlg.Dispose();

            }
            return pinCode;
        }

        private void UpdateFromTextDatabase()
        {
            List<string> list = new List<string>();

            using (StreamReader reader = new StreamReader("bankaccountdatabase.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    list.Add(line);
                }
            }

            foreach (string item in list)
            {
                string line = item;
                string[] data = line.Split(',');
                Bankaccount bankaccount = new Bankaccount(data[0], data[1], Convert.ToDecimal(data[2]));
                //Bankaccounts.Add(bankaccount);
                foreach(Owner owner in Owners)
                {
                    if(owner.Name == data[5])
                    {
                        if(!owner.ChangeBankAccount(bankaccount))
                        {
                            MessageBox.Show("Could not add/change bankaccount");
                        }
                    }
                }
            }

            list.Clear();
            using (StreamReader reader = new StreamReader("ownerdatabase.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    list.Add(line);
                }
            }

            foreach (string item in list)
            {
                string line = item;
                string[] data = line.Split(',');
                foreach (Bankaccount account in Bankaccounts)
                {
                    if (account.AccountNumber == data[1])
                    {
                        Owner owner = new Owner(data[0], account, Convert.ToInt32(data[2]));
                        Owners.Add(owner);
                    }
                }
            }

            list.Clear();
            using (StreamReader reader = new StreamReader("AllCarsdatabase.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    list.Add(line);
                }
            }

            foreach (string item in list)
            {
                string line = item;
                string[] data = line.Split(',');
                TypeOfFuel fueltype = TypeOfFuel.Unknown;
                switch (data[1])
                {
                    case "Petrol":
                        fueltype = TypeOfFuel.Petrol;
                        break;
                    case "Diesel":
                        fueltype = TypeOfFuel.Diesel;
                        break;
                    case "LPG":
                        fueltype = TypeOfFuel.LPG;
                        break;
                }
                foreach (Owner owner in Owners)
                {
                    if (owner.Name == data[4])
                    {
                        Car car = new Car(data[0], fueltype, data[2], Convert.ToDouble(data[3]), owner);
                        AllCars.Add(car);
                        owner.OwnedCars.Add(car);
                        break;
                    }
                }


            }

        }
    }
}
