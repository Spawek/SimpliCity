using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    /*
     * BUGS:
     *  - when during last day there was no commodity sold - last price is default price
     *  - there is some problem with pricing seen during simulation (price going down to 0) - test pricer module
     * 
     * IDEAS:
     *  - commodities can get lost when stored too long (work after 1 turn),
     *      then should be treated on market like sold for 0 (coz got wasted)
     *      (i dont know if its good idea) 
     * 
     * FOR NOW TODO:
     * - add commodity bundles - needed in SimpleProductionStrategy
     * - what is now sell offer - should be asset owner either
     * - optimize market - its really shitty
     * - job market
     * - needs
     * - companies creation
     * - sell prices changes
     *      - market history prices
     * - change city initial state to be in xml file (or mby do it later? - its easier to generate it from code)
     * - commodities spoiling (% per turn) (easier to implement (fast) than time to expire)
     * - work ammount citizens produce depends of how good they feel (mby some tech work, which appears only when they feel rly good?)
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
    class MainClass
    {
        static void Main(string[] args)
        {
            City simpliCity = new City();
            TurnCounter counter = new TurnCounter();
            TurnCounter.RegisterCounter(counter);

            while (true)
            {
                MakeTurn(simpliCity);
                counter.IncrementCounter();
            }
        }

        private static void MakeTurn(City simpliCity)
        {
            foreach (var c in simpliCity.citizens.OrderBy(x => rand.Next()))
            {
                c.CreateAndSellWork();
            }

            foreach (var c in simpliCity.companies.OrderBy(x => rand.Next()))
            {
                c.BuyAndProduce();
                c.SellAssets();
                c.PayDidivend();
            }

            foreach (var c in simpliCity.citizens.OrderBy(x => rand.Next()))
            {
                c.BuyAndConsume();
            }
        }

        private static Random rand = new Random(12344321);  // seed is given so app is deterministic
    }
}
