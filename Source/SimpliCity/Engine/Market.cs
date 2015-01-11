﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Market
    {
        public string Name { get; private set; }

        public Market(string name)
        {
            Name = name;
        }

        // when company gives sell offer to something, it is actually moved to market
        public void AddSellOffer(SellOffer offer)
        {
            if (!offer.Seller.commodities.ContainsKey(offer.Commodity))
                throw new ApplicationException();
            if (offer.Seller.commodities[offer.Commodity] < offer.Ammount)
                throw new ApplicationException();

            sellOffers.Add(offer);
            offer.Seller.commodities[offer.Commodity] -= offer.Ammount;

            Console.WriteLine(String.Format("On market {0} {1} wants to sell {2} of {3} for {4} per one",
                Name, offer.Seller.Name, offer.Ammount.ToString(), offer.Commodity.Name, offer.Price.ToString()));
        }

        // it's O(n^2) - can be optimized easily
        public decimal PriceBuyOffer(Commodity commodity, int ammount)
        {
            int currNeeded = ammount;

            var matchingOffers = sellOffers.Where(x => x.Commodity == commodity).ToList();

            if (matchingOffers.Sum(x => x.Ammount) < ammount)
                throw new ApplicationException();

            decimal currPrice = 0;
            while (currNeeded > 0)
            {
                var minPriceOffer = matchingOffers.MinElement(x => x.Price);
                if (minPriceOffer.Ammount < currNeeded)
                {
                    currPrice += minPriceOffer.Ammount * minPriceOffer.Price;
                    currNeeded -= minPriceOffer.Ammount;
                }
                else
                {
                    currPrice += currNeeded * minPriceOffer.Price;
                    currNeeded -= currNeeded;
                }
                matchingOffers.Remove(minPriceOffer);
            }

            Console.WriteLine("Market have {0} priced {1} of {2} to {3}",
                Name, ammount.ToString(), commodity.Name, currPrice);

            return currPrice;
        }

        public int GetCommodityAvailable(Commodity commodity)
        {
            return sellOffers.Where(x => x.Commodity == commodity).Sum(x => x.Ammount);
        }

        public void MakeBuyOffer(Commodity commodity, int ammount, AssetsOwner buyer)
        {
            int currNeeded = ammount;

            var matchingOffers = sellOffers.Where(x => x.Commodity == commodity).ToList();

            if (matchingOffers.Sum(x => x.Ammount) < ammount)
                throw new ApplicationException();

            while (currNeeded > 0)
            {
                var minPriceOffer = matchingOffers.MinElement(x => x.Price);
                if (minPriceOffer.Ammount <= currNeeded)
                {
                    minPriceOffer.FinalizeOffer(buyer);
                    matchingOffers.Remove(minPriceOffer);
                    sellOffers.Remove(minPriceOffer);
                    currNeeded -= minPriceOffer.Ammount;
                }
                else
                {
                    minPriceOffer.FinalizeOfferPartially(buyer, currNeeded);
                    currNeeded -= currNeeded;
                }
            }
        }

        public int CalcMaxAmmountAvailableToBuyForGivenPrice( //CAN BE OPTIMIZED EASILY
            Commodity commodity, decimal maxPrice)
        {
            if (sellOffers.Where(x => x.Commodity == commodity).Sum(x => x.Ammount * x.Price) < maxPrice)
                return sellOffers.Where(x => x.Commodity == commodity).Sum(x => x.Ammount);

            decimal currPrice = 0M;
            int currAmmount = 0;
            while (currPrice < maxPrice)
            {
                currAmmount++;
                currPrice = PriceBuyOffer(commodity, currAmmount);
            }
            int ammountToBuy = currAmmount - 1;
            return ammountToBuy;
        }

        private List<SellOffer> sellOffers = new List<SellOffer>();
    }
}
