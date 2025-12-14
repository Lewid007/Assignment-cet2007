using Assignment_cet2007;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace UnitTestProject
{
    [TestClass]
    public class CheckDeviceTest    
    {
        [TestMethod]
        public void IsListempty()
        {
             Manager.Network.Clear(); /// ensures the list is empty
             Assert.AreEqual(0, Manager.Network.Count);
         
        }
    }
}
