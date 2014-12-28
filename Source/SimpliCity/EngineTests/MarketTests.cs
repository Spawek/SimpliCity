using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Engine;
using System.Collections.Generic;

namespace EngineTests
{
    [TestClass]
    public class MarketTests
    {
        Commodity grain = new Commodity();
        Commodity meat = new Commodity();
        Market market = new Market("Market1");
        Company seller = new Company();
        
        public MarketTests()
        {
            seller.commodities.Add(grain, 15);

            SellOffer grainSellOffer1 = new SellOffer()
            {
                ammount = 10,
                commodity = grain,
                price = 2,
                seller = seller
            };

            SellOffer grainSellOffer2 = new SellOffer()
            {
                ammount = 5,
                commodity = grain,
                price = 1,
                seller = seller
            };


            market.addSellOffer(grainSellOffer1);
            market.addSellOffer(grainSellOffer2);
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

            SellOffer meatSellOffer = new SellOffer()
            {
                ammount = 60,
                commodity = meat,
                price = 10,
                seller = seller
            };

            Assert.AreEqual(100, seller.commodities[meat]);
            market.addSellOffer(meatSellOffer);
            Assert.AreEqual(40, seller.commodities[meat]);
        }

        [TestMethod]
        public void MakeBuyOfferTest()
        {
            Company buyer = new Company();
            buyer.money = 50;

            market.MakeBuyOffer(grain, 10, buyer);

            Assert.AreEqual(35, buyer.money);
            Assert.AreEqual(10, buyer.commodities[grain]);
            Assert.AreEqual(15, seller.money);
            Assert.AreEqual(5, market.GetCommodityAvailable(grain));
        }


    }
}
