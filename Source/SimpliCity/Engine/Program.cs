using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    /*
     * FOR NOW TODO:
     * - job market
     * - needs
     * - companies creation
     * - sell prices changes
     *      - market history prices
     * 
     * 
     * TODO:
     *  - add government
     *      - taxes
     *      - money priting / destroying
     *      - social system
     *  - technologing evolution (research?)
     *  - machines needed for production
     *  - currencies
     *  - banks
     *  - children
     *  - death
     *  - bancrucy
     *  - trade between markets
     *  - transport price
     *  - employees skills
     *  - company management? (using time for changing how company works)
     */
    class Program
    {
        static void Main(string[] args)
        {
            City simpliCity = new City();
            TurnCounter counter = new TurnCounter();
            TurnCounter.RegisterCounter(counter);

            while (true)
            {
                Console.WriteLine("TURN {0} BEGINS!", TurnCounter.Now);
                MakeTurn(simpliCity);
                Console.WriteLine("TURN {0} ENDS!", TurnCounter.Now);
                counter.IncrementCounter();
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
