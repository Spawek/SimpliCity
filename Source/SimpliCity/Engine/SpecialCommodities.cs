using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    static class SpecialCommodities
    {
        private static Commodity __work__ = new Commodity("work", null);
        public static Commodity Work { get{return __work__;} }
    }
}
