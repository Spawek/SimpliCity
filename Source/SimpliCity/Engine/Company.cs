using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    // TODO:
    // - if last buisness production was successfull (earned money) - repeat?
    public class Company : AssetsOwner
    {
        public string name;
        public IDictionary<Citizen, decimal> employees = new Dictionary<Citizen, decimal>();
        public IDictionary<AssetsOwner, decimal> shareholders = new Dictionary<AssetsOwner, decimal>();
        public City city;
        public Market market;

        public void Hire(Citizen employee, decimal salary)
        {
            employees.Add(employee, salary);
            employee.job = this;
        }

        private BuissnessStrategy strategy;
        public void BuyAndProduce()
        {
            strategy.BuyAndProduce();
        }

        public void SellAssets()
        {
            strategy.SellAssets();
        }

        public void PayDidivend()
        {
            strategy.PayDividend();
        }

        //private KeyValuePair<Technology, int>? GetBestProduction()
        //{
        //    var bestProduction = new KeyValuePair<Technology, int>();
        //    decimal bestPredictedRevenue = decimal.MinValue;
        //    foreach (Technology tech in city.commonTechnologies)
        //    {
        //        int timesPossible = CalculateHowManyTimesProductionIsPossible(tech);
        //        if (timesPossible <= 0) continue;
        //        decimal predictedRevenue = CalculatePredictedRevenue(tech, timesPossible);

        //        if (predictedRevenue > bestPredictedRevenue)
        //        {
        //            bestPredictedRevenue = predictedRevenue;
        //            bestProduction = new KeyValuePair<Technology, int>(tech, timesPossible);
        //        }
        //    }

        //    if (bestPredictedRevenue < 0)
        //    {
        //        return null;
        //    }

        //    return bestProduction;
        //}

        //private decimal CalculatePredictedRevenue(Technology tech, int timesPossible)
        //{
        //    throw new NotImplementedException();
        //}

        //private int CalculateHowManyTimesProductionIsPossible(Technology tech)
        //{
        //    int timesPossible = employees.Count / tech.labourNeeded;
        //    //foreach (var inputNeeded in tech.input)
        //    //{
        //    //    var currResource = inputNeeded.Key;
        //    //    var currAmmountNeeded = inputNeeded.Value;
        //    //    var ammountPossesed = commodities[currResource];
        //    //    var ammountAvailableToBuy = market.sellOffers.Where(x => x.commodity == currResource).Sum(x => x.ammount);
        //    //    var resourcesAvailableForTimesPossible = (ammountPossesed + ammountAvailableToBuy) / currAmmountNeeded;
        //    //    timesPossible = Math.Min(timesPossible, resourcesAvailableForTimesPossible);
        //    //}
        //    return timesPossible;
        //}

        ///// <returns>true if produced something</returns>
        ////public bool ProduceOnce()
        ////{
        ////    var bestProduction = GetBestProduction();
        ////    if (bestProduction == null)
        ////        return false;

        ////    Produce(bestProduction.Value.Key, bestProduction.Value.Value);
            
        ////}
        // /// <summary>
        // /// buy stuff and produce
        // /// </summary>
        // /// <param name="technology"></param>
        // /// <param name="ammount"></param>
        //private void Produce(Technology technology, int ammount)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Sell()
        //{
        //    throw new NotImplementedException();
        //}

        //public void PayDividend()
        //{
        //    throw new NotImplementedException();
        //}

        //BUISSNESSPAN IDEA - each production company got its buisnessplan - for 30 days i ll produce that using that tech and so on ....

    }
}
