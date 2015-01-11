using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Technology
    {
        public string name;
        public int labourNeeded;
        public IDictionary<Commodity, int> input;
        public IDictionary<Commodity, int> output;

        public void Produce(Company c, int times)
        {
            Console.WriteLine("Company {0} uses {1} technology {2} times",
                c.Name, name, times);
            UseResources(c, times);
            GiveOutput(c, times);
        }

        private void GiveOutput(Company c, int times)
        {
            foreach (var item in output)
            {
                int commodityProduced = item.Value * times;

                if (!c.commodities.ContainsKey(item.Key))
                {
                    c.commodities.Add(item.Key, 0);
                }
                c.commodities[item.Key] += commodityProduced;
            }
        }

        private void UseResources(Company c, int times)
        {
            foreach (var item in input)
            {
                int commodityNeeded = item.Value * times;

                if (!c.commodities.ContainsKey(item.Key))
                    throw new ApplicationException();
                if (c.commodities[item.Key] < commodityNeeded)
                    throw new ApplicationException();

                c.commodities[item.Key] -= commodityNeeded;
            }
        }
    }
}
