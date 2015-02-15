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


        //TODO: add test for checking if technology throws when there are no employees available

        //NO IDEA HOW TO TEST IT - ITS COMPANY WHO MOVES EMPLOYES TO FREE ONES (mby use mocks?)
        //[TestMethod]
        //public void ProduceUsesEmployees()
        //{
        //    const int TIMES_PRODUCED = 2;
        //    const int EMPLOYEES_NEEDED_ON_PEOPLE_GRAIN_FARM = 2;

        //    var labourPlace = new Technology("testTechnology", EMPLOYEES_NEEDED_ON_PEOPLE_GRAIN_FARM,
        //        new Dictionary<Commodity, int>(), new Dictionary<Commodity, int>());

        //    Citizen a = new SimpleCitizen("a", null, 0);
        //    Citizen b = new SimpleCitizen("b", null, 0);
        //    Citizen c = new SimpleCitizen("c", null, 0);
        //    Citizen d = new SimpleCitizen("d", null, 0);
        //    Citizen e = new SimpleCitizen("e", null, 0);


        //    company.Hire(a, 0);
        //    company.Hire(b, 0);
        //    company.Hire(c, 0);
        //    company.Hire(d, 0);
        //    company.Hire(e, 0);

        //    labourPlace.Produce(company, TIMES_PRODUCED);

        //    Assert.AreEqual(1, company.FreeEmployees.Count);
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
