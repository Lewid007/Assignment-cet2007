using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_cet2007
{
    public interface IComaprable
    {
        int CompareTo(object obj);
    }
    /// <summary>
    /// The Device class allows objects to be made within the system and it inherits from the status class aswell as two interfaces to allow for searching of devices and implementing the status of devices.
    /// </summary>
    public class Device : Status, IComparable<Device>, Istate<Device>
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

    public interface Istate<Device>
    {
        void ShowStatus();
    }
    /// <summary>
    ///  This Class is responsible for showing the status of devices. It is the parent class as all devices must have a status
    /// </summary>
    public class Status
    {
        public string DeviceStatus { get; set; }


        public Status(string deviceStatus)
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
}
