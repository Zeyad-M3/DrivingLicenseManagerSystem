using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ContactsDataAccessLayer
{
    public class clsUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string Role { get; set; }
        public string Description { get; set; }
    }

    public class clsUsersData
    {
        public static List<clsUser> GetAllUsersData()
        {
            List<clsUser> users = new List<clsUser>();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Users", connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new clsUser
                        {
                            UserId = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                            UserName = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                            UserPassword = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                            Role = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                            Description = reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
                        });
                    }
                }
            }
            return users;
        }

        public static clsUser GetUserByUserID(int userId)
        {
            clsUser user = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Users WHERE UserId = @UserId", connection);
                command.Parameters.AddWithValue("@UserId", userId);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    try
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                user = new clsUser
                                {
                                    UserId = reader.GetInt32(0), // First column: UserId
                                    UserName = reader.FieldCount > 2 ? reader.GetString(1) : string.Empty, // Third column: UserName
                                    UserPassword = reader.FieldCount > 3 ? reader.GetString(2) : string.Empty, // Fourth column: UserPassword
                                    Role = reader.FieldCount > 4 ? reader.GetString(3) : string.Empty, // Fifth column: Role
                                    Description = reader.FieldCount > 5 ? reader.GetString(4) : string.Empty // Sixth column: Description
                                };
                                // Use the user object here
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error reading user data: {ex.Message}");
                    }
                }
            }
            return user;
        }

        public static List<clsUser> GetUsersByPersonID(int personId)
        {
            return GetUsersByField("PersonID", personId);
        }

        public static List<clsUser> GetUsersByUserName(string userName)
        {
            return GetUsersByField("UserName", userName);
        }

        public static List<clsUser> GetUsersByRole(string role)
        {
            return GetUsersByField("Role", role);
        }

        private static List<clsUser> GetUsersByField(string fieldName, object value)
        {
            List<clsUser> users = new List<clsUser>();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand($"SELECT * FROM Users WHERE {fieldName} = @value", connection);
                command.Parameters.AddWithValue("@value", value);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new clsUser
                        {
                            UserId = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                            UserName = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                            UserPassword = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                            Role = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                            Description = reader.IsDBNull(4) ? string.Empty : reader.GetString(4)

                        });
                    }
                }
            }
            return users;
        }

        public static bool AddUserData(clsUser user)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(@"INSERT INTO Users 
                (UserName, [Password], Role, Description) 
                VALUES (@UserName, @Password, @Role, @Description)", connection);

                command.Parameters.AddWithValue("@UserName", user.UserName);
                command.Parameters.AddWithValue("@Password", user.UserPassword);
                command.Parameters.AddWithValue("@Role", user.Role);
                command.Parameters.AddWithValue("@Description", user.Description);

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        public static bool UpdateUserData(clsUser user)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(@"UPDATE Users 
                    SET UserName = @UserName, 
                        Password = @UserPassword, Role = @Role, Description = @Description 
                    WHERE UserId = @UserId", connection);

                command.Parameters.AddWithValue("@UserId", user.UserId);
                command.Parameters.AddWithValue("@UserName", user.UserName);
                command.Parameters.AddWithValue("@UserPassword", user.UserPassword);
                command.Parameters.AddWithValue("@Role", user.Role);
                command.Parameters.AddWithValue("@Description", user.Description);

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        public static bool DeleteUserData(int userId)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand("DELETE FROM Users WHERE UserId = @UserId", connection);
                command.Parameters.AddWithValue("@UserId", userId);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }
    }
}
