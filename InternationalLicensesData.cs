using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ContactsDataAccessLayer
{
    public class clsInternationalLicensesData
    {
        public class clsInternationalLicense
        {
            public int Id { get; set; }
            public int LicenseID { get; set; }
            public DateTime IssueDate { get; set; }
            public DateTime ExpiryDate { get; set; }
            public string Status { get; set; }
            public int PreviousInternationalLicenseID { get; set; }
        }

        public static bool IsInternationalLicenseAvailableByID(int id)
        {
            bool isAvailable = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM InternationalLicenses WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);
            try
            {
                connection.Open();
                int count = (int)command.ExecuteScalar();
                isAvailable = count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return isAvailable;
        }

        public static List<clsInternationalLicense> GetAllInternationalLicenses()
        {
            List<clsInternationalLicense> licenses = new List<clsInternationalLicense>();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand("SELECT * FROM InternationalLicenses", connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    clsInternationalLicense license = new clsInternationalLicense
                    {
                        Id = reader.GetInt32(0),
                        LicenseID = reader.GetInt32(1),
                        IssueDate = reader.GetDateTime(2),
                        ExpiryDate = reader.GetDateTime(3),
                        Status = reader.GetString(4),
                        PreviousInternationalLicenseID = reader.GetInt32(5)
                    };
                    licenses.Add(license);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return licenses;
        }

        public static bool AddInternationalLicense(clsInternationalLicense license)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(
                "INSERT INTO InternationalLicenses (LicenseType, IssuedDate, ExpiryDate, Status, PreviousInternaTionLicenseID) " +
                "VALUES (@LicenseType, @IssuedDate, @ExpiryDate, @Status, @PreviousInternaTionLicenseID)", connection);
            command.Parameters.AddWithValue("@LicenseType", license.LicenseID);
            command.Parameters.AddWithValue("@IssuedDate", license.IssueDate);
            command.Parameters.AddWithValue("@ExpiryDate", license.ExpiryDate);
            command.Parameters.AddWithValue("@Status", license.Status);
            command.Parameters.AddWithValue("@PreviousInternaTionLicenseID", license.PreviousInternationalLicenseID);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool UpdateInternationalLicense(clsInternationalLicense license)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(
                "UPDATE InternationalLicenses SET LicenseType = @LicenseType, IssuedDate = @IssuedDate, " +
                "ExpiryDate = @ExpiryDate, Status = @Status, PreviousInternaTionLicenseID = @PreviousInternaTionLicenseID " +
                "WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", license.Id);
            command.Parameters.AddWithValue("@LicenseType", license.LicenseID);
            command.Parameters.AddWithValue("@IssuedDate", license.IssueDate);
            command.Parameters.AddWithValue("@ExpiryDate", license.ExpiryDate);
            command.Parameters.AddWithValue("@Status", license.Status);
            command.Parameters.AddWithValue("@PreviousInternaTionLicenseID", license.PreviousInternationalLicenseID);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool DeleteInternationalLicense(int id)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand("DELETE FROM InternationalLicenses WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public static clsInternationalLicense GetInternationalLicenseByID(int id)
        {
            clsInternationalLicense license = null;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand("SELECT * FROM InternationalLicenses WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    license = new clsInternationalLicense
                    {
                        Id = reader.GetInt32(0),
                        LicenseID = reader.GetInt32(1),
                        IssueDate = reader.GetDateTime(2),
                        ExpiryDate = reader.GetDateTime(3),
                        Status = reader.GetString(4),
                        PreviousInternationalLicenseID = reader.GetInt32(5)
                    };
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return license;
        }

        public static List<clsInternationalLicense> GetInternationalLicensesByLicenseID(int licenseID)
        {
            List<clsInternationalLicense> licenses = new List<clsInternationalLicense>();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand("SELECT * FROM InternationalLicenses WHERE LicenseType = @LicenseType", connection);
            command.Parameters.AddWithValue("@LicenseType", licenseID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    clsInternationalLicense license = new clsInternationalLicense
                    {
                        Id = reader.GetInt32(0),
                        LicenseID = reader.GetInt32(1),
                        IssueDate = reader.GetDateTime(2),
                        ExpiryDate = reader.GetDateTime(3),
                        Status = reader.GetString(4),
                        PreviousInternationalLicenseID = reader.GetInt32(5)
                    };
                    licenses.Add(license);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return licenses;
        }
    }
}
