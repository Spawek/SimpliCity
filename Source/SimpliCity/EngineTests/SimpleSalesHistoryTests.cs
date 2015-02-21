using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Engine;
using System.Collections.Generic;

namespace EngineTests
{
    [TestClass]
    public class SimpleSalesHistoryTests : TestWithTimeCounter
    {
        Commodity grain = new Commodity("grain", null);
        IDictionary<Commodity, decimal> defaultPrices;
        const decimal DEFAULT_GRAIN_PRICE = 456;

        public SimpleSalesHistoryTests()
        {
            defaultPrices = new Dictionary<Commodity, decimal>()
            {
                { grain, DEFAULT_GRAIN_PRICE}
            };
        }

        [TestMethod]
        public void SimpleSalesHistoryTest()
        {
            var history = new SimpleSalesHistory(defaultPrices);

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
            counter.IncrementCounter();

            Assert.IsNull(history.GetAverageSellPrice(grain, 3));
        }

        [TestMethod]
        public void SimpleSalesHistoryActualPriceTest()
        {
            var history = new SimpleSalesHistory(defaultPrices);

            // 0 day
            var expected0DayPrice = DEFAULT_GRAIN_PRICE;
            var actual0DayPrice = history.GetActualPrice(grain);
            Assert.AreEqual(expected0DayPrice, actual0DayPrice);

            // 1st day
            history.AddTodaySaleData(grain, 3, 9.0M);
            var expected1stDayPrice = 9.0M;
            var actual1stDayPrice = history.GetActualPrice(grain);
            Assert.AreEqual(expected1stDayPrice, actual1stDayPrice);

            // 2nd day
            counter.IncrementCounter();

            history.AddTodaySaleData(grain, 3, 15.0M);
            history.AddTodaySaleData(grain, 1, 25.0M);
            var expected2ndDayPrice = (15.0M + 25.0M) / (2);
            var actual2ndDayPrice = history.GetActualPrice(grain);
            Assert.AreEqual(expected2ndDayPrice, actual2ndDayPrice);

            // 3rd day // no transactions added // should return last price
            counter.IncrementCounter();

            Assert.AreEqual(expected2ndDayPrice, actual2ndDayPrice);
        }
    }
}
