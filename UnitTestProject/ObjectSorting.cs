using Assignment_cet2007;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

namespace UnitTestProject
{
    [TestClass]
    public class ObjectSorting
    {
        /// <summary>
        /// when comparing two objetcs there is 3 possible solutions so it is important to test all three solutions
        /// </summary>
        [TestMethod]
        public void SameObjects()
        {
            var device1 = new Device("Printer" , "1122:3333:4444:5567" , 1 , "offline");
            var device2 = new Device("Iphone" , "1122:3333:4444:5567" , 1 , "offline");
           int CompareResult = device1.CompareTo(device2);
            if (CompareResult == 0)
            {
                Assert.AreEqual(0, CompareResult);
            }
        }
        [TestMethod]
           public void secondObjectless()
           {
                var device1 = new Device("Printer" , "1122:3333:4444:5567" , 1 , "offline");
               var device2 = new Device("Iphone" , "1122:3333:4435:5567" , 1 , "offline");
              var CompareResult = device1.CompareTo(device2);
               if (CompareResult == 0)
               {
                   Assert.Equals(0, CompareResult);
               }
           }
        [TestMethod]
           public void secondobjectgreater()
           {
            var device1 = new Device("Iphone", "1122:3333:4444:5567", 1, "offline");
            var device2 = new Device("Printer", "1122:3333:4435:5567", 1, "offline");
            var CompareResult = device1.CompareTo(device2);
            if (CompareResult == 0)
            {
                Assert.Equals(0, CompareResult);
            }
        }
        
    }
}
