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
            Name = name;
            City = city;
        }

        public string Name { get; private set; }
        public City City { get; private set; }
        public Company Job = null;

        public abstract void BuyAndConsume();
    }
}
