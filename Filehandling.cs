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
using static Assignment_cet2007.Program;
using System.Runtime.InteropServices;

namespace Assignment_cet2007
{
   /// <summary>
   /// This class is responsible for all the json file handiling.
   /// </summary>
    public static class FileSystem 
    {
       /// <summary>
       /// This method will file devices to the system
       /// </summary>
      
       public static void FileDevice(List<Device> network)
        {
            FileExist();
            /// serialize the data
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(network, options);
            File.WriteAllText("SystemDevice.json", json);

            try
            {
                string data = File.ReadAllText("SystemDevice.json");
                var devices = JsonSerializer.Deserialize<List<Device>>(data);

            }
            catch (JsonException ex)
            {
                Console.WriteLine("malformed Json file:" + ex.Message);
                File.WriteAllText("SystemDevice.json", "[]"); ///reset the file
            }
        }
        /// <summary>
        /// This will check if a fiel exist then load it when called
        /// </summary>
       
        public static void loadfile(string message)
        {
            FileExist();
            try
            {
              
                deserialize();
                
            }
            catch (IOException e)
            {
                Console.WriteLine("File could not be read");
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// This method will turn the json file data back into human readable data and print it to the console when called.
        /// </summary>
        public static void deserialize()
        {
            /// deserializing the data
            string jsonData = File.ReadAllText("SystemDevice.json");
            List<Device> devicelist2 = JsonSerializer.Deserialize<List<Device>>(jsonData);
            foreach (Device dev in devicelist2)
            {
                Console.WriteLine(dev.Name + " " + dev.IpAddress + " " + dev.IdUnique + " " + dev.DeviceStatus);
            }
        }
        /// <summary>
        /// This will check to see if a file exists.
        /// </summary>
        public static void FileExist()
        {
            try
            {
                string jsonData = File.ReadAllText("SystemDevice.json");
                jsonData = File.ReadAllText("SystemDevice.json");

            }
            catch (FileNotFoundException ex) 
            {
                Console.WriteLine("file not found");
                File.WriteAllText("student.json", "[]");
            }
        }
    }
}
