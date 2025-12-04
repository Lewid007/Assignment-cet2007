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
    ///  play around with colors font size etc-  Console.BackgroundColor = ConsoleColor.Green;
    internal class Program
    {
        /// <summary>
        /// This interface is used for implementing the search algorithm 
        /// </summary>
        public interface IComaprable
        {
            int CompareTo(object obj);
        }
        /// <summary>
        /// The Device class allows objects to be made within the system and it inherits from the status class aswell as two interfaces to allow for searching of devices and implementing the status of devices.
        /// </summary>
        public class Device : Status ,IComparable<Device>,Istate<Device>
        {
            public string Name { get; set; }
            public int IdUnique { get; set; }
            public string IpAddress { get; set; }
        

         
            public int CompareTo(Device other)
            {
                return Name.CompareTo(other.Name);
            }
            /// <summary>
            /// Creating an instance of the device class
            /// </summary>
            public Device(string name, string ipaddress, int idunique, string devicestatus) : base(devicestatus)
            {
                this.Name = name;
                this.IpAddress = ipaddress;
                this.IdUnique = idunique;
                this.DeviceStatus = devicestatus;
            }

            /// used for the status
            /// 
         

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

                return Name + " " + IpAddress;
            }
           

        }
        /// <summary>
        /// Interace which is responsible for linking together the feature to show the status of any device in the list
        /// </summary>
        
        interface Istate<Device>
        {
            void ShowStatus();
        }
        /// <summary>
        ///  This Class is responsible for showing the status of devices. It is the parent class as all devices must have a status
        /// </summary>
        public class Status   
        {
            public string DeviceStatus { get;  set; }
            

            public Status( string deviceStatus)
            {
                DeviceStatus = deviceStatus;
            }

           

            public void setStatus(string deviceStatus)
            {
                this.DeviceStatus = deviceStatus;
            }
            public void ShowStatus()
            {
                Console.WriteLine(DeviceStatus + "hello");
            }
            
        }
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
            public static Logger GetInstance()
            {
                if (instance == null)
                
                    instance = new Logger();
                    return instance;
                
                    
            }
            /// <summary>
            /// This method is responsible for creating a log and adding it to a text file.
            /// </summary>
            
            public void Log (string message)
            {
                string entry = "[Log - " + DateTime.Now.ToString("HH:mm:ss") + "]" + message;
                
                File.AppendAllText(logFile, entry + "\n");
            }

        }


        /// <summary>
        /// basic management system to allow a choice of feature
        /// </summary>
        class Manager
        {
            public int Menu_option { get; private set; }
            public int num { get; private set; }
            public string nameinput { get; set; }
            string ipinput { get; set; }
            public List<Device> network;

            /// <summary>
            /// creates and instance of the manager class
            /// </summary>
            /// 
            public void FileDevice()
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
            public Manager()
            {
                network = new List<Device>()
                { 
                /// manualy creating some devices  // the format of ipv6 and random numbers still needs to be done
                   new Device("Printer", "797a:efb2:fd97:368c:e92f:0c7a:d162:8073", 00000001,"online"),
                    new Device("Laptop", "d421:3ebd:a882:984e:1d7c:8c35:4834:d9f7", 00000002, "online"),
                    new Device("Usb", "797a:efb2:fd97:d162:8073", 00000003, "online"),
                    new Device("Mouse", "d421:3ebd:8c35:4834:d9f7", 00000004 ,"online"),



                };
                FileDevice();
                PrintMenu();

            }

            /// <summary>
            /// This method will display the menu and take a user input and match it to the correct choice they pick. this could and will likely be changed into an string array to make adding menu items a lot simpler
            /// </summary>
            public void PrintMenu()
            {
                Logger.GetInstance().Log("Menu Loaded successfully");
                Console.WriteLine("WELCOME" + Environment.NewLine);
                /// creating menu using string array to make adding and updating menu choice items easier in the future
                string[] menuOptions = new string[]
                {
                    "View All Devices",
                    "Add Device",
                    "Edit Device ",
                    "Search Device",
                    "Update Device Status",
                    "Sort Device",
                    "Remove Device",
                    "View System Health",
                    "Exit",
                };
                /// this for loop adds an index number to the menu options
                for (int i = 0; i < menuOptions.Length; i++)
                {
                    Console.WriteLine(i + 1 + "." + menuOptions[i]);
                }
                /// this section of code will link up the menu choice to the corresponding features with appropriate error handling used
                try
                {
                    Console.WriteLine(Environment.NewLine + "Enter your menu option here");
                    int Menu_option = Convert.ToInt32(Console.ReadLine());

                    if (Menu_option < 1 || Menu_option > menuOptions.Length)
                    {

                        InvalidData();
                        Logger.GetInstance().Log("User has entered invalid data for the menu option");


                        PrintMenu();
                    }
                    else if (Menu_option == 1)
                    {
                        Logger.GetInstance().Log("User has succesfully choesen to view all devices");
                        ViewAll();

                    }
                    else if (Menu_option == 2)
                    {
                        Logger.GetInstance().Log("User has succesfully chosen to add a new device to the sytem");
                        AddDevice();
                    }
                    else if (Menu_option == 3)
                    {
                        Logger.GetInstance().Log("User has succesfully chosen to edit a device on the system");
                        EditDevice();
                    }
                    else if (Menu_option == 4)
                    {
                        Logger.GetInstance().Log("User has succesfully chosen to Search for a device on the system");
                        SearchDevice();
                    }
                    else if (Menu_option == 5)
                    {
                        Logger.GetInstance().Log("User has succesfully chosen to update the device status on the system");
                        UpdateStatus();
                    }
                    else if (Menu_option == 6)
                    {
                        Logger.GetInstance().Log("User has succesfully chosen to sort the devices on the system");
                        SortDevice();
                    }
                    else if (Menu_option == 7)
                    {
                        Logger.GetInstance().Log("User has succesfully chosen to remove a device on the system");
                        RemoveDevice();
                    }
                    else if (Menu_option == 8)
                    {
                        Logger.GetInstance().Log("User has succesfully chosen to view the sytstem health");
                        ViewHealth();
                    }
                    else if (Menu_option == 9)
                    {
                        Logger.GetInstance().Log("User has succesfully chosen to quit the system");
                        Quit();
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException();
                    }

                        Console.Clear();
                    PrintMenu();

                }
                catch (FormatException e)
                {

                    InvalidData();
                    PrintMenu();

                }
            }

            /// <summary>
            ///  This method will print all the objects
            /// </summary>
            public void ViewAll()
            {
                StartOption("Below is a list of all the devices currently within this system in the format device name followed by ip address:");
                /// potentially need some error exception for when there is no users in the system
                ShowDevice(); /// reason this is seperate due to reusing the code just to show devices if view all was called this would print excess info if used in other class
                Logger.GetInstance().Log("Device data loaded successfully.");


                FinishOption();
            }
            /// <summary>
            /// This method will allow the user to add a device to the system
            /// </summary>
            public void AddDevice()
            {
                Logger.GetInstance().Log("User has successfully chosen to add a device");
                StartOption("Adding a Device to the system");

                try
                {
                    Device device = SetInfo();
                    if (device != null)
                    {

                        network.Add(device);
                        Console.WriteLine("Device successfully created!");
                        Logger.GetInstance().Log("New Device " + nameinput + " added to the system");
                        FileDevice();
                    }
                    else
                    {
                        AddDevice();
                    }
                }
                catch
                {
                    InvalidData();
                    AddDevice();
                }

                try
                {
                    Console.WriteLine("Would you like to add another device");
                    string repeat = Console.ReadLine().Trim().ToUpper();  /// converts to upper case and removes any extra spaces
                    if (repeat == "yes".ToUpper())
                    {
                        AddDevice();

                    }
                    else
                    {
                        FinishOption();
                    }
                }
                catch
                {
                    FinishOption();
                }
            }
            public void EditDevice()
            {
                StartOption("Edit Device on the system");
                /// Very basic but does the job - checks to see if any devices are already in the system to edit
                checkdevice();
                
               
                ShowDevice();


                Console.WriteLine("Enter the index of the device you would like to edit");
                int indexSelection = Convert.ToInt32(Console.ReadLine());
                indexSelection = indexSelection - 1; /// this is due to options given on screen are 1 ahead meaning selction 1 is actually position 0 etc

                if (indexSelection >= 0 && indexSelection <= network.Count - 1)
                {
                    Console.WriteLine("you have succesfully chosen a device to edit");
                    Logger.GetInstance().Log("User has successfully chosen to edit a device");


                    try
                    {
                        Device device = SetInfo();
                        if (device != null)
                        {
                            network[indexSelection] = device;

                            Console.WriteLine("Device successfully created!");
                            // Logger.GetInstance().Log("New Device " + nameinput + " added to the system");
                            FileDevice();
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


            public void SearchDevice()
            {
                StartOption("Search for a device on the system");
                checkdevice();
                Console.WriteLine("Enter the name of the device you would like to search for");
                string nameinput = Console.ReadLine();
                bool bfound = false;

                if (!string.IsNullOrEmpty(nameinput)) /// ensures that something is entered
                {
                    for (int i = 0; i < network.Count; i++)
                    {
                        if (network[i].Name.ToLower().Equals(nameinput))
                        {
                            Console.WriteLine(network[i].Details());
                            bfound = true;
                        }

                    }
                    if (!bfound)
                    {
                        Console.WriteLine("No Devices with that name");
                    }
                    else
                    {

                    }
                }
                else
                {
                    SearchDevice();
                }
                FinishOption();

            }
            public void UpdateStatus() ///status is seperate list due to security reas
            {
                StartOption("");
                Console.WriteLine("Update Device Status");
                ShowDevice();
                Console.WriteLine("Enter the index of the device you would like to update the status");
                int indexSelection = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter the status");
                string statusinput = Console.ReadLine();
                indexSelection = indexSelection - 1;
                network[indexSelection].DeviceStatus = statusinput;
                FileDevice();

                Device d = network[1];
                Status dstatus = d as Status;

                dstatus.ShowStatus();
                FinishOption();
            }
            public void SortDevice()
            {
                StartOption("Sorting Device on the system");

                Console.WriteLine("Below is a list of all the devices sorted into aplhabetical order based on the first name of the device." + Environment.NewLine);
                Logger.GetInstance().Log("User has succesfull sorted chosen to sort the devices into alphabetical order");
                network.Sort(); ///uses compare to automatically
                foreach (Device device in network)
                {

                    Console.WriteLine(device.Name);
                }
                FinishOption();
            }
            public void RemoveDevice()
            {
                StartOption("Remove a device from the system");
                if (network.Count == 0)
                {
                    Console.WriteLine("You need to add devices to the system before you can remove them");
                }
                else
                {
                    Console.WriteLine("Here is a list of all the devices please enter the number corresponding to the device you want to remove" + Environment.NewLine);
                    ShowDevice();

                    int indexSelection = Convert.ToInt32(Console.ReadLine());
                    indexSelection = indexSelection - 1; /// this is due to options given on screen are 1 ahead meaning selction 1 is actually position 0 etc

                    if (indexSelection >= 0 && indexSelection <= network.Count - 1)
                    {
                        Logger.GetInstance().Log("User has successfully chosen to remove a device ");
                        network.RemoveAt(indexSelection);
                    }
                    FinishOption();

                }
                FinishOption();
            }
            public void ViewHealth() /// implemented once all the file and data handling is done

            {
                StartOption("");
                //// this will show the log files and json files etc
                loadfile("SystemDevice.json");
                Console.WriteLine("View the health of the system devices");
                FinishOption();
            }
            public void Quit()
            {
                Logger.GetInstance().Log("User has successfully chosen to quit the system");
                Environment.Exit(0); /// sorta works for now
            }
            public void StartOption(string message)
            {
                Console.Clear();
                Console.WriteLine(message + Environment.NewLine);
            }
            /// <summary>
            /// responsible for the code to loop back to menu once the user has finished this option.
            /// </summary>
            public void FinishOption()
            {
                Console.WriteLine(Environment.NewLine + "Press enter to return to the main menu");
                Console.ReadLine();
            }
            public void ShowDevice()
            {
                int i = 0;
                foreach (Device device in network)
                {
                    i++;
                    Console.WriteLine(i + ". " + device.Details());
                }
            }


            public void loadfile(string message)
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
            public void deserialize()
            {
                /// deserializing the data
                string jsonData = File.ReadAllText("SystemDevice.json");
                List<Device> devicelist2 = JsonSerializer.Deserialize<List<Device>>(jsonData);
            }
            public void InvalidData()
            {
                Console.WriteLine("Invalid data press enter to try again");
                Console.ReadLine();
                Console.Clear();
            }
            public Device SetInfo()
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
                    return new Device(nameinput, ipinput, num, "offline");
                }
                else
                {
                    InvalidData();

                }
                return null;
            }

            public void checkdevice()
            {
            if (network.Count == 0)
                {
                    Console.WriteLine("You need to add devices to the system before you can edit them" + Environment.NewLine +  "Press enter to be redirected and add users to the system");
                    Console.ReadLine();
                    
                }
            }

        }
        static void Main(string[] args)
        {
          
            
           Console.Clear();  
           Manager manager = new Manager();
            /// creates the menu


        }
    }
}
