using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_cet2007
{
    internal class Program
    {
        private static Random Id_Unique;  /// used to generate a random number for the id 

       class Device   /// setting up a device class which is used to create devices on the network system
        {
            public string Name { get; set; }
            public static Random Id_Unique { get; set; }
            public int Ip_Address { get; set; }


            public void Network_Device(string name, int ip_address) //// 
            {
                this.Name = name;   
                this.Ip_Address = ip_address;
            }

        }
        
        
        static void Main(string[] args)    /// main program
        {
            Random rnd = new Random();
            int rndInt = rnd.Next();
            rnd = Id_Unique;
            
            Console.WriteLine("enter the name of the network device here"); //// takes in the input of the name of the device
            string Name = Console.ReadLine();

            
            Console.WriteLine("enter the name Ip address of the network device here");
            int Ip_Address = Convert.ToInt32(Console.ReadLine());  /// takes the input of ip as int for now this needs to be changed data type at some point to all for ipv6 or ipv4 

            var D1 = new Device();
            
        }
    }
}
