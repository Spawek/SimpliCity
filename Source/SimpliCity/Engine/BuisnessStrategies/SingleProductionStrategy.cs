﻿using System;
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

            if (expectedProfit > 0 && maxProductionPossible > WantedProductionSize)
            {
                int increasedProductionSize = productionSize + 1;
                var expectedIncreasedProductionProfit = GetExpectedProfit(increasedProductionSize);
                if (expectedIncreasedProductionProfit > expectedProfit)
                {
                    productionSize++;
                    WantedProductionSize++;
                }
            }

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
        private decimal? PriceCommoditiesByHistory(IEnumerable<KeyValuePair<Commodity, int>> commodities)
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
        private static decimal? GetMarketPrice(IEnumerable<KeyValuePair<Commodity, int>> commodities, Market market)
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
            if (productionSize > 0)
                production.Produce(Company, productionSize);
        }

        private void BuyGoodsForProduction(Technology production, int productionSize)
        {
            foreach (var item in production.Input)
            {

                var storedCommodities = Company.commodityStorage[item.Key];
                var countNeeded = item.Value * productionSize - storedCommodities;
                if (countNeeded > 0)
                {
                    Company.Market.MakeBuyOffer(item.Key, countNeeded, Company);
                }
            }
        }

        private static IDictionary<Commodity, int> GetCommoditiesNeededToBuyForProduction(
            Technology technology, int productionSize,
            CommodityStorage commodityStorage)
        {
            return technology.Input.ToDictionary(
                x => x.Key,
                x => x.Value * productionSize - commodityStorage[x.Key]);
        }

        // TODO: this needs to be rewritten, just made it work (but slowly) for now
        private static int GetMaxProductionSize(Company company, Technology production)
        {
            var market = company.Market;
            int maxProductionPossible = int.MaxValue;
            foreach (var c in production.Input)
            {
                var commodityAvailableOnMarket = market.GetCommodityAvailable(c.Key);
                var storedCommodities = company.commodityStorage[c.Key];
                var commodityAvailable = commodityAvailableOnMarket + storedCommodities;
                maxProductionPossible = Math.Min(maxProductionPossible, commodityAvailable / c.Value);
            }

            // this 'algorithm' is really slow - TODO: find better one
            while (GetMarketPrice(
                GetCommoditiesNeededToBuyForProduction(production, maxProductionPossible, company.commodityStorage), 
                    company.Market) > company.money)
            {
                maxProductionPossible--;
            }
            
            return maxProductionPossible;
        }

        // TODO: rewrite it 
        protected override void SellAssets()
        {
            foreach (Commodity commodity in Company.commodityStorage.Select(x => x.Key).ToList())
            {
                int ammountPossessed = Company.commodityStorage[commodity];
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
