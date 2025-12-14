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
    /// <summary>
    /// This class is responsible for controlling the menu system within the project
    /// </summary>
    public class Manager
    {
        public int MenuOption { get; private set; }
        public int Num { get; private set; }
        public string NameInput { get; set; }
        string ipinput { get; set; }
        public static List<Device> Network = new List<Device>();

        /// <summary>
        /// creates and instance of the manager class
        /// </summary>
        public Manager()
        { 
            FileSystem.FileDevice(Network);
            PrintMenu();

        }

        /// <summary>
        /// This method will display the menu and take a user input and match it to the correct menu choice they pick.  
        /// </summary>
        public void PrintMenu()
        {
            Logger.GetInstance().Log("Menu Loaded successfully");
            Console.WriteLine("WELCOME" + Environment.NewLine);
            /// Menu is created by using string array to make adding and updating menu choice items easier in the future
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

            /// This section of code will link up the menu choice to the corresponding features with appropriate error handling used.
            try
            {
                Console.WriteLine(Environment.NewLine + "Enter your menu option here");
                int Menu_option = Convert.ToInt32(Console.ReadLine());


                if (Menu_option < 1 || Menu_option > menuOptions.Length)
                {

                    MenuFunctionality.InvalidData();
                    Logger.GetInstance().Log("User has entered invalid data for the menu option");
                    PrintMenu();
                }
                else if (Menu_option == 1)
                {
                    Logger.GetInstance().Log("User has succesfully choesen to view all devices");
                    MenuFunctionality.ViewAll();
                }
                else if (Menu_option == 2)
                {
                    Logger.GetInstance().Log("User has succesfully chosen to add a new device to the sytem");
                    MenuFunctionality.AddDevice();
                }
                else if (Menu_option == 3)
                {
                    Logger.GetInstance().Log("User has succesfully chosen to edit a device on the system");
                    MenuFunctionality.EditDevice();
                }
                else if (Menu_option == 4)
                {
                    Logger.GetInstance().Log("User has succesfully chosen to Search for a device on the system");
                    MenuFunctionality.SearchDevice();
                }
                else if (Menu_option == 5)
                {
                    Logger.GetInstance().Log("User has succesfully chosen to update the device status on the system");
                    MenuFunctionality.UpdateStatus();
                }
                else if (Menu_option == 6)
                {
                    Logger.GetInstance().Log("User has succesfully chosen to sort the devices on the system");
                    MenuFunctionality.SortDevice();
                }
                else if (Menu_option == 7)
                {
                    Logger.GetInstance().Log("User has succesfully chosen to remove a device on the system");
                    MenuFunctionality.RemoveDevice();
                }
                else if (Menu_option == 8)
                {
                    Logger.GetInstance().Log("User has succesfully chosen to view the sytstem health");
                    MenuFunctionality.ViewHealth();
                }
                else if (Menu_option == 9)
                {
                    Logger.GetInstance().Log("User has succesfully chosen to quit the system");
                    MenuFunctionality.Quit();
                }
            }
            catch(Exception e)

            {
                Console.WriteLine("error");

            }

            Console.Clear();
            PrintMenu();
        }
    }
/// <summary>
///  Main program is very short due to the the dependncy on the Manager class that manages the entire menu system.
/// </summary>
    internal class Program
    {

        
        static void Main(string[] args)
        {

            Console.Clear();
            Manager manager = new Manager();
            /// creates the menu


        }
    }
}
