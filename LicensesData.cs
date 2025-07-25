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
            public string LicenseNumber { get; set; }
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
        //GetLicenseByPersonId
        public static clsLicense GetLicenseByPersonId(int personId)
        {
            clsLicense license = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Licenses WHERE PersonID = @PersonId", connection);
                command.Parameters.AddWithValue("@PersonId", personId);
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

        public static int AddLicense(clsLicense license)
        {
            int newId = 0;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(
                    "INSERT INTO Licenses (PersonID, LicenseNumber, ApplicationType, Photo, IssueDate, ExpiryDate, Conditions, IssueStatus) " +
                    "OUTPUT INSERTED.LicenseID VALUES (@PersonId, @LicenseNumber, @ApplicationType, @Photo, @IssueDate, @ExpiryDate, @Conditions, @IssueStatus)",
                    connection);

                // إضافة المعاملات مع التحقق من القيم
                command.Parameters.AddWithValue("@PersonId", (object)license.PersonId ?? DBNull.Value);
                command.Parameters.AddWithValue("@LicenseNumber", (object)license.LicenseNumber ?? DBNull.Value);
                command.Parameters.AddWithValue("@ApplicationType", (object)license.ApplicationType ?? DBNull.Value);
                command.Parameters.AddWithValue("@Photo", (object)license.PhotoPath ?? DBNull.Value);
                command.Parameters.AddWithValue("@IssueDate", (object)license.IssueDate ?? DBNull.Value);
                command.Parameters.AddWithValue("@ExpiryDate", (object)license.ExpiryDate ?? DBNull.Value);
                command.Parameters.AddWithValue("@Conditions", (object)license.Conditions ?? DBNull.Value);
                command.Parameters.AddWithValue("@IssueStatus", (object)license.IssueStatus ?? DBNull.Value);

                // تسجيل القيم لتتبع المشكلة
                Console.WriteLine($"Adding license: PersonId={license.PersonId}, LicenseNumber={license.LicenseNumber}, ApplicationType={license.ApplicationType}, PhotoPath={license.PhotoPath}, IssueDate={license.IssueDate}, ExpiryDate={license.ExpiryDate}, Conditions={license.Conditions}, IssueStatus={license.IssueStatus}");

                try
                {
                    connection.Open();
                    newId = (int)command.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error adding license: {ex.Message}");
                }
            }
            return newId;
        }

        //update License
        public static bool UpdateLicense(clsLicense license)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(
                    "UPDATE Licenses SET PersonID = @PersonId, LicenseNumber = @LicenseNumber, ApplicationType = @ApplicationType, Photo = @Photo, IssueDate = @IssueDate, ExpiryDate = @ExpiryDate, Conditions = @Conditions, IssueStatus = @IssueStatus WHERE LicenseID = @LicenseId",
                    connection);
                command.Parameters.AddWithValue("@LicenseId", license.LicenseId);
                command.Parameters.AddWithValue("@PersonId", license.PersonId);
                command.Parameters.AddWithValue("@LicenseNumber", license.LicenseNumber);
                command.Parameters.AddWithValue("@ApplicationType", license.ApplicationType);
                command.Parameters.AddWithValue("@Photo", license.PhotoPath);
                command.Parameters.AddWithValue("@IssueDate", license.IssueDate);
                command.Parameters.AddWithValue("@ExpiryDate", license.ExpiryDate);
                command.Parameters.AddWithValue("@Conditions", license.Conditions);
                command.Parameters.AddWithValue("@IssueStatus", license.IssueStatus);
                try
                {
                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error updating license: " + ex.Message);
                    return false;
                }
            }
        }


        private static clsLicense ReadLicense(SqlDataReader reader)
        {
            return new clsLicense
            {
                LicenseId = reader.GetInt32(0),
                PersonId = reader.GetInt32(1),
                LicenseNumber = reader.GetString(2),
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
