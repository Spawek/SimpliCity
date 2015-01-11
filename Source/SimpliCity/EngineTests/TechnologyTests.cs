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
        const int EMPLOYEES_NEEDED_ON_GRAIN_FARM = 2;
        const int GRAIN_NEEDED_TO_FARM = 2;
        const int GRAIN_GATHERED_FROM_FARM = 5;
        Technology grainFarm;

        public TechnologyTests()
        {
            inputGrain = new Commodity("input grain", null);
            outputGrain = new Commodity("output grain", null);
            grainFarm = new Technology("testTechnology", EMPLOYEES_NEEDED_ON_GRAIN_FARM,
                new Dictionary<Commodity, int>() { { inputGrain, GRAIN_NEEDED_TO_FARM } },
                new Dictionary<Commodity, int>() { { outputGrain, GRAIN_GATHERED_FROM_FARM } });
        }

        [TestMethod]
        public void ProduceTakesInput()
        {
            const int INITIAL_GRAIN = 5;
            const int TIMES_PRODUCED = 2;
            company.commodities.Add(inputGrain, INITIAL_GRAIN);
            grainFarm.Produce(company, TIMES_PRODUCED);

            int expectedGrainLeft = INITIAL_GRAIN - TIMES_PRODUCED * GRAIN_NEEDED_TO_FARM;
            Assert.AreEqual(expectedGrainLeft, company.commodities[inputGrain]);
        }

        //TODO - it should throw - i dont remember how to check it and i dont have internet
        //[TestMethod]
        //public void ProduceNeedsInput()
        //{
        //    const int INITIAL_GRAIN = 3;
        //    const int TIMES_PRODUCED = 2;
        //    company.commodities.Add(inputGrain, INITIAL_GRAIN);
            
        //    grainFarm.Produce(company, TIMES_PRODUCED);


        //}

        [TestMethod]
        public void ProeducesGivesOutput()
        {
            
            const int INITIAL_GRAIN = 5;
            const int TIMES_PRODUCED = 2;
            company.commodities.Add(inputGrain, INITIAL_GRAIN);
            grainFarm.Produce(company, TIMES_PRODUCED);

            int expectedOuputGrain = TIMES_PRODUCED * GRAIN_GATHERED_FROM_FARM;
            Assert.AreEqual(expectedOuputGrain, company.commodities[outputGrain]);
        }
    }
}
