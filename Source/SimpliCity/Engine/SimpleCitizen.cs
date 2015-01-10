using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    class SimpleCitizen : Citizen
    {
        public SimpleCitizen(string name, City city, decimal money)
            : base(name, city, money)
        {
        }

        private const decimal MAX_MONEY_SPENT_PER_TURN = 0.1M;
        private const string COMMODITY_TO_BUY = "grain";

        public override void BuyAndConsume()
        {
            decimal maxSpendings = money * MAX_MONEY_SPENT_PER_TURN;
            var grain = city.GetCommodity(COMMODITY_TO_BUY);
            var market = city.markets[0]; //HARDCODED

            int ammountToBuy = market.CalcMaxAmmountAvailableToBuyForGivenPrice(
                grain, maxSpendings);

            market.MakeBuyOffer(grain, ammountToBuy, this);
        }
    }
}
