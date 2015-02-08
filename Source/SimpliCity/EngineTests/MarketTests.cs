using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Engine;
using System.Collections.Generic;

namespace EngineTests
{
    [TestClass]
    public class MarketTests
    {
        Commodity grain = new Commodity("grain", null);
        Commodity meat = new Commodity("meat", null);
        Market market = new Market("Market1", new DaySalesHistory());
        Company seller = new Company("seller", null, null, null);
        
        public MarketTests()
        {
            seller.commodities.Add(grain, 15);

            var grainSellOffer1 = new StableSellOffer(
                ammount: 10,
                commodity: grain,
                price: 2,
                seller: seller
            );

            var grainSellOffer2 = new StableSellOffer(
                ammount: 5,
                commodity: grain,
                price: 1,
                seller: seller
            );

            market.AddSellOffer(grainSellOffer1);
            market.AddSellOffer(grainSellOffer2);
        }

        [TestMethod]
        public void PriceBuyOfferTest()
        {
            var actualPrice = market.PriceBuyOffer(grain, 10);
            decimal expectedPrice = 15; // 5x1 + 5x2

            Assert.AreEqual(expectedPrice, actualPrice);
        }

        [TestMethod]
        public void GetCommodityAvailableTest()
        {
            var actuallGrainAmmount = market.GetCommodityAvailable(grain);
            decimal expectedAmmount = 15;

            Assert.AreEqual(expectedAmmount, actuallGrainAmmount);
        }

        [TestMethod]
        public void SellerShouldHaveCommodityTakenAfterPostingSellOffer()
        {
            seller.commodities.Add(meat, 100);

            var meatSellOffer = new StableSellOffer(
                ammount: 60,
                commodity: meat,
                price: 10,
                seller: seller
            );

            Assert.AreEqual(100, seller.commodities[meat]);
            market.AddSellOffer(meatSellOffer);
            Assert.AreEqual(40, seller.commodities[meat]);
        }

        [TestMethod]
        public void MakeBuyOfferTest()
        {
            Citizen c = new SimpleCitizen("some rich guy", null, decimal.MaxValue);
            Company buyer = new Company("Company", null, null, null);
            c.TransferMoney(buyer, 50M);

            market.MakeBuyOffer(grain, 10, buyer);

            Assert.AreEqual(35, buyer.money);
            Assert.AreEqual(10, buyer.commodities[grain]);
            Assert.AreEqual(15, seller.money);
            Assert.AreEqual(5, market.GetCommodityAvailable(grain));
        }

        [TestMethod]
        public void CalcMaxAmmountAvailableForGivenPriceTest()
        {
            decimal maxPrice = 8M;
            int actualAavailable = market.CalcMaxAmmountAvailableToBuyForGivenPrice(grain, maxPrice);
            int expectedAvailable = 6;

            Assert.AreEqual(expectedAvailable, actualAavailable);
        }
    }
}
