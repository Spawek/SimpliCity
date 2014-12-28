using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine
{
    public class BuissnessStrategy
    {
        private Company company;

        public BuissnessStrategy(Company c)
        {
            company = c;
        }

        public void BuyAndProduce()
        {
            throw new NotImplementedException();
        }

        public void SellAssets()
        {
            throw new NotImplementedException();
        }

        public void PayDividend()
        {
            throw new NotImplementedException();
        }
    }
}
