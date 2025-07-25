using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ContactsDataAccessLayer
{
    public class clsLicenseData
    {
        public class clsLicense
        {
            public int LicenseId { get; set; }
            public int PersonId { get; set; }
            public int LicenseNumber { get; set; }
            public string ApplicationType { get; set; }
            public string PhotoPath { get; set; }
            public DateTime IssueDate { get; set; }
            public DateTime ExpiryDate { get; set; }
            public string Conditions { get; set; }
            public string IssueStatus { get; set; }
        }

        public static List<clsLicense> GetAllLicenseData()
        {
            List<clsLicense> licenses = new List<clsLicense>();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Licenses", connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        licenses.Add(ReadLicense(reader));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return licenses;
        }

        public static clsLicense GetLicenseById(int licenseId)
        {
            clsLicense license = null;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Licenses WHERE LicenseID = @LicenseId", connection);
                command.Parameters.AddWithValue("@LicenseId", licenseId);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        license = ReadLicense(reader);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return license;
        }

        public static List<clsLicense> GetLicensesByPersonId(int personId)
        {
            List<clsLicense> licenses = new List<clsLicense>();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Licenses WHERE PersonID = @PersonId", connection);
                command.Parameters.AddWithValue("@PersonId", personId);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        licenses.Add(ReadLicense(reader));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return licenses;
        }

        public static List<clsLicense> GetLicensesByLicenseNumber(int licenseNumber)
        {
            List<clsLicense> licenses = new List<clsLicense>();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Licenses WHERE LicenseNumber = @LicenseNumber", connection);
                command.Parameters.AddWithValue("@LicenseNumber", licenseNumber);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        licenses.Add(ReadLicense(reader));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return licenses;
        }

        private static clsLicense ReadLicense(SqlDataReader reader)
        {
            return new clsLicense
            {
                LicenseId = reader.GetInt32(0),
                PersonId = reader.GetInt32(1),
                LicenseNumber = reader.GetInt32(2),
                ApplicationType = reader.GetString(3),
                PhotoPath = reader.GetString(4),
                IssueDate = reader.GetDateTime(5),
                ExpiryDate = reader.GetDateTime(6),
                Conditions = reader.GetString(7),
                IssueStatus = reader.GetString(8)
            };
        }
    }
}
