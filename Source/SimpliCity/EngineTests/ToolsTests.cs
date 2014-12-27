using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Engine;
using System.Collections.Generic;

namespace EngineTests
{
    [TestClass]
    public class ToolsTests
    {
        [TestMethod]
        public void MinElemTests()
        {
            List<int> someList = new List<int>() { 3, 2, 1, 6, 5, 4 };

            var actualMaxElem = someList.MinElement(x => -x);
            var expecetedMaxElem = 6;

            Assert.AreEqual(expecetedMaxElem, actualMaxElem);
        }

        [TestMethod]
        public void MaxElemTests()
        {
            List<int> someList = new List<int>() { 3, 2, 1, 6, 5, 4 };

            var actualMaxElem = someList.MaxElement(x => -x);
            var expecetedMaxElem = 1;

            Assert.AreEqual(expecetedMaxElem, actualMaxElem);
        }
    }
}
