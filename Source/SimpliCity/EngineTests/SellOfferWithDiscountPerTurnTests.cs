﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Engine;

namespace EngineTests
{
    [TestClass]
    public class SellOfferWithDiscountPerTurnTests
    {
        [TestMethod]
        public void SellOfferWithDiscountPerTurnTest()
        {
            const decimal INITIAL_PRICE = 10.0M;
            const double DISCOUNT_PER_TURN = 0.05;  // 5%
            const double DELTA = 0.0001;

            TurnCounter counter = new TurnCounter();
            TurnCounter.RegisterCounter(counter);

            var sellOffer = new SellOfferWithDiscountPerTurn(null, 0,
                INITIAL_PRICE, null, DISCOUNT_PER_TURN);

            Assert.AreEqual(INITIAL_PRICE, sellOffer.Price);
            counter.IncrementCounter();
            Assert.AreEqual((double)INITIAL_PRICE * 0.95, (double)sellOffer.Price, DELTA);
            counter.IncrementCounter();
            Assert.AreEqual((double)INITIAL_PRICE * 0.95 * 0.95, (double)sellOffer.Price, DELTA);
        }
    }
}