using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class SimpleCitizen : Citizen
    {
        public SimpleCitizen(string name, City city, decimal money, SellAssistant sellAssistant)
            : base(name, city, money)
        {
            SellAssistant = sellAssistant;
        }

        private const decimal MAX_MONEY_SPENT_PER_TURN = 0.1M;
        private const string COMMODITY_TO_BUY = "grain";

        public override void BuyAndConsume()
        {
            decimal maxSpendings = money * MAX_MONEY_SPENT_PER_TURN;
            var grain = City.GetCommodity(COMMODITY_TO_BUY);
            var market = City.markets[0]; //HARDCODED

            int ammountToBuy = market.CalcMaxAmmountAvailableToBuyForGivenPrice(
                grain, maxSpendings);

            market.MakeBuyOffer(grain, ammountToBuy, this);
        }

        protected override void SellWork()
        {
            var work = SpecialCommodities.Work;
            SellAssistant.SellAsset(this, work, this.commodityStorage[work]);
        }

        private SellAssistant SellAssistant { get; set; }
    }
}
