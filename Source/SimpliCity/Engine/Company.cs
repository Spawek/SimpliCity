using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Company : AssetsOwner
    {
        public Company(string _name, City _city, Market _market, IDictionary<AssetsOwner, decimal> _shareholders)
            : base (0)
        {
            name_ = _name;
            City = _city;
            Market = _market;
            Shareholders = _shareholders;
        }

        private string name_;
        public override string Name { get { return name_; } }
        public IDictionary<AssetsOwner, decimal> Shareholders { get; private set; }
        public City City { get; private set; }
        public Market Market { get; private set; }

        public BuissnessStrategy strategy;
        public void BuyAndProduce()
        {
            strategy.BuyAndProduceBase();
        }

        public void SellAssets()
        {
            strategy.SellAssetsBase();
        }

        public void PayDidivend()
        {
            strategy.PayDividendBase();
        }
    }
}
