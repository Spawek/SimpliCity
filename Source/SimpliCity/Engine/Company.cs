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
            name = _name;
            city = _city;
            market = _market;
            shareholders = _shareholders;
        }

        public string name { get; private set; }
        public IDictionary<Citizen, decimal> employees = new Dictionary<Citizen, decimal>();
        public IDictionary<AssetsOwner, decimal> shareholders { get; private set; }
        public City city { get; private set; }
        public Market market { get; private set; }

        public void Hire(Citizen employee, decimal salary)
        {
            employees.Add(employee, salary);
            employee.job = this;
        }

        public BuissnessStrategy strategy;
        public void BuyAndProduce()
        {
            PayWages();
            strategy.BuyAndProduce();
        }

        private void PayWages()
        {
            foreach (var employee in employees)
            {
                this.TransferMoney(employee.Key, employee.Value);
            }
        }

        public void SellAssets()
        {
            strategy.SellAssets();
        }

        public void PayDidivend()
        {
            strategy.PayDividend();
        }
    }
}
