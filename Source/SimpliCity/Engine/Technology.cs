using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    class Technology
    {
        public string name;
        public int labourNeeded;
        public IDictionary<Commodity, int> input;
        public IDictionary<Commodity, int> output;
    }
}
