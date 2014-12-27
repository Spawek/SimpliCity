using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    class Market
    {
        public string name;
        public IDictionary<Commodity, decimal> lastPrices = new Dictionary<Commodity, decimal>();
        public List<SellOffer> sellOffers = new List<SellOffer>();

        //public decimal PriceBuyOffer(Commodity commodity, int ammount) // TODO: TEST IT!
        //{
        //    int currNeeded = ammount;
        //    var matchingOffers = sellOffers.Where(x => x.commodity == commodity);

        //    if (matchingOffers.Sum(x => x.ammount) < ammount)
        //        throw new ApplicationException();

        //    while (currNeeded > 0)
        //    {
        //        var minPrice = matchingOffers.Min(x => x.price);    
        //        var minPriceOffers = matchingOffers.Where(x => x.price == minPrice);

        //        if (minPriceOffers.Sum)
        //    }
        //}
    }
}
