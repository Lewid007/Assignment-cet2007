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
       static class FileSystem 
    {
       
       public static void FileDevice(List<Device> network)
        {
            /// serialize the data
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(network, options);
            File.WriteAllText("SystemDevice.json", json);


            /// deserializing the data
            string jsonData = File.ReadAllText("SystemDevice.json");
            List<Device> devicelist2 = JsonSerializer.Deserialize<List<Device>>(jsonData);


            try
            {
                jsonData = File.ReadAllText("SystemDevice.json");

            }
            catch
            {
                Console.WriteLine("file not found");
                File.WriteAllText("student.json", "[]");
            }

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
        public static void loadfile(string message)
        {

            try
            {
                deserialize();
                using (StreamReader reader = new StreamReader(message))
                {
                    string text = reader.ReadToEnd();
                    deserialize();
                    Console.WriteLine(text);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("File could not be read");
                Console.WriteLine(e.Message);
            }
        }
        public static void deserialize()
        {
            /// deserializing the data
            string jsonData = File.ReadAllText("SystemDevice.json");
            List<Device> devicelist2 = JsonSerializer.Deserialize<List<Device>>(jsonData);
        }
    }
}
