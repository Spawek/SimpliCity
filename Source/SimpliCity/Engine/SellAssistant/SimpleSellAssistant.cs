using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    class SimpleSellAssistant : SellAssistant
    {
        private const decimal SELL_PREMIUM = 0.5M;
        private const double SELL_DISCOUNT_PER_TURN = 0.05;

        public SimpleSellAssistant(Market market)
        {
            Market = market;
        }

        public void SellAsset(AssetsOwner seller, Commodity commodity, int ammount)
        {
            var lastTurnPrice = Market.SalesHistory.GetAverageSellPrice(commodity, TurnCounter.Now - 1);
            var price = lastTurnPrice.HasValue ? lastTurnPrice.Value * (1 + SELL_PREMIUM) : GetDefaultPrice(commodity);

            Market.AddSellOffer(new SellOfferWithDiscountPerTurn(
                commodity: commodity,
                price: price,
                ammount: ammount,
                seller: seller,
                discount: SELL_DISCOUNT_PER_TURN));
        }

        private decimal GetDefaultPrice(Commodity commodity)
        {
            Console.WriteLine("Getting default value for commodity: {0}", commodity.Name);

            decimal defaultPrice;
            if (defaultPrices.TryGetValue(commodity.Name, out defaultPrice))
                return defaultPrice;

            throw new ApplicationException(String.Format(
                "There is no default price for commodity: {0}", commodity.Name));
        }

        private IDictionary<string, decimal> defaultPrices = new Dictionary<string, decimal>()
        {
            {"grain", 4}
        };

        public Market Market { get; private set; }
    }
}
