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
            int maxProductionPossible = GetMaxProductionSize(Company, production);
            int productionSize = Math.Min(maxProductionPossible, wantedProductionSize);

            BuyGoodsForProduction(production, productionSize);
            Produce(production, productionSize);
        }

        private void Produce(Technology production, int productionSize)
        {
            production.Produce(Company, productionSize);
        }

        private void BuyGoodsForProduction(Technology production, int productionSize)
        {
            foreach (var item in production.input)
            {
                int countNeeded = item.Value * productionSize - Company.commodities[item.Key];
                Company.Market.MakeBuyOffer(item.Key, countNeeded, Company);
            }
        }

        private static int GetMaxProductionSize(Company company, Technology production)
        {
            var market = company.Market;
            int maxProductionPossible = int.MaxValue;
            foreach (var c in production.input)
            {
                var currCommodityAvailable = market.GetCommodityAvailable(c.Key);
                if (currCommodityAvailable != 0)
                {
                    maxProductionPossible = Math.Min(maxProductionPossible, c.Value / currCommodityAvailable);
                }
            }

            maxProductionPossible = Math.Min(maxProductionPossible, company.Employees.Count / production.labourNeeded);
            return maxProductionPossible;
        }

        public override void SellAssets()
        {
            foreach (Commodity commodity in Company.commodities.Keys.ToList())  // .ToList() is needed as list dictionary is being modified in here
            {
                int ammountPossessed = Company.commodities[commodity];
                int ammountToSell = 0;
                if (production.input.ContainsKey(commodity))
                {
                    int ammountNeeded = production.input[commodity] * wantedProductionSize;
                    ammountToSell = ammountPossessed - ammountNeeded;
                }
                else
                {
                    ammountToSell = ammountPossessed;
                }

                if (ammountToSell != 0)
                {
                    Company.Market.addSellOffer(new SellOffer(
                        _commodity: commodity,
                        _price: 4, //TODO: pricing model
                        _ammount: ammountToSell,
                        _seller: Company));
                }
            }
        }

        public override void PayDividend()
        {
            decimal moneyToGive = Company.money * 0.1M;
            foreach (var item in Company.Shareholders)
            {
                decimal currDividend = moneyToGive * item.Value;
                Company.TransferMoney(item.Key, currDividend);
            }
        }

        private Technology production;
        private int wantedProductionSize;
    }
}
