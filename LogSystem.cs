using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace Assignment_cet2007
{
    /// <summary>
    ///  This class is for auditing purposes and contains everything to do with the log aspect of the system
    /// </summary>
    public class Logger
    {

        private static Logger instance;
        private static string logFile = "Logs.txt";

        private Logger()
        {
        }
        /// <summary>
        /// creating an instance of the class
        /// </summary>
        /// <returns></returns>
        public static Logger GetInstance()
        {
            if (instance == null)

                instance = new Logger();
            return instance;


        }
        /// <summary>
        /// This method is responsible for creating a log and adding it to a text file.
        /// </summary>

        public void Log(string message)
        {
            string entry = "[Log - " + DateTime.Now.ToString("HH:mm:ss") + "]" + message;

            File.AppendAllText(logFile, entry + "\n");
        }

    }
}
