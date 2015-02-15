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

        public SingleProductionStrategy(Company company, Technology technology,
            SellAssistant sellAssistant)
            : base(company)
        {
            Production = technology;
            WantedProductionSize = DEFAULT_PRODUCTION_SIZE;
            SellAssistant = sellAssistant;
        }

        protected override void BuyAndProduce()
        {
            int maxProductionPossible = GetMaxProductionSize(Company, Production);
            int productionSize = Math.Min(maxProductionPossible, WantedProductionSize);

            var expectedProfit = GetExpectedProfit(productionSize);
            if (!expectedProfit.HasValue || expectedProfit.Value > 0.0M)
            {
                BuyGoodsForProduction(Production, productionSize);
                Produce(Production, productionSize);
            }
            else
            {
                Console.WriteLine(String.Format(
                    "Company: {0} doesn't produce as it is not profitable", Company.Name));
            }
        }

        /// <summary></summary>
        /// <param name="commodities"></param>
        /// <returns>null means it cannot calculate expected value</returns>
        private decimal? PriceCommoditiesByHistory(IDictionary<Commodity, int> commodities)
        {
            var salesHistory = Company.Market.SalesHistory;

            var intputs = commodities.Select(
                x => salesHistory.GetAverageSellPrice(x.Key, TurnCounter.Now) * x.Value).ToList();

            if (intputs.Any(x => !x.HasValue))
                return null;

            return intputs.Sum(x => x.Value);
        }

        /// <summary></summary>
        /// <param name="commodities"></param>
        /// <returns>null means it cannot calculate expected price</returns>
        private static decimal? GetMarketPrice(IDictionary<Commodity, int> commodities, Market market)
        {
            var inputs = commodities.Select(
                x => market.PriceBuyOffer(x.Key, x.Value));

            if (inputs.Any(x => !x.HasValue))
                return null;

            return inputs.Sum(x => x.Value);
        }

        /// <summary></summary>
        /// <param name="productionSize"></param>
        /// <returns>null means it cannot calculate expected profit</returns>
        private decimal? GetExpectedProfit(int productionSize)
        {
            var totalInputCost = GetMarketPrice(Production.Input, Company.Market);
            var totalExpectedOutputProfit = PriceCommoditiesByHistory(Production.Output);
            if (!totalExpectedOutputProfit.HasValue)
                return null;

            return totalExpectedOutputProfit - totalInputCost;
        }

        private void Produce(Technology production, int productionSize)
        {
            production.Produce(Company, productionSize);
        }

        private void BuyGoodsForProduction(Technology production, int productionSize)
        {
            foreach (var item in production.Input)
            {
                var storedCommodities = Company.commodities.ContainsKey(item.Key) ? Company.commodities[item.Key] : 0;
                var countNeeded = item.Value * productionSize - storedCommodities;
                if (countNeeded > 0)
                {
                    Company.Market.MakeBuyOffer(item.Key, countNeeded, Company);
                }
            }
        }

        private static IDictionary<Commodity, int> GetCommoditiesNeededToBuyForProduction(
            Technology technology, int productionSize, IDictionary<Commodity, int> storedCommodities)
        {
            return technology.Input.ToDictionary(
                x => x.Key,
                x => x.Value * productionSize - (storedCommodities.ContainsKey(x.Key) ? storedCommodities[x.Key] : 0));
        }

        // TODO: this needs to be rewritten, just made it work (but slowly) for now
        private static int GetMaxProductionSize(Company company, Technology production)
        {
            var market = company.Market;
            int maxProductionPossible = int.MaxValue;
            foreach (var c in production.Input)
            {
                var commodityAvailableOnMarket = market.GetCommodityAvailable(c.Key);
                var storedCommodities = company.commodities.ContainsKey(c.Key) ? company.commodities[c.Key] : 0;
                var commodityAvailable = commodityAvailableOnMarket + storedCommodities;
                maxProductionPossible = Math.Min(maxProductionPossible, commodityAvailable / c.Value);
            }

            // this 'algorithm' is really slow - TODO: find better one
            while (GetMarketPrice(
                GetCommoditiesNeededToBuyForProduction(production, maxProductionPossible, company.commodities), 
                    company.Market) > company.money)
            {
                maxProductionPossible--;
            }
            
            return maxProductionPossible;
        }

        protected override void SellAssets()
        {
            foreach (Commodity commodity in Company.commodities.Keys.ToList())  // .ToList() is needed as list dictionary is being modified in here
            {
                int ammountPossessed = Company.commodities[commodity];
                int ammountToSell = 0;
                if (Production.Input.ContainsKey(commodity) && ammountPossessed > ammountToSell)
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
                    SellAssistant.SellAsset(Company, commodity, ammountToSell);
                }
            }
        }

        protected override void PayDividend()
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
        public SellAssistant SellAssistant { get; private set; }
    }
}
