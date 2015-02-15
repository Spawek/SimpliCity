using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    /// <summary>
    /// this class is a singleton with 1 owner (the one who register its counter)
    /// only the owner can increment the counter
    /// everyone can get current turn
    /// </summary>
    public class TurnCounter
    {
        public void IncrementCounter()
        {
            currentTurn++;
            Console.WriteLine("TURN {0} BEGINS!", TurnCounter.Now);
        }

        public static void RegisterCounter(TurnCounter turnCounter)
        {
            if (instance != null)
                throw new ApplicationException("You cannot register 2 counters!");

            instance = turnCounter;
        }

        /// <summary>
        /// Mostly for test purposes
        /// </summary>
        /// <param name="turnCounter">
        /// just for safety - if u dont know what is registred, you cannot unregister
        /// </param>
        public static void UnregisterCounter(TurnCounter turnCounter)
        {
            if (instance != turnCounter)
                throw new ApplicationException("Your counter is not registred!");
            
            instance = null;
        }

        public static int Now
        {
            get { return instance.currentTurn; }
        }

        private static TurnCounter instance = null;
        private int currentTurn = 1;
    }
}
