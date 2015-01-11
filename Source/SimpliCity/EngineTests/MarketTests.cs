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
        Market market = new Market("Market1");
        Company seller = new Company("seller", null, null, null);
        
        public MarketTests()
        {
            seller.commodities.Add(grain, 15);

            SellOffer grainSellOffer1 = new SellOffer(
                _ammount: 10,
                _commodity: grain,
                _price: 2,
                _seller: seller
            );

            SellOffer grainSellOffer2 = new SellOffer(
                _ammount: 5,
                _commodity: grain,
                _price: 1,
                _seller: seller
            );


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

            SellOffer meatSellOffer = new SellOffer(
                _ammount: 60,
                _commodity: meat,
                _price: 10,
                _seller: seller
            );

            Assert.AreEqual(100, seller.commodities[meat]);
            market.addSellOffer(meatSellOffer);
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
