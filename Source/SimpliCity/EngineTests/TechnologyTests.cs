using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Engine;

namespace EngineTests
{
    [TestClass]
    public class TechnologyTests
    {
        Company company = new Company("testCompany", null, null, null);
        Commodity inputGrain;
        Commodity outputGrain; // its not the same as input grain for test purposes
        const int GRAIN_NEEDED_TO_FARM = 2;
        const int GRAIN_GATHERED_FROM_FARM = 5;
        Technology grainFarm;

        public TechnologyTests()
        {
            inputGrain = new Commodity("input grain", null);
            outputGrain = new Commodity("output grain", null);
            grainFarm = new Technology("testTechnology",
                new Dictionary<Commodity, int>() { { inputGrain, GRAIN_NEEDED_TO_FARM } },
                new Dictionary<Commodity, int>() { { outputGrain, GRAIN_GATHERED_FROM_FARM } });
        }

        [TestMethod]
        public void ProduceTakesInput()
        {
            const int INITIAL_GRAIN = 5;
            const int TIMES_PRODUCED = 2;
            company.commodityStorage.Deposit(inputGrain, INITIAL_GRAIN);
            grainFarm.Produce(company, TIMES_PRODUCED);

            int expectedGrainLeft = INITIAL_GRAIN - TIMES_PRODUCED * GRAIN_NEEDED_TO_FARM;
            Assert.AreEqual(expectedGrainLeft, company.commodityStorage[inputGrain]);
        }


        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void produceneedsinput()
        {
            const int initial_grain = 3;
            const int times_produced = 2;
            company.commodityStorage.Deposit(inputGrain, initial_grain);

            grainFarm.Produce(company, times_produced);
        }

        [TestMethod]
        public void ProeducesGivesOutput()
        {
            const int INITIAL_GRAIN = 5;
            const int TIMES_PRODUCED = 2;
            company.commodityStorage.Deposit(inputGrain, INITIAL_GRAIN);
            grainFarm.Produce(company, TIMES_PRODUCED);

            int expectedOuputGrain = TIMES_PRODUCED * GRAIN_GATHERED_FROM_FARM;
            Assert.AreEqual(expectedOuputGrain, company.commodityStorage[outputGrain]);
        }
    }
}
