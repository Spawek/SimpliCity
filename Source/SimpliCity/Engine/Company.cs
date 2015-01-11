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
            Name = _name;
            City = _city;
            Market = _market;
            Shareholders = _shareholders;
            Employees = new Dictionary<Citizen, decimal>();
        }

        public string Name { get; private set; }
        public IDictionary<Citizen, decimal> Employees { get; private set; }
        public IDictionary<AssetsOwner, decimal> Shareholders { get; private set; }
        public City City { get; private set; }
        public Market Market { get; private set; }

        public void Hire(Citizen employee, decimal salary)
        {
            if (employee.Job != null)
                throw new ApplicationException();

            Employees.Add(employee, salary);
            employee.Job = this;
        }

        public void Fire(Citizen employee)
        {
            if (employee.Job != this)
                throw new ApplicationException();
            if (!Employees.ContainsKey(employee))
                throw new ApplicationException();

            employee.Job = null;
            Employees.Remove(employee);
        }

        public BuissnessStrategy strategy;
        public void BuyAndProduce()
        {
            PayWages();
            strategy.BuyAndProduce();
        }

        private void PayWages()
        {
            foreach (var employee in Employees)
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
