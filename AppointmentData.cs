using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsDataAccessLayer
{
    public class clsAppointmentData
    {
        public static bool GetAppointmentById(int appointmentId)
        {
            bool appointmentFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Appointments WHERE AppointmentId = @AppointmentId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@AppointmentId", appointmentId);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    appointmentFound = true;
                    // Process the data as needed
                }
                reader.Close();
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions
                Console.WriteLine("SQL Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Handle general exceptions
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return appointmentFound;

        }

        public static bool AddAppointment(int appointmentId, string TestID, DateTime AppointmentDate,
            string Status, string Description, string CreatedByUserId)
        {
            bool appointmentAdded = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "INSERT INTO Appointments (AppointmentId, TestID,AppointmentDate, Status,Description,CreatedByUserId) VALUES (@AppointmentId, @TestID,@AppointmentDate,@Status,@Description,@CreatedByUserId)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@AppointmentId", appointmentId);
            command.Parameters.AddWithValue("@TestID", TestID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@Status", Status);
            command.Parameters.AddWithValue("@Description", Description);
            command.Parameters.AddWithValue("@CreatedByUserId", CreatedByUserId);

            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    appointmentAdded = true;
                }
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions
                Console.WriteLine("SQL Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Handle general exceptions
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return appointmentAdded;

        }

        public static bool UpdateAppointment(int appointmentId, string TestID, DateTime AppointmentDate,
            string Status, string Description, string CreatedByUserId)
        {
            bool appointmentUpdated = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "UPDATE Appointments SET TestID = @TestID, AppointmentDate = @AppointmentDate, Status = @Status, Description = @Description, CreatedByUserId = @CreatedByUserId WHERE AppointmentId = @AppointmentId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@AppointmentId", appointmentId);
            command.Parameters.AddWithValue("@TestID", TestID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@Status", Status);
            command.Parameters.AddWithValue("@Description", Description);
            command.Parameters.AddWithValue("@CreatedByUserId", CreatedByUserId);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    appointmentUpdated = true;
                }
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions
                Console.WriteLine("SQL Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Handle general exceptions
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return appointmentUpdated;
        }

        public static bool DeleteAppointment(int appointmentId)
        {
            bool appointmentDeleted = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "DELETE FROM Appointments WHERE AppointmentId = @AppointmentId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@AppointmentId", appointmentId);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    appointmentDeleted = true;
                }
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions
                Console.WriteLine("SQL Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Handle general exceptions
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return appointmentDeleted;
        }

        public static void GetAllAppointments()
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Appointments";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    // Process each appointment record
                    int appointmentId = reader.GetInt32(reader.GetOrdinal("AppointmentId"));
                    string testId = reader.GetString(reader.GetOrdinal("TestID"));
                    DateTime appointmentDate = reader.GetDateTime(reader.GetOrdinal("AppointmentDate"));
                    string status = reader.GetString(reader.GetOrdinal("Status"));
                    string description = reader.GetString(reader.GetOrdinal("Description"));
                    string createdByUserId = reader.GetString(reader.GetOrdinal("CreatedByUserId"));
                    // Output or store the data as needed
                    Console.WriteLine($"Appointment ID: {appointmentId}, Test ID: {testId}, Date: {appointmentDate}, Status: {status}, Description: {description}, Created By: {createdByUserId}");
                }
                reader.Close();
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions
                Console.WriteLine("SQL Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Handle general exceptions
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public static List<string> GetAppointmentStatuses()
        {
            List<string> statuses = new List<string>();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT DISTINCT Status FROM Appointments";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    statuses.Add(reader.GetString(0));
                }
                reader.Close();
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions
                Console.WriteLine("SQL Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Handle general exceptions
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return statuses;
        }

        // get appointment by created by user id
        public static void GetAppointmentsByCreatedByUserId(int createdByUserId)
        {
            int appointmentIds = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT AppointmentId FROM Appointments WHERE CreatedByUserId = @CreatedByUserId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CreatedByUserId", createdByUserId);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    appointmentIds = reader.GetInt32(reader.GetOrdinal("AppointmentId"));
                    // Output or store the data as needed
                    Console.WriteLine($"Appointment ID: {appointmentIds}");
                }
                reader.Close();
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions
                Console.WriteLine("SQL Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Handle general exceptions
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

        }

        public static void GetAppointmentsByTestId(string testId)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Appointments WHERE TestID = @TestID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestID", testId);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    // Process each appointment record
                    int appointmentId = reader.GetInt32(reader.GetOrdinal("AppointmentId"));
                    DateTime appointmentDate = reader.GetDateTime(reader.GetOrdinal("AppointmentDate"));
                    string status = reader.GetString(reader.GetOrdinal("Status"));
                    string description = reader.GetString(reader.GetOrdinal("Description"));
                    string createdByUserId = reader.GetString(reader.GetOrdinal("CreatedByUserId"));
                    // Output or store the data as needed
                    Console.WriteLine($"Appointment ID: {appointmentId}, Test ID: {testId}, Date: {appointmentDate}, Status: {status}, Description: {description}, Created By: {createdByUserId}");
                }
                reader.Close();
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions
                Console.WriteLine("SQL Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Handle general exceptions
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }


    }
}
