using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ContactsDataAccessLayer.clsLicenseData;
using static ContactsDataAccessLayer.DriversData;

namespace ContactsDataAccessLayer
{
    public class DriversData
    {   public class Driver
        {
            public int DriverId { get; set; }
            public int PersonId { get; set; }
            public string NationalNO { get; set; }
            public string FullName { get; set; }
            public DateTime Date { get; set; }
            public bool Active_Licenses { get; set; }
            public int LicenseID { get; set; }

        }
        // Change the call from ReadLicense(reader) to a method that is accessible and returns a Driver object.
        // If ReadLicense is intended to be used here, its access modifier should be changed to public in clsLicenseData.
        // Alternatively, implement a private method in DriversData to read a Driver from SqlDataReader.
        // get drivers by PersonId
        public static List<Driver> GetDriversByPersonId(int personId)
        {
            List<Driver> drivers = new List<Driver>();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Drivers WHERE PersonID = @PersonId", connection);
                command.Parameters.AddWithValue("@PersonId", personId);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        drivers.Add(ReadDriver(reader)); // Use a local method to read Driver
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            return drivers;
        }

        // get Driver by License ID
        public static Driver GetDriverByLicenseId(int licenseId)
        {
            Driver driver = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Drivers WHERE LicenseID = @LicenseID", connection);
                command.Parameters.AddWithValue("@LicenseID", licenseId);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        driver = ReadDriver(reader); // Use a local method to read Driver
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            return driver;
        }
        public static List<Driver> GetAllDrivers()
        {
            List<Driver> drivers = new List<Driver>();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand("select * from Drivers", connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        drivers.Add(ReadDriver(reader)); // Use a local method to read Driver
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return drivers;
        }

        public static Driver GetDriverById(int driverId)
        {
            Driver driver = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Drivers WHERE DriverId = @DriverId", connection);
                command.Parameters.AddWithValue("@DriverId", driverId);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        driver = ReadDriver(reader); // Use a local method to read Driver
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            return driver;
        }

        // add new Driver
        public static int AddNewDriver(Driver driver)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand("INSERT INTO Drivers (PersonId, [National NO], FullName, Date, LicenseID,[Active Licenses]) " +
                                                    "VALUES (@PersonId, @NationalNO, @FullName, @Date, @LicenseID,@Active_Licenses); " +
                                                    "SELECT SCOPE_IDENTITY();", connection);
                command.Parameters.AddWithValue("@PersonId", driver.PersonId);
                command.Parameters.AddWithValue("@NationalNO", driver.NationalNO);
                command.Parameters.AddWithValue("@FullName", driver.FullName);
                command.Parameters.AddWithValue("@Date", driver.Date);
                command.Parameters.AddWithValue("@LicenseID", driver.LicenseID);
                command.Parameters.AddWithValue("@Active_Licenses", driver.Active_Licenses); // إضافة المعامل

                try
                {
                    connection.Open();
                    return Convert.ToInt32(command.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    return -1; // Indicate failure
                }
                
            }
        }

        // Add this private method to DriversData to read a Driver from SqlDataReader
        private static Driver ReadDriver(SqlDataReader reader)
        {
            return new Driver
            {
                DriverId = reader.GetInt32(reader.GetOrdinal("DriverId")),
                PersonId = reader.GetInt32(reader.GetOrdinal("PersonId")),
                NationalNO = reader.IsDBNull(reader.GetOrdinal("NationalNO")) ? null : reader.GetString(reader.GetOrdinal("NationalNO")),
                FullName = reader.IsDBNull(reader.GetOrdinal("FullName")) ? null : reader.GetString(reader.GetOrdinal("FullName")),
                Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                LicenseID = reader.IsDBNull(reader.GetOrdinal("LicenseID")) ? 0 : reader.GetInt32(reader.GetOrdinal("LicenseID")),
                Active_Licenses = reader.IsDBNull(reader.GetOrdinal("ActiveLicenses")) ? false : reader.GetBoolean(reader.GetOrdinal("ActiveLicenses"))
            };
        }

    }

    
}
