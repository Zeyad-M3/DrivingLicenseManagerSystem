
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ContactsDataAccessLayer
{
    public class clsApplication
    {
        public int Id { get; set; }
        public int PersonID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public int ApplicationType { get; set; }
        public string Status { get; set; }
        public decimal ApplicationFee { get; set; }
        public DateTime PaymentDate { get; set; }
        public int LicenseClassID { get; set; }
        public int? Score { get; set; } // إضافة Score كقابل للـ null
    }

    public class clsApplicationData
    {
        public static List<clsApplication> GetAllApplicationData()
        {
            List<clsApplication> applications = new List<clsApplication>();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"
                SELECT 
                    a.ApplicationID AS ApplicationId,
                    a.PersonID,
                    a.ApplicationDate,
                    a.ApplicationType,
                    a.Status,
                    a.ApplicationFee,
                    a.PaymentDate,
                    a.LicenseClassID,
                    t.Score
                FROM Applications a
                LEFT JOIN Tests t ON a.PersonID = t.PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    applications.Add(MapReaderToApplication(reader));
                }
            }
            finally
            {
                connection.Close();
            }
            return applications;
        }

        public static clsApplication GetApplicationById(int id)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"
                SELECT 
                    a.ApplicationID AS ApplicationId,
                    a.PersonID,
                    a.ApplicationDate,
                    a.ApplicationType,
                    a.Status,
                    a.ApplicationFee,
                    a.PaymentDate,
                    a.LicenseClassID,
                    t.Score
                FROM Applications a
                LEFT JOIN Tests t ON a.PersonID = t.PersonID
                WHERE a.ApplicationID = @Id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return MapReaderToApplication(reader);
                }
            }
            finally
            {
                connection.Close();
            }
            return null;
        }

        public static int AddApplication(clsApplication application)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"INSERT INTO Applications 
                    (PersonID, ApplicationDate, ApplicationType, Status, ApplicationFee, PaymentDate, LicenseClassID) 
                    VALUES (@PersonID, @ApplicationDate, @ApplicationType, @Status, @ApplicationFee, @PaymentDate, @LicenseClassID);
                    SELECT SCOPE_IDENTITY();";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SetApplicationParams(command, application);
                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        return result != null ? Convert.ToInt32(result) : 0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error adding application: " + ex.Message);
                        return 0;
                    }
                }
            }
        }

        public static bool UpdateApplication(clsApplication application)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"UPDATE Applications 
                    SET PersonID = @PersonID, ApplicationDate = @ApplicationDate, ApplicationType = @ApplicationType, 
                        Status = @Status, ApplicationFee = @ApplicationFee, PaymentDate = @PaymentDate, 
                        LicenseClassID = @LicenseClassID 
                    WHERE ApplicationID = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SetApplicationParams(command, application);
                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error updating application: " + ex.Message);
                        return false;
                    }
                }
            }
        }

        public static bool DeleteApplication(int id)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "DELETE FROM Applications WHERE ApplicationID = @Id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            finally
            {
                connection.Close();
            }
        }

        public static List<clsApplication> GetApplicationByPersonID(int personId)
        {
            List<clsApplication> applications = new List<clsApplication>();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"
                SELECT 
                    a.ApplicationID AS ApplicationId,
                    a.PersonID,
                    a.ApplicationDate,
                    a.ApplicationType,
                    a.Status,
                    a.ApplicationFee,
                    a.PaymentDate,
                    a.LicenseClassID,
                    t.Score
                FROM Applications a
                LEFT JOIN Tests t ON a.PersonID = t.PersonID
                WHERE a.PersonID = @PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", personId);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    applications.Add(MapReaderToApplication(reader));
                }
            }
            finally
            {
                connection.Close();
            }
            return applications;
        }

        // Helper Methods:
        private static clsApplication MapReaderToApplication(SqlDataReader reader)
        {
            try
            {
                return new clsApplication
                {
                    Id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                    PersonID = reader.IsDBNull(1) ? 0 : reader.GetInt32(1),
                    ApplicationDate = reader.IsDBNull(2) ? DateTime.MinValue : reader.GetDateTime(2),
                    ApplicationType = reader.IsDBNull(3) ? 0 : reader.GetInt32(3),
                    Status = reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
                    ApplicationFee = reader.IsDBNull(5) ? 0m : reader.GetDecimal(5),
                    PaymentDate = reader.IsDBNull(6) ? DateTime.MinValue : reader.GetDateTime(6),
                    LicenseClassID = reader.IsDBNull(7) ? 0 : reader.GetInt32(7),
                    Score = reader.IsDBNull(8) ? (int?)null : reader.GetInt32(8)
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in MapReaderToApplication: {ex.Message}");
                return new clsApplication
                {
                    Id = 0,
                    PersonID = 0,
                    ApplicationDate = DateTime.MinValue,
                    ApplicationType = 0,
                    Status = string.Empty,
                    ApplicationFee = 0m,
                    PaymentDate = DateTime.MinValue,
                    LicenseClassID = 0,
                    Score = null
                };
            }
        }

        private static void SetApplicationParams(SqlCommand command, clsApplication application)
        {
            command.Parameters.AddWithValue("@Id", application.Id);
            command.Parameters.AddWithValue("@PersonID", application.PersonID);
            command.Parameters.AddWithValue("@ApplicationDate", application.ApplicationDate != DateTime.MinValue ? (object)application.ApplicationDate : DBNull.Value);
            command.Parameters.AddWithValue("@ApplicationType", application.ApplicationType);
            command.Parameters.AddWithValue("@Status", (object)application.Status ?? DBNull.Value);
            command.Parameters.AddWithValue("@ApplicationFee", application.ApplicationFee);
            command.Parameters.AddWithValue("@PaymentDate", application.PaymentDate != DateTime.MinValue ? (object)application.PaymentDate : DBNull.Value);
            command.Parameters.AddWithValue("@LicenseClassID", application.LicenseClassID);
            command.Parameters.AddWithValue("@Score", (object)application.Score ?? DBNull.Value); // إضافة معالجة Score
        }
    }
}
        

