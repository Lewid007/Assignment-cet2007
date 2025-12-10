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
    public class Manager
    {
        public int Menu_option { get; private set; }
        public int num { get; private set; }
        public string nameinput { get; set; }
        string ipinput { get; set; }
        public static List<Device> network = new List<Device>();

        /// <summary>
        /// creates and instance of the manager class
        /// </summary>
        /// 

        public Manager()

        {




            FileSystem.FileDevice(network);
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

                    menuFunctionality.InvalidData();
                    Logger.GetInstance().Log("User has entered invalid data for the menu option");


                    PrintMenu();
                }
                else if (Menu_option == 1)
                {



                    Logger.GetInstance().Log("User has succesfully choesen to view all devices");
                    menuFunctionality.ViewAll();
                }
                else if (Menu_option == 2)
                {
                    Logger.GetInstance().Log("User has succesfully chosen to add a new device to the sytem");
                    menuFunctionality.AddDevice();
                }
                else if (Menu_option == 3)
                {
                    Logger.GetInstance().Log("User has succesfully chosen to edit a device on the system");
                    menuFunctionality.EditDevice();
                }
                else if (Menu_option == 4)
                {
                    Logger.GetInstance().Log("User has succesfully chosen to Search for a device on the system");
                    menuFunctionality.SearchDevice();
                }
                else if (Menu_option == 5)
                {
                    Logger.GetInstance().Log("User has succesfully chosen to update the device status on the system");
                    menuFunctionality.UpdateStatus();
                }
                else if (Menu_option == 6)
                {
                    Logger.GetInstance().Log("User has succesfully chosen to sort the devices on the system");
                    menuFunctionality.SortDevice();
                }
                else if (Menu_option == 7)
                {
                    Logger.GetInstance().Log("User has succesfully chosen to remove a device on the system");
                    menuFunctionality.RemoveDevice();
                }
                else if (Menu_option == 8)
                {
                    Logger.GetInstance().Log("User has succesfully chosen to view the sytstem health");
                    menuFunctionality.ViewHealth();
                }
                else if (Menu_option == 9)
                {
                    Logger.GetInstance().Log("User has succesfully chosen to quit the system");
                    menuFunctionality.Quit();
                }
            }
            catch

            {
                Console.WriteLine("error");

            }

            Console.Clear();
            PrintMenu();



        }



}
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
