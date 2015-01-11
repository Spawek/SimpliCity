using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public abstract class Citizen : AssetsOwner
    {
        public Citizen(string name, City city, decimal momey)
            : base(momey)
        {
            name_ = name;
            City = city;
        }

        private string name_;
        public City City { get; private set; }
        public Company Job = null;

        public abstract void BuyAndConsume();

        public override string Name { get { return name_; } }
    }
}
