using System;
using System.Data;
using System.Data.SqlClient;
using ContactsDataAccessLayer;


namespace ContactsDataAccessLayer
{
    public class clsApplicationDataAccess
    {
        public static bool GetApplicationByID(int Id, ref int PersonID, ref string ApplicationDate, ref string ApplicationType, ref string status, ref string ApplicationFee, ref string PaymentDate, ref string licenseClassID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT PersonID, ApplicationDate, ApplicationType, Status, ApplicationFee, PaymentDate, LicenseClassID " +
                "FROM Applications WHERE Id = @Id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", Id);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    PersonID = reader.GetInt32(0);
                    ApplicationDate = reader.GetString(1);
                    ApplicationType = reader.GetString(2);
                    status = reader.GetString(3);
                    ApplicationFee = reader.GetString(4);
                    PaymentDate = reader.GetString(5);
                    licenseClassID = reader.GetString(6);
                    
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return isFound; // Ensure a return statement is present in all code paths
        }


        // get application by person id
        public static bool GetApplicationByPersonID(int personId)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Applications WHERE PersonID = @PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", personId);

            bool isFound = false; // Variable to check if any application is found for the person ID
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
                Console.WriteLine("An error occurred: " + ex.Message);
                isFound = false; // If an exception occurs, set isFound to false
            }
            finally
            {
                connection.Close();
            }
            return isFound ; // Return the DataTable if applications are found, otherwise return null

        }
        //Get all applications
        public static DataTable GetAllApplications()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Applications";

            SqlCommand command = new SqlCommand(query, connection);

            // using reader to fill the DataTable
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
              
                if(reader.HasRows)
                {
                    dt.Load(reader);

                }
                reader.Close();
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return dt; // Return the DataTable containing all applications


        }
        // get all applications by person id
        public static DataTable GetAllApplicationsData()
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT Id, PersonID, ApplicationDate, ApplicationType, Status, ApplicationFee, PaymentDate, LicenseClassID FROM Applications";
            SqlCommand command = new SqlCommand(query, connection);
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return dt; // Return the DataTable containing all applications


        }

        public static bool TestConnection()
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                try
                {
                    conn.Open();
                    return true;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Connection failed: " + ex.Message);
                    return false;
                }
            }
        }

        // count applications
        public static int CountApplications()
        {
            int count = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT COUNT(*) FROM Application";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                count = (int)command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return count; // Return the count of applications
        }

        // Method to add a new application
        public static int AddAnewApplication(int PersonID, string ApplicationDate, string ApplicationType, string Status, string ApplicationFee, string PaymentDate, string LicenseClassID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "INSERT INTO Applications (PersonID, ApplicationDate, ApplicationType, Status, ApplicationFee, PaymentDate, LicenseClassID) " +
                           "VALUES (@PersonID, @ApplicationDate, @ApplicationType, @Status, @ApplicationFee, @PaymentDate, @LicenseClassID); " +
                           "SELECT SCOPE_IDENTITY();"; // Get the ID of the newly inserted application
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationType", ApplicationType);
            command.Parameters.AddWithValue("@Status", Status);
            command.Parameters.AddWithValue("@ApplicationFee", ApplicationFee);
            command.Parameters.AddWithValue("@PaymentDate", PaymentDate);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            int newApplicationId = 0;
            try
            {
                connection.Open();
                newApplicationId = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return newApplicationId; // Return the ID of the newly added application

        }

        // Method to update an existing application
        public static bool UpdateApplication(int Id, int PersonID, string ApplicationDate, string ApplicationType, string Status, string ApplicationFee, string PaymentDate, string LicenseClassID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "UPDATE Applications SET PersonID = @PersonID, ApplicationDate = @ApplicationDate, " +
                           "ApplicationType = @ApplicationType, Status = @Status, ApplicationFee = @ApplicationFee, " +
                           "PaymentDate = @PaymentDate, LicenseClassID = @LicenseClassID WHERE Id = @Id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", Id);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationType", ApplicationType);
            command.Parameters.AddWithValue("@Status", Status);
            command.Parameters.AddWithValue("@ApplicationFee", ApplicationFee);
            command.Parameters.AddWithValue("@PaymentDate", PaymentDate);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0; // Return true if at least one row was updated
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
                Console.WriteLine("An error occurred: " + ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
        // Method to delete an application by ID
        public static bool DeleteApplication(int Id)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "DELETE FROM Applications WHERE Id = @Id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", Id);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0; // Return true if at least one row was deleted
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
                Console.WriteLine("An error occurred: " + ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
        //method to get application by ApplicationType
        public static bool GetApplicationTypebyName(string Id)
        {
           
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT ApplicationType FROM Applications WHERE ApplicationType = @ApplicationType";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationType", Id);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    return true; // Application type exists
                }
                else
                {
                    return false; // Application type does not exist
                }
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
                Console.WriteLine("An error occurred: " + ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }


        }

    }
}
