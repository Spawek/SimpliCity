using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Commodity
    {
        public Commodity(string name, Need need)
        {
            Name = name;
            Need = need;
        }

        public string Name { get; private set; }
        public Need Need { get; private set; }
    }
}
