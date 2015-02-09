using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public abstract class Citizen : AssetsOwner
    {
        private const int WORK_CREATED_PER_DAY = 1;

        public Citizen(string name, City city, decimal momey)
            : base(momey)
        {
            name_ = name;
            City = city;
        }

        private string name_;
        public override string Name { get { return name_; } }

        public City City { get; private set; }
        public Company Job = null;

        public abstract void BuyAndConsume();
        protected abstract void SellWork();

        public void CreateAndSellWork()
        {
            var work = SpecialCommodities.Work;
            if (!this.commodities.ContainsKey(work))
            {
                this.commodities.Add(work, 0);
            }
            this.commodities[work] += WORK_CREATED_PER_DAY;

            SellWork();
        }
    }
}
