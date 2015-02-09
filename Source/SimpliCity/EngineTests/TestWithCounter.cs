using Engine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineTests
{
    public abstract class TestWithTimeCounter
    {
        public TurnCounter counter;

        [TestInitialize]
        public void SetUp()
        {
            counter = new TurnCounter();
            TurnCounter.RegisterCounter(counter);
        }

        [TestCleanup]
        public void TeadDown()
        {
            TurnCounter.UnregisterCounter(counter);
        }
    }
}
