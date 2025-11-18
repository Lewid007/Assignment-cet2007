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
            public int IdUnique { get; set; }
            public string IpAddress { get; set; }


            public Device(string name, string ipaddress, int idunique)  ///instance of the device class 
            {
                this.Name = name;
                this.IpAddress = ipaddress;
                this.IdUnique = idunique;


            }




            public void describe()  /// describes the objects attributes
            {
               
                Console.WriteLine("The name of the device added is " + Name + ".It has an Ip Address of " + IpAddress + "and a unique value of " + IdUnique + ".");
            }

            /*  private HashSet<int> id_unique = new HashSet<int>();
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
           */
        }
        class Manager  /// basic management system to allow a choice of feature- not all features implemenented at this stage
        {


            public Manager()
            {

                printMenu();
            }
            public void printMenu()
            {
                Console.WriteLine("1. View All Devices");
                Console.WriteLine("2. Add Device");
                Console.WriteLine("3. Edit Device ");
                Console.WriteLine("4. Search Device");
                Console.WriteLine("5. Update Device Status");
                Console.WriteLine("6. Sort Device");
                Console.WriteLine("7. Remove Device");
                Console.WriteLine("8. View System Health");
                Console.WriteLine("9. Exit");


                Console.WriteLine("Enter your menu option here");
                int Menu_option = Convert.ToInt32(Console.ReadLine());
                if (Menu_option == 1)
                {
                    ViewAll();
                }
                if (Menu_option == 2)
                {
                    AddDevice();
                }
                if (Menu_option == 3)
                {
                    EditDevice();
                }
                if (Menu_option == 4)
                {
                    SearchDevice();
                }
                if (Menu_option == 5)
                {
                    UpdateStatus();
                }
                if (Menu_option == 6)
                {
                    SortDevice();
                }
                if (Menu_option == 7)
                {
                    RemoveDevice();
                }
                if (Menu_option == 8)
                {
                    ViewHealth();
                }
                if (Menu_option == 9)
                {
                    Quit();
                }
            }

            ///  these are very basic print satements used to test each aspect works and links up correctly from the menu before any more complex development begins

            public void ViewAll()
            {
                Console.WriteLine("View All Devices");
            }
            public void AddDevice()
            {
                Console.WriteLine("Add Device to the system");
            }
            public void EditDevice()
            {
                Console.WriteLine("Edit Device on the system");
            }
            public void SearchDevice()
            {
                Console.WriteLine("Search Device on the system");
            }
            public void UpdateStatus()
            {
                Console.WriteLine("Update Device Status");
            }
            public void SortDevice()
            {
                Console.WriteLine("Sort Device on the system");
            }
            public void RemoveDevice()
            {
                Console.WriteLine("Remove Device on the system");
            }
            public void ViewHealth()
            {
                Console.WriteLine("View the health of the system devices");
            }
            public void Quit()
            {
                Console.WriteLine("Exit");
            }

        }
        static void Main(string[] args) 
        {


                Console.WriteLine("enter the name of the network device here"); //// takes in the input of the name of the device
                string Name = Console.ReadLine();



                Console.WriteLine("enter the name Ip address of the network device here - please type it in ipv6 format");
                string IpAddress = (Console.ReadLine()); /// takes the input of ip as string ipv6 - try and catch blocks will be used to monitor length of input


                var D1 = new Device(Name, IpAddress, 1); /// setting up an object of the class device - id still needs be made unique this will prevent duplicate objects
                D1.describe();
            Console.Clear();  /// at the minute the program ask for devices to be entered first so once this is done the console is cleared to bring up the menu - this will change

                Manager manager = new Manager();

            
        }
    }
}
