using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class SingleProductionStrategy : BuissnessStrategy
    {
        public SingleProductionStrategy(Company c, Technology t)
            : base(c)
        {
            production = t;
            wantedProductionSize = 1;
        }

        public override void BuyAndProduce()
        {
            int maxProductionPossible = GetMaxProductionSize(company, production);
            int productionSize = Math.Min(maxProductionPossible, wantedProductionSize);

            BuyGoodsForProduction(production, productionSize);
            Produce(production, productionSize);
        }

        private void Produce(Technology production, int productionSize)
        {
            production.Produce(company, productionSize);
        }

        private void BuyGoodsForProduction(Technology production, int productionSize)
        {
            foreach (var item in production.input)
            {
                int countNeeded = item.Value * productionSize - company.commodities[item.Key];
                company.market.MakeBuyOffer(item.Key, countNeeded, company);
            }
        }

        private static int GetMaxProductionSize(Company company, Technology production)
        {
            var market = company.market;
            int maxProductionPossible = int.MaxValue;
            foreach (var c in production.input)
            {
                var currCommodityAvailable = market.GetCommodityAvailable(c.Key);
                if (currCommodityAvailable != 0)
                {
                    maxProductionPossible = Math.Min(maxProductionPossible, c.Value / currCommodityAvailable);
                }
            }

            maxProductionPossible = Math.Min(maxProductionPossible, company.employees.Count / production.labourNeeded);
            return maxProductionPossible;
        }

        public override void SellAssets()
        {
            foreach (var item in company.commodities) //lista się modyfikuje i wywala tutaj - (w sumie to jest directory i nie ma on foreacha - no kurwa)
            {
                int ammountPossessed = company.commodities[item.Key];
                int ammountToSell = 0;
                if (production.input.ContainsKey(item.Key))
                {
                    int ammountNeeded = production.input[item.Key] * wantedProductionSize;
                    ammountToSell = ammountPossessed - ammountNeeded;
                }
                else
                {
                    ammountToSell = ammountPossessed;
                }

                if (ammountToSell != 0)
                {
                    company.market.addSellOffer(new SellOffer(
                        _commodity: item.Key,
                        _price: 4, //TODO: pricing model
                        _ammount: ammountToSell,
                        _seller: company));
                }
            }
        }

        public override void PayDividend()
        {
            decimal moneyToGive = company.money * 0.1M;
            foreach (var item in company.shareholders)
            {
                decimal currDividend = moneyToGive * item.Value;
                item.Key.money += currDividend;
                company.money -= currDividend;
            }
        }

        private Technology production;
        private int wantedProductionSize;
    }
}
