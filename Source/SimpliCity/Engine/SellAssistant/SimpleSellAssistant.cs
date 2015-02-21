using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class SimpleSellAssistant : SellAssistant
    {
        private const decimal SELL_PREMIUM = 0.2M;
        private const double SELL_DISCOUNT_PER_TURN = 0.05;

        public SimpleSellAssistant(Market market)
        {
            Market = market;
        }

        public void SellAsset(AssetsOwner seller, Commodity commodity, int ammount)
        {
            var lastTurnPrice = Market.SalesHistory.GetActualPrice(commodity);
            var priceToOffer = lastTurnPrice * (1 + SELL_PREMIUM);

            Market.AddSellOffer(new SellOfferWithDiscountPerTurn(
                commodity: commodity,
                price: priceToOffer,
                ammount: ammount,
                seller: seller,
                discount: SELL_DISCOUNT_PER_TURN));
        }

        public Market Market { get; private set; }
    }
}
