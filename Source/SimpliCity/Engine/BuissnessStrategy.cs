using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine
{
    public abstract class BuissnessStrategy
    {
        public BuissnessStrategy(Company c)
        {
            company = c;
        }

        protected Company company;

        public abstract void BuyAndProduce();
        public abstract void SellAssets();
        public abstract void PayDividend();
    }
}
