using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Engine;

namespace EngineTests
{
    [TestClass]
    public class DaySalesHistoryTests
    {
        Commodity grain = new Commodity("grain", null);
        TurnCounter counter;

        [TestInitialize]
        public void SetUp()
        {
            counter = new TurnCounter();
            TurnCounter.RegisterCounter(counter);
        }

        [TestCleanup]
        public void TeadDown()
        {
            TurnCounter.UnregisterCounter(counter);
        }

        [TestMethod]
        public void DasySalesHistorySimpleTest()
        {
            var history = new DaySalesHistory();

            // 1st day
            history.AddTodaySaleData(grain, 3, 9.0M);
            var expected1stDayPrice = 9.0M / 3;
            var actual1stDayPrice = history.GetAverageSellPrice(grain, 1);
            Assert.IsTrue(actual1stDayPrice.HasValue);
            Assert.AreEqual(expected1stDayPrice, actual1stDayPrice.Value);

            // 2nd day
            counter.IncrementCounter();

            history.AddTodaySaleData(grain, 3, 15.0M);
            history.AddTodaySaleData(grain, 1, 25.0M);
            var expected2ndDayPrice = (15.0M + 25.0M) / (1 + 3);
            var actual2ndDayPrice = history.GetAverageSellPrice(grain, 2);
            Assert.IsTrue(actual2ndDayPrice.HasValue);
            Assert.AreEqual(expected2ndDayPrice, actual2ndDayPrice.Value);

            // 3rd day // no transactions added
            Assert.IsNull(history.GetAverageSellPrice(grain, 3));
        }
    }
}
