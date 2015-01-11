using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Need
    {
        public Need(string name, Func<int, double> enjoymentCurveFoo)
        {
            Name = name;
            EnjoymentCurveFoo = enjoymentCurveFoo;
        }

        public string Name;
        public double EnjoymentCurve(int ammount)
        {
            return EnjoymentCurveFoo(ammount);
        }

        public Func<int, double> EnjoymentCurveFoo { get; private set; }
    }
}
