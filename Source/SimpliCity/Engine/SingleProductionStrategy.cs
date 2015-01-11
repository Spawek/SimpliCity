using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class SingleProductionStrategy : BuissnessStrategy
    {
        private const int DEFAULT_PRODUCTION_SIZE = 1;

        public SingleProductionStrategy(Company company, Technology technology)
            : base(company)
        {
            Production = technology;
            WantedProductionSize = DEFAULT_PRODUCTION_SIZE;
        }

        public override void BuyAndProduce()
        {
            int maxProductionPossible = GetMaxProductionSize(Company, Production);
            int productionSize = Math.Min(maxProductionPossible, WantedProductionSize);

            BuyGoodsForProduction(Production, productionSize);
            Produce(Production, productionSize);
        }

        private void Produce(Technology production, int productionSize)
        {
            production.Produce(Company, productionSize);
        }

        private void BuyGoodsForProduction(Technology production, int productionSize)
        {
            foreach (var item in production.Input)
            {
                int countNeeded = item.Value * productionSize - Company.commodities[item.Key];
                Company.Market.MakeBuyOffer(item.Key, countNeeded, Company);
            }
        }

        private static int GetMaxProductionSize(Company company, Technology production)
        {
            var market = company.Market;
            int maxProductionPossible = int.MaxValue;
            foreach (var c in production.Input)
            {
                var currCommodityAvailable = market.GetCommodityAvailable(c.Key);
                if (currCommodityAvailable != 0)
                {
                    maxProductionPossible = Math.Min(maxProductionPossible, c.Value / currCommodityAvailable);
                }
            }

            maxProductionPossible = Math.Min(maxProductionPossible, company.Employees.Count / production.LabourNeeded);
            return maxProductionPossible;
        }

        public override void SellAssets()
        {
            foreach (Commodity commodity in Company.commodities.Keys.ToList())  // .ToList() is needed as list dictionary is being modified in here
            {
                int ammountPossessed = Company.commodities[commodity];
                int ammountToSell = 0;
                if (Production.Input.ContainsKey(commodity))
                {
                    int ammountNeeded = Production.Input[commodity] * WantedProductionSize;
                    ammountToSell = ammountPossessed - ammountNeeded;
                }
                else
                {
                    ammountToSell = ammountPossessed;
                }

                if (ammountToSell != 0)
                {
                    Company.Market.addSellOffer(new SellOfferWithDiscountPerTurn(
                        commodity: commodity,
                        price: 4, //TODO: pricing model
                        ammount: ammountToSell,
                        seller: Company,
                        discount: 0.05));
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

        public Technology Production { get; private set; }
        public int WantedProductionSize { get; private set; }
    }
}
