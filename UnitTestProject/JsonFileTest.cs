using Assignment_cet2007;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace UnitTestProject
{
    [TestClass]
    public class JsonFileTest
    {
        [TestMethod]
        public void FileExistTest()
        {
            FileSystem.FileExist();
            Assert.IsTrue(File.Exists("SystemDevice.json"));
          
        }
    }
}
