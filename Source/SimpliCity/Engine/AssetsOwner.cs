using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    abstract public class AssetsOwner
    {
        public decimal money;
        public IDictionary<Company, decimal> companyShares = new Dictionary<Company, decimal>();
        public IDictionary<Commodity, int> commodities = new Dictionary<Commodity, int>();
    }
}
