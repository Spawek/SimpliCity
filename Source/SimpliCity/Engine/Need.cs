using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    class Need
    {
        public string name;
        public double EnjoymentCurve(int ammount)
        {
            return enjoymentCurveFoo(ammount);
        }

        public Func<int, double> enjoymentCurveFoo;
    }
}
