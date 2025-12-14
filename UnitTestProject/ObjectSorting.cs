using Assignment_cet2007;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

namespace UnitTestProject
{
    [TestClass]
    /// <summary>
    /// when comparing two objetcs there is 3 possible outcomes when comparing 2 devices/objects so it is important to test all three solutions
    /// </summary>
    public class ObjectSorting
    {
  
        [TestMethod]
     
       
        public void SameObjects()
        {
            var device1 = new Device("Printer" , "1122:3333:4444:5567" , 1 , "offline");
            var device2 = new Device("Printer" , "1122:3333:4444:5567" , 1 , "offline");
           int CompareResult = device1.CompareTo(device2);
            
            
                Assert.AreEqual(0,CompareResult);
            
        }
        [TestMethod]
           public void SecondObjectleLess()
           {
                var device1 = new Device("Printer" , "1122:3333:4444:5567" , 1 , "offline");
               var device2 = new Device("Iphone" , "1122:3333:4435:5567" , 1 , "offline");
              var CompareResult = device1.CompareTo(device2);
               
                   Assert.IsTrue( CompareResult>0);
               
           }
        [TestMethod]
        public void SecondObjectGreater()
        {
            var device1 = new Device("Iphone", "1122:3333:4444:5567", 1, "offline");
            var device2 = new Device("Printer", "1122:3333:4435:5567", 1, "offline");
            var CompareResult = device1.CompareTo(device2);
           
                Assert.IsTrue(CompareResult < 0);

        }
    }
}
