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
    class menuFunctionality 
    {
       
        public  static int num { get; private set; }
        public string nameinput { get; set; }
        string ipinput { get; set; }

        public static void ViewAll()
        {
            checkdevice();
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

                    Console.WriteLine("Enter The ip address for the device");
                    string ipinput = Console.ReadLine();

                    num = 0;

                    if (!string.IsNullOrEmpty(nameinput) && !string.IsNullOrEmpty(ipinput))
                    {
                        num = num + 1;
                        /// id set to one for now but this will have to be made unique at some point
                        Device device = new Device(nameinput, ipinput, num, "offline");
                        Manager.network.Add(device);
                        Console.WriteLine("Device successfully created!");
                        Logger.GetInstance().Log("New Device " + nameinput + " added to the system");
                        FileSystem.FileDevice(Manager.network);
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
                         catch(Exception e)
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
            catch(Exception e)
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
            checkdevice();
            StartOption("Edit Device on the system");
            ShowDevice();
            
       


            Console.WriteLine("Enter the index of the device you would like to edit");
            int indexSelection = Convert.ToInt32(Console.ReadLine());
            indexSelection = indexSelection - 1; /// this is due to options given on screen are 1 ahead meaning selction 1 is actually position 0 etc

            if (indexSelection >= 0 && indexSelection <= Manager.network.Count - 1)
            {
                Console.WriteLine("you have succesfully chosen a device to edit");
                Logger.GetInstance().Log("User has successfully chosen to edit a device");


                try
                {
                    Console.WriteLine("Enter The device name");
                    string nameinput = Console.ReadLine();

                    Console.WriteLine("Enter The ip address for the device");
                    string ipinput = Console.ReadLine();
                    if (!string.IsNullOrEmpty(nameinput))
                    {
                        Manager.network[indexSelection].Name = nameinput;
                        Manager.network[indexSelection].IpAddress = ipinput;
                        Console.WriteLine("Device successfully created!");
                        Logger.GetInstance().Log("New Device " + nameinput + " added to the system");
                        FileSystem.FileDevice(Manager.network);
                    }
                    else
                    {
                        EditDevice();
                    }

                }

                catch
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
        ///  this feature will allow users to search for devices based on the name of device
        /// </summary>
        ///linear search
        public static void SearchDevice()
        {
            checkdevice();
            StartOption("Search for a device on the system");
            
            Console.WriteLine("Enter the name of the device you would like to search for");
            string nameinput = Console.ReadLine();
            bool bfound = false;

            if (!string.IsNullOrEmpty(nameinput)) /// ensures that something is entered
            {
                for (int i = 0; i < Manager.network.Count; i++)
                {
                    Console.WriteLine("Any devices that match your input will be displayed below:");
                    if (Manager.network[i].Name.ToLower().Equals(nameinput))
                    { 
                        Console.WriteLine(Manager.network[i].Details());
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
        public static void UpdateStatus() ///status is seperate list due to security reas
        {
            checkdevice();
            StartOption("");
            
            Console.WriteLine("Update Device Status");
            ShowDevice();
            Console.WriteLine("Enter the index of the device you would like to update the status");
            int indexSelection = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the status");
            string statusinput = Console.ReadLine(); //// need validation on this input
            indexSelection = indexSelection - 1;
            Manager.network[indexSelection].DeviceStatus = statusinput;
            FileSystem.FileDevice(Manager.network);

            Device d = Manager.network[1];
            Status dstatus = d as Status;

            dstatus.ShowStatus();
            FinishOption();
        }
        /// <summary>
        ///  This will allow the user to sort devices
        /// </summary>
        public static void SortDevice()
        {
            StartOption("Sorting Device on the system");
            checkdevice();
            Console.WriteLine("Below is a list of all the devices sorted into aplhabetical order based on the first name of the device." + Environment.NewLine);
            Logger.GetInstance().Log("User has succesfull sorted chosen to sort the devices into alphabetical order");
            Manager.network.Sort(); ///uses compare to automatically
            foreach (Device device in Manager.network)
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
            checkdevice();
            StartOption("Remove a device from the system");
            
            if (Manager.network.Count == 0)
            {
                Console.WriteLine("You need to add devices to the system before you can remove them");
            }
            else
            {
                Console.WriteLine("Here is a list of all the devices please enter the number corresponding to the device you want to remove" + Environment.NewLine);
                ShowDevice();

                int indexSelection = Convert.ToInt32(Console.ReadLine());
                indexSelection = indexSelection - 1; /// this is due to options given on screen are 1 ahead meaning selction 1 is actually position 0 etc

                if (indexSelection >= 0 && indexSelection <=  Manager.network.Count - 1)
                {
                    Logger.GetInstance().Log("User has successfully chosen to remove a device ");
                    Console.WriteLine("device has been removed successfully");
                    Manager.network.RemoveAt(indexSelection);
                }
                FinishOption();

            }
            
        }
        /// <summary>
        /// This will allow the user to view the health of the system
        /// </summary>
        public static void ViewHealth() /// implemented once all the file and data handling is done

        {
            StartOption("");
            //// this will show the log files and json files etc
            //loadfile("SystemDevice.json");
            Console.WriteLine("View the health of the system devices");
            FinishOption();
        }
        /// <summary>
        /// allow the user to exit the system properly.
        /// </summary>
        public static void Quit()
        {
            Logger.GetInstance().Log("User has successfully chosen to quit the system");
            Environment.Exit(0); /// sorta works for now
        }
        public static void StartOption(string message)
        {
            Console.Clear();
            Console.WriteLine(message + Environment.NewLine);
        }
        /// <summary>
        /// responsible for the code to loop back to menu once the user has finished this option.
        /// </summary>
        public static void FinishOption()
        {
            Console.WriteLine(Environment.NewLine + "Press enter to return to the main menu");
            Console.ReadLine();
        }
        public static void ShowDevice()
        {
            int i = 0;
            foreach (Device device in Manager.network)
            {
                i++;
                Console.WriteLine(i + ". " + device.Details());
            }
        }



        public static void InvalidData()
        {
            Console.WriteLine("Invalid data press enter to try again");
            Console.ReadLine();
            Console.Clear();
        }
        
        /// <summary>
        ///  gonna unit test this
        /// </summary>
        public static void checkdevice()
        {
            Console.Clear();
            if (Manager.network.Count == 0)
            {
                Console.WriteLine("You need to add devices at least one device to the system before you can use the system" + Environment.NewLine + "Press enter to be redirected to add new device to the system");
               
                Console.ReadLine();
                AddDevice();
                

            }
        }
    }
}
