using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine
{
    public abstract class BuissnessStrategy
    {
        public BuissnessStrategy(Company company)
        {
            Company = company;
        }

        protected Company Company { get; private set; }

        protected abstract void BuyAndProduce();
        protected abstract void SellAssets();
        protected abstract void PayDividend();

        public void BuyAndProduceBase()
        {
            BuyAndProduce();
        }

        public void SellAssetsBase()
        {
            SellAssets();
        }

        public void PayDividendBase()
        {
            Console.WriteLine(String.Format("Company {0} pays dividends: ", Company.Name));
            PayDividend();
            Console.WriteLine(String.Format("Company {0} finished paying dividends", Company.Name));
        }
    }
}
