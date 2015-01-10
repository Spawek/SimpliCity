using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public abstract class Citizen : AssetsOwner
    {
        public Citizen(string n, City c, decimal m)
        {
            name = n;
            city = c;
            money = m;
        }

        public string name;
        public City city;
        public Company job = null;

        public abstract void BuyAndConsume();
    }
}
