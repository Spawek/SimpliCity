using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    class Program
    {
        static void Main(string[] args)
        {
            City simpliCity = new City();

            int turnNo = 1;
            while (true)
            {
                Console.WriteLine("TURN {0} BEGINS!", turnNo);
                MakeTurn(simpliCity);
                Console.WriteLine("TURN {0} ENDS!", turnNo);
                turnNo++;
            }
        }

        private static void MakeTurn(City simpliCity)
        {
            foreach (var c in simpliCity.companies)
            {
                c.BuyAndProduce();
                c.SellAssets();
                c.PayDidivend();
            }

            foreach (var c in simpliCity.citizens)
            {
                c.BuyAndConsume();
            }
        }
    }
}
