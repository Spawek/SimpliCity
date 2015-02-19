using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Engine;

namespace EngineTests
{
    [TestClass]
    public class CommodityStorageTests
    {
        Commodity grain = new Commodity("grain", null);

        [TestMethod]
        public void SimpleCommodityStorageTest()
        {
            var storage = new CommodityStorage();

            Assert.AreEqual(0, storage[grain]);
            storage.Deposit(grain, 2);
            Assert.AreEqual(2, storage[grain]);
            storage.Withdraw(grain, 1);
            Assert.AreEqual(1, storage[grain]);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TakeMoreThanAvailableShouldThrow()
        {
            var storage = new CommodityStorage();

            storage.Deposit(grain, 2);
            storage.Withdraw(grain, 3);
        }
    }
}
