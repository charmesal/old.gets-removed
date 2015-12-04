using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCenter
{
    public class Owner
    {
        public List<Car> OwnedCars {get; private set;}

        public string Name { get; private set; }
        public Bankaccount Bankaccount { get; private set; }
        public string PinCode { get; private set; }

        /// <summary>
        /// Create a owner.
        /// </summary>
        /// <param name="name">Name of the owner.</param>
        /// <param name="bankaccount">Bankaccountnumber.</param>
        /// <param name="authenticationCode">authenticationCode of the owner.</param>
        public  Owner (string name, Bankaccount bankaccount, string pinCode)
        {
            Name               = name;
            Bankaccount        = bankaccount;
            PinCode = pinCode;

            OwnedCars = new List<Car>();
        }

        public bool ChangeBankAccount(Bankaccount bankAccount)
        {
            if(bankAccount != null)
            {
                Bankaccount = bankAccount;
                return true;
            }
            return false;
        }

        public bool AddCar(Car car)
        {
            if (car != null)
            {
                OwnedCars.Add(car);
                return true;
            }
            return false;
        }

    }
}
