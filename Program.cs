using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assignment_cet2007
{
    internal class Program
    {


        /// <summary>
        /// represents the device class used to link together the main attributes for devices
        /// </summary>
        class Device

        {

            public string Name { get; set; }
            public int IdUnique { get; set; }
            public string IpAddress { get; set; }

            /// <summary>
            /// Creating an instance of the device class
            /// </summary>
            /// <param name="name"></param>
            /// <param name="ipaddress"></param>
            /// <param name="idunique"></param>
            public Device(string name, string ipaddress, int idunique)
            {

                this.Name = name;
                this.IpAddress = ipaddress;
                this.IdUnique = idunique;


            }
            public void setName(string Name)
            {
                this.Name = Name;
            }
            public void setIpAddress(string IpAddress)
            {
                this.IpAddress = IpAddress;
            }
            public void setIDUnique(string IpUnique)
            {
                this.IpAddress = IpUnique;
            }

            /// <summary>
            /// Describes the attributes
            /// </summary>
            public string Details()
            {

                return Name + IpAddress + IdUnique;
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
        /// <summary>
        /// basic management system to allow a choice of feature- not all features implemenented at this stage
        /// </summary>
        class Manager   
        {
            public int Menu_option { get; private set; }
            public List<Device> network;
            public Manager()
            {

                network = new List<Device>()

                
                /// manualy creating some devices
                {
                    new Device("Printer", "123455", 0235454676),
                    new Device("Laptop", "125667865", 0465673676)
                }
                ;
                PrintMenu();
            }
            /// <summary>
            /// This method will display the menu and take a user input and match it to the correct choice they pick. this could and will likely be changed into an string array to make adding menu items a lot simpler
            /// </summary>
            public void PrintMenu()
            {
                Console.WriteLine("WELCOME" + Environment.NewLine);
                Console.WriteLine("1. View All Devices");
                Console.WriteLine("2. Add Device");
                Console.WriteLine("3. Edit Device ");
                Console.WriteLine("4. Search Device");
                Console.WriteLine("5. Update Device Status");
                Console.WriteLine("6. Sort Device");
                Console.WriteLine("7. Remove Device");
                Console.WriteLine("8. View System Health");
                Console.WriteLine("9. Exit");
                
                try
                {
                    Console.WriteLine("Enter your menu option here");
                    int Menu_option = Convert.ToInt32(Console.ReadLine());
                    if (Menu_option < 1 || Menu_option >= 9)
                    {

                        Console.WriteLine("Please enter the correct whole number choice you would like to select from this menu. Press enter to continue ");
                        Console.ReadLine();
                        PrintMenu();
                    }
                    else if (Menu_option == 1)
                    {
                        ViewAll();
                    }
                    else  if (Menu_option == 2)
                    {
                        AddDevice();
                    }
                    else if (Menu_option == 3)
                    {
                        EditDevice();
                    }
                    else if (Menu_option == 4)
                    {
                        SearchDevice();
                    }
                    else if (Menu_option == 5)
                    {
                        UpdateStatus();
                    }
                    else if (Menu_option == 6)
                    {
                        SortDevice();
                    }
                    else if (Menu_option == 7)
                    {
                        RemoveDevice();
                    }
                    else if (Menu_option == 8)
                    {
                        ViewHealth();
                    }
                    else if (Menu_option == 9)
                    {
                        Quit();
                    }

                }

                catch (FormatException e)
                { 

                    Console.WriteLine(e.ToString() + "Invalid data Press enter to try again");
                    
                    Console.ReadLine();
                    Console.Clear();
                    PrintMenu();

                }
                
                  
                
              
            }

            ///  these are very basic print satements used to test each aspect works and links up correctly from the menu before any more complex development begins

            public void ViewAll()
            {
                Console.WriteLine("View All Devices");
                int i = 0;

                foreach (Device  device in network)
                {
                    i++;
                    Console.WriteLine(i + "." + device.Details());
                }
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

         
          
           Console.Clear();  /// at the minute the program ask for devices to be entered first so once this is done the console is cleared to bring up the menu - this will change once all menu features are implemented the menu/ welcome screen will be the first aspect of the system the user will interact with.
           
            Manager manager = new Manager();
           
            
        }
    }
}
