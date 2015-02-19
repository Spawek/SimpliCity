using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    // IDEA: can be generic (but is it needed?) 
    public class CommodityStorage : IEnumerable<KeyValuePair<Commodity, int>>
    {
        public void Deposit(Commodity commodity, int ammount)
        {
            if (!commodities.ContainsKey(commodity))
                commodities.Add(commodity, ammount);
            else
                commodities[commodity] += ammount;
        }

        public void Withdraw(Commodity commodity, int ammount)
        {
            if (ammount == 0) throw new ApplicationException();
            if (!commodities.ContainsKey(commodity)) throw new ApplicationException();
            if (commodities[commodity] < ammount) throw new ApplicationException();

            commodities[commodity] -= ammount;
        }

        public int this[Commodity commodity]
        {
            get
            {
                if (!commodities.ContainsKey(commodity))
                    return 0;
                return commodities[commodity];
            }
        }

        private IDictionary<Commodity, int> commodities = new Dictionary<Commodity, int>();

        public IEnumerator<KeyValuePair<Commodity, int>> GetEnumerator()
        {
            return commodities.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return commodities.GetEnumerator();
        }
    }
}
