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

namespace Assignment_cet2007
{
    internal class Program
    {
        /// <summary>
        /// represents the device class used to link together the main attributes for devices
        /// </summary>
        
      
        public interface IComaprable
        {
            int CompareTo(object obj);
        }
        
        public class Device: IComparable<Device>
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
            public int CompareTo(Device other)
            {
                return Name.CompareTo(other.Name);
            }
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
            public void setIDUnique(string IpUnique)  //// this will be kept at 8 digits starting at 00000001 and will be hidden from the user as it is irelavant as the main purpose is to prevent duplicate items
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
        ///  class responsible for showing the status of devices
        /// </summary>
        public class Status : Device
        {
            public string DeviceStatus { get; private set; }
            public Status(string deviceStatus, string name, string ipaddress, int idunique) : base(name, ipaddress, idunique)
            {
                DeviceStatus = deviceStatus;
            }
            public void setStatus(string deviceStatus)
            {
                this.DeviceStatus = deviceStatus;
            }
            public string showstatus()
            {
                return DeviceStatus;
            }
        }
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
            public void Log (string message)
            {
                string entry = "[Log - " + DateTime.Now.ToString("HH:mm:ss") + "]" + message;
                Console.WriteLine(entry); /// proves the logger is working 
                File.AppendAllText(logFile, entry + "\n");
            }

        }


        /// <summary>
        /// basic management system to allow a choice of feature- not all features implemenented at this stage
        /// </summary>
        class Manager   
        {
            public int Menu_option { get; private set; }
           public int num {  get; private set; }    
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
                   new Device("Printer", "797a:efb2:fd97:368c:e92f:0c7a:d162:8073", 00000001),
                    new Device("Laptop", "d421:3ebd:a882:984e:1d7c:8c35:4834:d9f7", 00000002),
                    new Device("Usb", "797a:efb2:fd97:d162:8073", 00000003),
                    new Device("Mouse", "d421:3ebd:8c35:4834:d9f7", 00000004)



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
                /// creating menu using string array to make adding new items easier in the future
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
                /// this for loop adds numbers to the menu options
                for (int i = 0; i < menuOptions.Length; i++)
                {
                    Console.WriteLine(i+1 +"."+ menuOptions[i]);
                }
                
                try
                {
                    Console.WriteLine(Environment.NewLine +"Enter your menu option here");
                    int Menu_option = Convert.ToInt32(Console.ReadLine());

                    if (Menu_option < 1 || Menu_option > menuOptions.Length)
                    {

                        Console.WriteLine("Invalid data press enter to try again");
                        Logger.GetInstance().Log("User has entered invalid data for the menu option");
                        Console.ReadLine();
                        Console.Clear();
                        PrintMenu();
                    }
                    else if (Menu_option == 1)
                    {
                        Logger.GetInstance().Log("User has succesfully choesen to view all devices");
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

                    Console.Clear();
                    PrintMenu();

                }   
                catch (FormatException e)
                { 

                    Console.WriteLine( "Invalid data press enter to try again");
                    
                    Console.ReadLine();
                    Console.Clear();
                    PrintMenu();

                } 
            }

            ///  these are very basic print satements used to test each aspect works and links up correctly from the menu before any more complex development begins
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
            public void AddDevice()
            {
                Logger.GetInstance().Log("User has successfully chosen to add a device");
                StartOption("Adding a Device to the system");

                Console.WriteLine("Enter The device name");
                string nameinput = Console.ReadLine();

                Console.WriteLine("Enter The ip address for the device");
                string ipinput = Console.ReadLine();
                num  = 0;

                if (!string.IsNullOrEmpty(nameinput) && !string.IsNullOrEmpty(ipinput))
                {
                    num = num + 1;
                    Device device = new Device(nameinput, ipinput, num); /// id set to one for now but this will have to be made unique at some point
                    network.Add(device);

                    


                    Console.WriteLine("Device successfully created!");
                    Logger.GetInstance().Log("New Device " + nameinput + " added to the system");
                    FileDevice();
                    
                    try
                    {
                        Console.WriteLine("Would you like to add another device yes or no");
                        string repeat = Console.ReadLine().Trim().ToUpper();  /// converts to upper case and removes any extra spaces
                        if (repeat == "yes".ToUpper())
                        {
                            AddDevice();

                        }

                        else if (repeat == "no".ToUpper())
                        {
                            FinishOption();

                        }
                        else
                        {
                            Console.WriteLine("inconsistent data has been entered therefore it is assumed that");
                            FinishOption();
                        }
                    }
                    catch
                    {
                        FinishOption();
                    }

                }
                else
                {
                    Console.WriteLine("Please add data to all input fields press enter to try again");
                    Console.ReadLine();
                    AddDevice();
                }
                
                
                    
                

                /// for sucesfull options play around with colors font size etc-  Console.BackgroundColor = ConsoleColor.Green;
                
            }
            public void EditDevice()
            {
                StartOption("Edit Device on the system");
                /// Very basic but does the job - checks to see if any devices are already in the system to edit
                if (network.Count == 0)
                {
                    Console.WriteLine("You need to add devices to the system before you can edit them");
                }
                else
                {
                    ShowDevice();
                    try
                    {
                        Console.WriteLine("Enter the index of the device you would like to edit");
                        int indexSelection = Convert.ToInt32(Console.ReadLine());
                        indexSelection = indexSelection - 1; /// this is due to options given on screen are 1 ahead meaning selction 1 is actually position 0 etc

                        if (indexSelection >= 0 && indexSelection <= network.Count - 1)
                        {
                            Console.WriteLine("you have succesfully chosen a device to edit");

                            Console.WriteLine("Enter The device name");
                            string nameinput = Console.ReadLine();

                            Console.WriteLine("Enter The ip address for the device");
                            string ipinput = Console.ReadLine();
                            if (!string.IsNullOrEmpty(nameinput))
                            {
                                network[indexSelection].Name= nameinput;
                                network[indexSelection].IpAddress = ipinput;
                              
                                FileDevice();
                                
                            }
                            else
                            {
                                Console.WriteLine("Please add data to all input fields");
                                EditDevice();
                            }
                            
                        }
                        else
                        {
                            Console.WriteLine("you need to select a valid option");
                            Console.WriteLine("try again");
                            Console.ReadLine();
                            EditDevice();
                        }
                    }

                    catch (Exception)
                    {
                        Console.WriteLine("Something went wrong try again");
                        Console.ReadLine();
                        EditDevice();/// catch only works when string is entered not numbers outside the list
                    }
                    try
                    {
                        Console.WriteLine("Would you like to add another device yes or no");
                        string repeat = Console.ReadLine().Trim().ToUpper();  /// converts to upper case and removes any extra spaces
                        if (repeat == "yes".ToUpper())
                        {
                            EditDevice();

                        }

                        else if (repeat == "no".ToUpper())
                        {
                            FinishOption();

                        }
                        else
                        {
                            Console.WriteLine("inconsistent data has been entered therefore it is assumed that");
                            FinishOption();
                        }
                    }
                    catch
                    {
                        FinishOption();
                    }
                }
            }
                    
            public void SearchDevice()
            {
                StartOption("Search for a device on the system");
                /// searching will be by name at this stage but will be updated to include searching by ip
                Console.WriteLine("Enter the name of the device you would like to search for");
                string nameinput = Console.ReadLine();

                if (!string.IsNullOrEmpty(nameinput)) /// ensures that something is entered
                {
                    for (int i = 0; i < network.Count; i++)
                    {
                        if (network[i].Name.ToLower().Equals(nameinput))
                        {
                            Console.WriteLine(network[i].Details());
                        }
                    }
                }
                FinishOption();

            }
            public void UpdateStatus() 
            {
                StartOption("");
                Console.WriteLine("Update Device Status");
                Device d = network[1];
                Status dstatus = d as Status;
                if (dstatus != null)
                {
                    Console.WriteLine();
                }
               
                FinishOption();
            }
            public void SortDevice()
            {
                StartOption("Sorting Device on the system");
               
                ShowDevice();
                Console.WriteLine(Environment.NewLine +"devices can only be sorted into alphabetical order based on the name");
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
                Console.WriteLine(Environment.NewLine +"You have finsished viewing this option press any key to return to the main menu");
                Console.ReadLine();
            }
            public void ShowDevice()
            {
                int i = 0;
                foreach (Device device in network)
                {
                    i++;
                    Console.WriteLine(i + "." + device.Details());
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
        }
        static void Main(string[] args)
        {
          
            
           Console.Clear();  
           Manager manager = new Manager(); /// creates the menu
           

        }
    }
}
