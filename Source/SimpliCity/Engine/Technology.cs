using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    /*
     * machines can be part of input - they dont need separate field - e.g.
     *      cow -> cow + milk
     */
    public class Technology
    {
        public Technology(string name, IDictionary<Commodity, int> input,
            IDictionary<Commodity, int> output)
        {
            Name = name;
            Input = input;
            Output = output;
        }

        public string Name { get; private set; }
        public IDictionary<Commodity, int> Input { get; private set; }
        public IDictionary<Commodity, int> Output { get; private set; }

        public void Produce(Company company, int times)
        {
            Console.WriteLine("Company {0} uses {1} technology {2} times",
                company.Name, Name, times);
            UseCommodities(company, times);
            GiveOutput(company, times);
        }

        private void GiveOutput(Company company, int times)
        {
            foreach (var item in Output)
            {
                int commodityProduced = item.Value * times;
                company.commodityStorage.Deposit(item.Key, commodityProduced);
            }
        }

        private void UseCommodities(Company company, int times)
        {
            foreach (var item in Input)
            {
                int commodityNeeded = item.Value * times;
                company.commodityStorage.Withdraw(item.Key, commodityNeeded);
            }
        }
    }
}
