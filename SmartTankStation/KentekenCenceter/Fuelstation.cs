using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Net;

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
        private int newAccountNumber = 0;
        private int newAuthenticationNumber = 10000000;

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

            //moet nog de pc opslaan 
            return pc.AskNewCarFuelType(licenseplate);
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
            decimal[] fuelPrices = GetFuelPrice();
            decimal PetrolPrice = fuelPrices[0];
            decimal DieselPrice = fuelPrices[2];
            decimal LPGPRice    = fuelPrices[3];
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
            price = price / 100;
            return price;
        }

        public decimal[] GetFuelPrice()
        {
            string htmlcontent = ParseUrl("http://autotraveler.ru/en/netherlands/trend-price-fuel-netherlands.html#.Vlha93YveM8");
            decimal[] resultarray;
            if (htmlcontent == "not found")
            {
                //If the site doens't load, this will be returned
                resultarray = new decimal[] { 1.60m, 1.65m, 1.25m, 0.75m }; 
                return resultarray;
            }

            int htmlindex1 = htmlcontent.IndexOf("diffBenzPrice");
            int htmlindex2 = htmlcontent.IndexOf("boxFuel rekPriceFuel");

            string htmlsubstring = htmlcontent.Substring(htmlindex1, htmlindex2 - htmlindex1);
            string[] htmlsplit = htmlsubstring.Split('<');

            decimal petrolPrice = Convert.ToDecimal(htmlsplit[2].Substring(9, 5).Replace('.', ','));
            decimal petrolPrice98 = Convert.ToDecimal(htmlsplit[9].Substring(9, 5).Replace('.', ','));
            decimal dieselPrice = Convert.ToDecimal(htmlsplit[16].Substring(9, 5).Replace('.', ','));
            decimal lpgPrice = Convert.ToDecimal(htmlsplit[23].Substring(9, 5).Replace('.', ','));

            resultarray = new decimal[] { petrolPrice, petrolPrice98, dieselPrice, lpgPrice };

            return resultarray;
        }

        public string ParseUrl(string url)
        {
            WebClient wc = new WebClient();

            return wc.DownloadString(url);
        }

        public void Pay(string licencePlate, decimal amountOfFuel)
        {
            decimal PayAmount = CalculatePrice(licencePlate, amountOfFuel);
            Owner owner = getOwner(licencePlate);
            string AccountNumber = owner.Bankaccount.AccountNumber;
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

        public void getTextFromFile(List<string> list, string fileLocation)
        {
            using (StreamReader reader = new StreamReader(fileLocation))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    list.Add(line);
                }
            }
        }

        public Bankaccount getBankaccount(List<string> listBankAccounts, string ownerAccountNumber)
        {
            foreach (string bankAccountString in listBankAccounts)
            {
                string[] dataBankAccount = bankAccountString.Split(',');
                if (dataBankAccount[0] == ownerAccountNumber)
                {
                    decimal balance;
                    if (decimal.TryParse(dataBankAccount[2], out balance))
                    {
                        return new Bankaccount(dataBankAccount[0], dataBankAccount[1], balance);
                    }
                    else
                    {
                        decimal newBalance = 0;
                        return new Bankaccount(dataBankAccount[0], dataBankAccount[1], newBalance);
                    }
                }
            }
            return null;
        }

        private void UpdateFromTextDatabase()
        {
            List<string> listCars = new List<string>();
            List<string> listBankAccounts = new List<string>();
            List<string> listOwners = new List<string>();

            getTextFromFile(listCars, "carsdatabase.txt");
            getTextFromFile(listBankAccounts, "bankaccountdatabase.txt");
            getTextFromFile(listOwners, "ownerdatabase.txt");

            foreach(string ownerString in listOwners)
            {
                string[] dataOwner = ownerString.Split(',');
                Bankaccount ownerBankAccount = getBankaccount(listBankAccounts, dataOwner[1]);

                if (ownerBankAccount != null)
                {
                    Owners.Add(new Owner(dataOwner[0], ownerBankAccount, dataOwner[2]));
                }
                else
                {
                    newAccountNumber++;
                    newAuthenticationNumber++;
                    if (newAccountNumber > 9999)
                    {
                        newAuthenticationNumber = 1;
                    }
                    string newAccountNumberString = newAccountNumber.ToString();
                    string newAuthenticationNumberString = newAuthenticationNumber.ToString();
                    while (newAuthenticationNumberString.Length < 3)
                    {
                        newAuthenticationNumberString = "0" + newAuthenticationNumberString;
                    }
                    Bankaccount bankaccount = new Bankaccount(newAccountNumberString, newAuthenticationNumberString, 0);
                    Owners.Add(new Owner(dataOwner[0], bankaccount, dataOwner[2]));
                }
            }

            foreach (string carString in listCars)
            {
                string[] data = carString.Split(',');
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
