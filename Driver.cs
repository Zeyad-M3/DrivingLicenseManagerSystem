using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactsDataAccessLayer;

namespace ContactsBusinessLayer
{
    public class Driver
    {

        public int DriverId { get; set; }
        public int PersonId { get; set; }
        public string NationalNO { get; set; }
        public string FullName { get; set; }
        public DateTime Date { get; set; }
        public bool Active_Licenses { get; set; }
        public int LicenseID { get; set; }



        public static List<Driver> GetalltheDrivers()
        {
            List<Driver> drivers = new List<Driver>();
            var dataList = DriversData.GetAllDrivers();
            foreach (var item in dataList)
            {
                drivers.Add(MapToBusiness(item));
            }
            return drivers;

        }
        public static Driver GetDriverById(int driverId)
        {
            var dataDriver = DriversData.GetDriverById(driverId);
            return dataDriver != null ? MapToBusiness(dataDriver) : null;
        }
        public static List<Driver> GetDriversByPersonId(int personId)
        {
            List<Driver> drivers = new List<Driver>();
            var dataList = DriversData.GetDriversByPersonId(personId);
            foreach (var item in dataList)
            {
                drivers.Add(MapToBusiness(item));
            }
            return drivers;
        }

        // add New Driver
      public static bool AddNewDriver(Driver driver)
        {
            if (driver == null) return false;
            var dataDriver = new ContactsDataAccessLayer.DriversData.Driver
            {
                DriverId = driver.DriverId,
                PersonId = driver.PersonId,
                NationalNO = driver.NationalNO,
                FullName = driver.FullName,
                Date = driver.Date,
                LicenseID = driver.LicenseID
            };
           
            return DriversData.AddNewDriver(dataDriver) > 0;
            
        }


        // get Driver by License ID
        public static Driver GetDriverByLicenseId(int licenseId)
        {
            var dataDriver = DriversData.GetDriverByLicenseId(licenseId);
            return dataDriver != null ? MapToBusiness(dataDriver) : null;
        }


        // Add this private static method to map DriversData.Driver to ContactsBusinessLayer.Driver
        private static Driver MapToBusiness(ContactsDataAccessLayer.DriversData.Driver dataDriver)
        {
            return new Driver
            {
                DriverId = dataDriver.DriverId,
                PersonId = dataDriver.PersonId,
                NationalNO = dataDriver.NationalNO,
                FullName = dataDriver.FullName,
                Date = dataDriver.Date,
                LicenseID = dataDriver.LicenseID
                // Note: Active_Licenses is private in DriversData.Driver and Driver, so cannot be mapped directly.
            };
        }
    }
}
