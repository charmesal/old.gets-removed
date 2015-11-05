using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarCenter;

namespace CarCenterUnitTests
{
    [TestClass]
    public class BankAccountTest
    {
        [TestMethod]
        public void Testmakingbankaccountwiththreevalues()
        {
            Bankaccount bankaccount = new Bankaccount("coen", "1234", 5.45m);

            Assert.AreEqual("coen", bankaccount.AccountNumber);
            Assert.AreEqual(1234, bankaccount.Pincode);
            Assert.AreEqual(5.45m, bankaccount.Balance);
        }

        [TestMethod]
        public void TestPaymethod()
        {
            Bankaccount bankaccount = new Bankaccount("coen", "1234", 5.45m);

            bankaccount.Pay(3.45m);

            Assert.AreEqual(2.0m, bankaccount.Balance);
        }
        [TestMethod]
        public void TestPaymethodwithnegativevalue()
        {
            Bankaccount bankaccount = new Bankaccount("coen", "1234", 5.45m);

            bankaccount.Pay(-1.00m);

            Assert.AreEqual(5.45m, bankaccount.Balance);
        }
    }
}
