using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCenter
{
    public class Bankaccount
    {
        public string AccountNumber { get; private set; }
        public string Pincode { get; private set; }
        public decimal Balance { get; private set; }

        /// <summary>
        /// Create a bankaccount.
        /// </summary>
        /// <param name="accountNumber">The name of the bankaccount.</param>
        /// <param name="pincode">The pincode of the bankaccount.</param>
        /// <param name="balance">The balance of the bankaccount.</param>
        public Bankaccount(string accountNumber, string pincode, decimal balance)
        {
            AccountNumber    = accountNumber;
            Pincode = pincode;
            Balance = balance;
        }

        /// <summary>
        /// Paymethod.
        /// </summary>
        /// <param name="ammount">The ammount that will be subtracted from the balance.</param>
        public bool Pay(decimal ammount)
        {
            if (ammount > 0 && (Balance - ammount) >= 0 )
            {
                Balance -= ammount;
                return true;
            }
            return false;
        }
    }
}
