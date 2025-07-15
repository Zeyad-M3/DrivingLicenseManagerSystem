using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ContactsDataAccessLayer
{
    public class clsPerson
    {
        public int PersonId { get; set; }
        public string NationalID { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Nationality { get; set; }
        public string Photo { get; set; }
    }

    public class clsPersonData
    {
        public static List<clsPerson> GetAllPersonData()
        {
            List<clsPerson> persons = new List<clsPerson>();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand("SELECT * FROM Persons", connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    persons.Add(MapReaderToPerson(reader));
                }
            }
            finally
            {
                connection.Close();
            }
            return persons;
        }

        public static clsPerson GetPersonById(int personId)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Persons WHERE PersonId = @PersonId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonId", personId);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return MapReaderToPerson(reader);
                }
            }
            finally
            {
                connection.Close();
            }
            return null;
        }

        public static bool AddPerson(clsPerson person)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"INSERT INTO Persons 
            (PersonId, NationalID, FullName, DateOfBirth, Address, PhoneNumber, Email, Nationality, Photo) 
            VALUES (@PersonId, @NationalID, @FullName, @DateOfBirth, @Address, @PhoneNumber, @Email, @Nationality, @Photo)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SetPersonParams(command, person);
                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error adding person: " + ex.Message);
                        return false;
                    }
                }
            }
        }

        //public static void UpdatePerson(clsPerson person)
        //{
        //    SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
        //    string query = @"UPDATE Persons SET NationalID = @NationalID, FullName = @FullName, 
        //        DateOfBirth = @DateOfBirth, Address = @Address, PhoneNumber = @PhoneNumber, 
        //        Email = @Email, Nationality = @Nationality, Photo = @Photo 
        //        WHERE PersonId = @PersonId";
        //    SqlCommand command = new SqlCommand(query, connection);
        //    SetPersonParams(command, person);
        //    ExecuteCommand(command, connection);
        //}

        public static bool UpdatePerson(clsPerson person)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"UPDATE Persons 
            SET NationalID = @NationalID, FullName = @FullName, DateOfBirth = @DateOfBirth, 
                Address = @Address, PhoneNumber = @PhoneNumber, Email = @Email, 
                Nationality = @Nationality, Photo = @Photo 
            WHERE PersonId = @PersonId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SetPersonParams(command, person);
                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error updating person: " + ex.Message);
                        return false;
                    }
                }
            }
        }
        private static bool ExecuteCommand1(SqlCommand command, SqlConnection connection)
        {
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0; // ترجع true إذا تم تحديث صف واحد على الأقل
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in ExecuteCommand: {ex.Message}");
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool DeletePerson(int personId)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "DELETE FROM Persons WHERE PersonId = @PersonId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonId", personId);
            ExecuteCommand(command, connection);
            return true; // Assuming deletion is successful, you might want to check rows affected
        }

        public static List<clsPerson> GetPersonByNationalID(string nationalId)
        {
            List<clsPerson> persons = new List<clsPerson>();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Persons WHERE NationalID = @NationalID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalID", nationalId);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    persons.Add(MapReaderToPerson(reader));
                }
            }
            finally
            {
                connection.Close();
            }
            return persons;
        }

        public static List<clsPerson> GetPersonByNationality(string nationality)
        {
            List<clsPerson> persons = new List<clsPerson>();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Persons WHERE Nationality = @Nationality";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Nationality", nationality);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    persons.Add(MapReaderToPerson(reader));
                }
            }
            finally
            {
                connection.Close();
            }
            return persons;
        }

        // Helper Methods:
        private static clsPerson MapReaderToPerson(SqlDataReader reader)
        {
            return new clsPerson
            {
                PersonId = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                NationalID = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                FullName = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                DateOfBirth = reader.IsDBNull(3) ? DateTime.MinValue : reader.GetDateTime(3),
                Address = reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
                PhoneNumber = reader.IsDBNull(5) ? string.Empty : reader.GetString(5),
                Email = reader.IsDBNull(6) ? string.Empty : reader.GetString(6),
                Nationality = reader.IsDBNull(7) ? string.Empty : reader.GetString(7),
                Photo = reader.IsDBNull(8) ? string.Empty : reader.GetString(8)
            };
        }

        private static void SetPersonParams(SqlCommand command, clsPerson person)
        {
            command.Parameters.AddWithValue("@PersonId", person.PersonId);
            command.Parameters.AddWithValue("@NationalID", (object)person.NationalID ?? DBNull.Value);
            command.Parameters.AddWithValue("@FullName", (object)person.FullName ?? DBNull.Value);
            command.Parameters.AddWithValue("@DateOfBirth", (object)person.DateOfBirth ?? DBNull.Value);
            command.Parameters.AddWithValue("@Address", (object)person.Address ?? DBNull.Value);
            command.Parameters.AddWithValue("@PhoneNumber", (object)person.PhoneNumber ?? DBNull.Value);
            command.Parameters.AddWithValue("@Email", (object)person.Email ?? DBNull.Value);
            command.Parameters.AddWithValue("@Nationality", (object)person.Nationality ?? DBNull.Value);
            command.Parameters.AddWithValue("@Photo", (object)person.Photo ?? DBNull.Value); // التعامل مع null
        }

        private static void ExecuteCommand(SqlCommand command, SqlConnection connection)
        {
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
