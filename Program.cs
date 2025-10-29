using System;
using System.Collections.Generic;
using System.Configuration;
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
        

       class Device   /// setting up a device class which is used to create devices on the network system
        {
            public string Name { get; set; }
            public int Id_Unique { get; set; }
            public string Ip_Address { get; set; }


            public Device(string name, string ip_address, int id_unique) 
            {
                this.Name = name;   
                this.Ip_Address = ip_address;
                this.Id_Unique = id_unique;

                Console.WriteLine("enter the name of the network device here"); //// takes in the input of the name of the device
                string Name = Console.ReadLine();



                Console.WriteLine("enter the name Ip address of the network device here - please type it in ipv6 format");
                string Ip_Address = (Console.ReadLine()); /// takes the input of ip as string ipv6 - try and catch blocks will be used to monitor length of input
            }
            

            public void describe()  /// describes the objects attributes
            {
                Console.WriteLine("The name of the device added is "+ Name+ ".It has an Ip Address of " + Ip_Address + "and a unique value of " + Id_Unique +".");
            }

            private HashSet<int> id_unique = new HashSet<int>();
            public void Add_Id_Unique(int id)
            {
                try
                {
                    if (!id_unique.Add(id))
                    {
                        throw new InvalidOperationException("attribute exists");

                    }
                }
                catch(InvalidOperationException ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
            }
        }


        static void Main(string[] args, string Ip_Address, string Name) /// main program
        {
           

            
            
            
            var D1 = new Device(Name, Ip_Address,1); /// setting up an object of the class device - id still needs be made unique this will prevent duplicate objects
            D1.describe();

           
            
        }
    }
}
