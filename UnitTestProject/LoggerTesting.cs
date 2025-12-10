using Assignment_cet2007;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;


namespace UnitTestProject
{
    [TestClass]
    public class LoggerTesting
    {
        [TestMethod]
        /// this method will test the logger part of the system to ensure functionality is correct
        public void LogToFile()
        {

            Logger.GetInstance().Log("This is a test log");
            string LogContents = File.ReadAllText("Logs.txt");
            Assert.IsTrue(LogContents.Contains("This is a test log"));



        }
    }
}
