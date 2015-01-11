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
        }

        public static void RegisterCounter(TurnCounter turnCounter)
        {
            if (instance != null)
                throw new ApplicationException();

            instance = turnCounter;
        }

        public static int Now
        {
            get { return instance.currentTurn; }
        }

        private static TurnCounter instance = null;
        private int currentTurn = 1;
    }
}
