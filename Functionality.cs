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

namespace Assignment_cet2007
{
    /// <summary>
    /// This class is responsible for controlling all of the menu choices.
    /// </summary>
    class MenuFunctionality 
    {
       
        public  static int Num { get; private set; }
        public string NameInput { get; set; }
        public string IpInput { get; set; }

        /// <summary>
        /// this method is responsible for allowing the user to view all the devices on the system
        /// </summary>
        public  static void ViewAll()
        {
            CheckDevice();
            StartOption("Below is a list of all the devices currently within this system in the format device name followed by ip address:");
            ShowDevice(); 
            Logger.GetInstance().Log("Device data loaded successfully.");
            FinishOption();
        }

        /// <summary>
        /// This method will allow the user to add a device to the system
        /// </summary>
        public static void AddDevice()
        {
            Logger.GetInstance().Log("User has successfully chosen to add a device");
            StartOption("Adding a Device to the system");
            try
            {
                    Console.WriteLine("Enter The device name");
                    string nameinput = Console.ReadLine();

                    Console.WriteLine("Enter The ip address for the device please use the format of 8 groups of 4 hexadecmial digits separated by colons (ipv6)");
                    string ipinput = Console.ReadLine();

                    Num = 0;

                    if (!string.IsNullOrEmpty(nameinput) && !string.IsNullOrEmpty(ipinput))
                    {
                        Num = Num + 1;
                        /// id set to one for now but this will have to be made unique at some point
                        Device device = new Device(nameinput, ipinput, Num, "offline");
                        Manager.Network.Add(device);
                        Console.WriteLine("Device successfully created!");
                        Logger.GetInstance().Log("New Device " + nameinput + " added to the system");
                        FileSystem.FileDevice(Manager.Network);
                         try
                         {
                            Console.WriteLine("Would you like to add another device");
                            string repeat = Console.ReadLine().Trim().ToUpper();  /// converts to upper case and removes any extra spaces
                            if (repeat == "yes".ToUpper())
                            {
                                AddDevice();
                            FinishOption();

                            }
                            else
                            {
                                FinishOption();
                            }
                         }
                         catch(Exception )
                         {
                              FinishOption();
                         }
                    } 
                    else
                    {
                        InvalidData();
                        AddDevice();
                    }
            }
            catch(Exception )
            {
                InvalidData();
                AddDevice();
            }
        }

        /// <summary>
        ///  This method will allow users to edit devices
        /// </summary>
        public static void EditDevice()
        {
            CheckDevice();
            StartOption("Edit Device on the system");
            ShowDevice();

            Console.WriteLine("Enter the index of the device you would like to edit");
            int indexSelection = Convert.ToInt32(Console.ReadLine());
            indexSelection = indexSelection - 1;   /// this is due to options given on screen are 1 ahead meaning selction 1 is actually position 0 etc

            if (indexSelection >= 0 && indexSelection <= Manager.Network.Count - 1)
            {
                Console.WriteLine("you have succesfully chosen a device to edit");
                Logger.GetInstance().Log("User has successfully chosen to edit a device");

                try
                {
                    Console.WriteLine("Enter The device name");
                    string nameinput = Console.ReadLine();

                    Console.WriteLine("Enter The ip address for the device please use the format of 8 groups of 4 hexadecmial digits separated by colons (ipv6)");
                    string ipinput = Console.ReadLine();
                    if (!string.IsNullOrEmpty(nameinput))
                    {   
                        /// updates the devices across the system
                        Manager.Network[indexSelection].Name = nameinput;
                        Manager.Network[indexSelection].IpAddress = ipinput;

                        Console.WriteLine("Device successfully created!");
                        Logger.GetInstance().Log("New Device " + nameinput + " added to the system");
                        FileSystem.FileDevice(Manager.Network);
                    }
                    else
                    {
                        EditDevice();
                    }

                }

                catch(Exception)
                {
                    InvalidData();
                    EditDevice();
                }
                try
                {
                    Console.WriteLine("Would you like to edit another device");
                    string repeat = Console.ReadLine().Trim().ToUpper();  /// converts to upper case and removes any extra spaces
                    if (repeat == "yes".ToUpper())
                    {
                        EditDevice();
                    }
                    else
                    {
                        FinishOption();
                    }
                }
                catch (Exception)
                {
                    FinishOption();
                }
            }
            else
            {
                Console.WriteLine("You need to select an option within the index range of the devices try again");
                EditDevice();
            }

        }
        /// <summary>
        /// This feature will allow users to carry out a linear search for devices based on the first name of device within the system.
        /// </summary>
        public static void SearchDevice()
        {
            CheckDevice();
            StartOption("Search for a device on the system");
            
            Console.WriteLine("Enter the name of the device you would like to search for");
            string nameinput = Console.ReadLine();
            bool bfound = false;

            if (!string.IsNullOrEmpty(nameinput)) /// ensures that something is entered
            {
                for (int i = 0; i < Manager.Network.Count; i++)
                {
                    Console.WriteLine("Any devices that match your input will be displayed below:");
                    if (Manager.Network[i].Name.ToLower().Equals(nameinput))
                    { 
                        Console.WriteLine(Manager.Network[i].Details());
                        bfound = true;
                    }
                }
                if (!bfound)
                {
                    Console.WriteLine("No Devices with that name" + Environment.NewLine + "Would you like to try again?");
                    string repeat = Console.ReadLine().Trim().ToUpper();  /// converts to upper case and removes any extra spaces
                    if (repeat == "yes".ToUpper())
                    {
                        SearchDevice();

                    }
                    else
                    {
                        FinishOption();
                    }
                }    
            }
            else
            {
                SearchDevice();
            } 
        }

        /// <summary>
        ///  This method will allow users to update the device status
        /// </summary>
        public static void UpdateStatus()
        {
            CheckDevice();
            StartOption("");
            
            Console.WriteLine("Update Device Status");
            ShowDevice();
            Console.WriteLine("Enter the index of the device you would like to update the status");
            int indexSelection = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the status");
            string statusinput = Console.ReadLine(); 
            indexSelection = indexSelection - 1;
            Manager.Network[indexSelection].DeviceStatus = statusinput;
            FileSystem.FileDevice(Manager.Network);
            Device d = Manager.Network[1];
            Status dstatus = d as Status;
            dstatus.ShowStatus();
            FinishOption();
        }

        /// <summary>
        ///  This will allow the user to sort devices alphabeticaly using built in sort methods.
        /// </summary>
        public static void SortDevice()
        {
            StartOption("Sorting Device on the system");
            CheckDevice();
            Console.WriteLine("Below is a list of all the devices sorted into aplhabetical order based on the first name of the device." + Environment.NewLine);
            Logger.GetInstance().Log("User has succesfull sorted chosen to sort the devices into alphabetical order");
            Manager.Network.Sort(); ///uses compare to automatically
            foreach (Device device in Manager.Network)
            {

                Console.WriteLine(device.Name);
            }
            FinishOption();
        }
        /// <summary>
        /// This will allow the user to remove devices
        /// </summary>
        public static void RemoveDevice()
        {
            CheckDevice();
            StartOption("Remove a device from the system");
            
            if (Manager.Network.Count == 0)
            {
                Console.WriteLine("You need to add devices to the system before you can remove them");
            }
            else
            {
                Console.WriteLine("Here is a list of all the devices please enter the number corresponding to the device you want to remove" + Environment.NewLine);
                ShowDevice();

                int indexSelection = Convert.ToInt32(Console.ReadLine());
                indexSelection = indexSelection - 1; /// this is due to options given on screen are 1 ahead meaning selction 1 is actually position 0 etc

                if (indexSelection >= 0 && indexSelection <=  Manager.Network.Count - 1)
                {
                    Logger.GetInstance().Log("User has successfully chosen to remove a device ");
                    Console.WriteLine("device has been removed successfully");
                    Manager.Network.RemoveAt(indexSelection);
                }
                FinishOption();
            }    
        }
        /// <summary>
        /// This will allow the user to view the health of the system
        /// </summary>
        public static void ViewHealth() 
        {
            
            StartOption("View the health of the system devices");
            //// this will show the log files and json files etc
            Console.WriteLine("There is two options to view the system health would you like to view please enter the option you would like below:" + Environment.NewLine + "a) Full device specifications " + Environment.NewLine + "b) Full system Logs.");
           string UserChoice = Console.ReadLine().Trim().ToUpper();
            if (UserChoice==null)
            {
                InvalidData();
                ViewHealth();

            }
            else if (UserChoice== "a".ToUpper())
            {
                CheckDevice(); // must be device to show
                Console.WriteLine("This is the full specifications of devices on the system");
                FileSystem.LoadFile("SystemDevice.json");
            }
            else if (UserChoice == "b".ToUpper())
            {
                Console.WriteLine("This is the full System Logs");
               string Logs = File.ReadAllText("Logs.txt");
                Console.WriteLine(Logs);
                
            }



            FinishOption();
        }
        /// <summary>
        /// This will allow the user to exit the system properly.
        /// </summary>
        public static void Quit()
        {
            Logger.GetInstance().Log("User has successfully chosen to quit the system");
            Environment.Exit(0); 
        }

        /// <summary>
        /// Refactored so not reusing same code each time a menu option is called
        /// </summary>
        public static void StartOption(string message)
        {
            Console.Clear();
            Console.WriteLine(message + Environment.NewLine);
        }
        /// <summary>
        /// refactored so that there is code to loop back to menu once the user has finished the option.
        /// </summary>
        public static void FinishOption()
        {
            Console.WriteLine(Environment.NewLine + "Press enter to return to the main menu");
            Console.ReadLine();
        }
        /// <summary>
        /// This method will show the devices on the system
        /// </summary>
        public static void ShowDevice()
        {
            int i = 0;
            foreach (Device device in Manager.Network)
            {
                i++;
                Console.WriteLine(i + ". " + device.Details());
            }
        }

        /// <summary>
        /// This is called when invalid data is entered
        /// </summary>
        public static void InvalidData()
        {
            Console.WriteLine("Invalid data press enter to try again");
            Console.ReadLine();
            Console.Clear();
        }
        
        /// <summary>
        ///  Checks to see if there is any devices on the system.
        /// </summary>
        public static void CheckDevice()
            
        {
            Console.Clear();
            if (Manager.Network.Count == 0)
            {
                Console.WriteLine("You need to add devices at least one device to the system before you can use the system" + Environment.NewLine + "Press enter to be redirected to add new device to the system");
                Console.ReadLine();
                AddDevice();
            }
        }
    }
}
