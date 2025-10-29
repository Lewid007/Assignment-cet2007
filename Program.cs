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
            public int Id_Unique { get; set; }
            public int Ip_Address { get; set; }


            public Device(string name, int ip_address, int id_unique) 
            {
                this.Name = name;   
                this.Ip_Address = ip_address;
                this.Id_Unique = id_unique;
            }
            public void describe()
            {
                Console.WriteLine(Name + " " + Ip_Address + " " + Id_Unique);
            }
        }
        
        
        static void Main(string[] args)    /// main program
        {
           var D1 = new Device("Printer" , 1 , 12345);


            D1.describe();

           
            
        }
    }
}
