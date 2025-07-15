using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ContactsDataAccessLayer
{
    public static class clsLicenseClassData
    {
        public class clsLicenseClass
        {
            public int LicenseClassId { get; set; }
            public string LicenseClassName { get; set; }
            public string ClassDescription { get; set; }
            public short MinimumAge { get; set; }
            public short ValidityLength { get; set; }
            public short ClassFee { get; set; }
        }

        public static List<clsLicenseClass> GetAllLicenseClassData()
        {
            List<clsLicenseClass> licenseClasses = new List<clsLicenseClass>();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM LicenseClass", connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        clsLicenseClass licenseClass = new clsLicenseClass
                        {
                            LicenseClassId = reader.GetInt32(0),
                            LicenseClassName = reader.GetString(1),
                            ClassDescription = reader.GetString(2),
                            MinimumAge = reader.GetInt16(3),
                            ValidityLength = reader.GetInt16(4),
                            ClassFee = reader.GetInt16(5)
                        };
                        licenseClasses.Add(licenseClass);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("SQL Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return licenseClasses;
        }

        public static clsLicenseClass GetLicenseClassDataById(int licenseClassID)
        {
            clsLicenseClass licenseClass = null;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM LicenseClass WHERE LicenseClassId = @LicenseClassId", connection);
                command.Parameters.AddWithValue("@LicenseClassId", licenseClassID);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        licenseClass = new clsLicenseClass
                        {
                            LicenseClassId = reader.GetInt32(0),
                            LicenseClassName = reader.GetString(1),
                            ClassDescription = reader.GetString(2),
                            MinimumAge = reader.GetInt16(3),
                            ValidityLength = reader.GetInt16(4),
                            ClassFee = reader.GetInt16(5)
                        };
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("SQL Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return licenseClass;
        }

        public static clsLicenseClass GetLicenseClassDataByName(string licenseClassName)
        {
            clsLicenseClass licenseClass = null;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM LicenseClass WHERE LicenseClassName = @LicenseClassName", connection);
                command.Parameters.AddWithValue("@LicenseClassName", licenseClassName);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        licenseClass = new clsLicenseClass
                        {
                            LicenseClassId = reader.GetInt32(0),
                            LicenseClassName = reader.GetString(1),
                            ClassDescription = reader.GetString(2),
                            MinimumAge = reader.GetInt16(3),
                            ValidityLength = reader.GetInt16(4),
                            ClassFee = reader.GetInt16(5)
                        };
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("SQL Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return licenseClass;
        }
        // داخل clsLicenseClassData

        public static bool InsertLicenseClass(clsLicenseClass licenseClass)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(
                    "INSERT INTO LicenseClass (LicenseClassName, ClassDescription, MinimumAge, ValidityLength, ClassFee) " +
                    "VALUES (@LicenseClassName, @ClassDescription, @MinimumAge, @ValidityLength, @ClassFee)", connection);

                command.Parameters.AddWithValue("@LicenseClassName", licenseClass.LicenseClassName);
                command.Parameters.AddWithValue("@ClassDescription", licenseClass.ClassDescription);
                command.Parameters.AddWithValue("@MinimumAge", licenseClass.MinimumAge);
                command.Parameters.AddWithValue("@ValidityLength", licenseClass.ValidityLength);
                command.Parameters.AddWithValue("@ClassFee", licenseClass.ClassFee);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch
                {
                    return false;
                }
            }
        }

        public static bool UpdateLicenseClass(clsLicenseClass licenseClass)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(
                    "UPDATE LicenseClass SET LicenseClassName = @LicenseClassName, ClassDescription = @ClassDescription, " +
                    "MinimumAge = @MinimumAge, ValidityLength = @ValidityLength, ClassFee = @ClassFee WHERE LicenseClassId = @LicenseClassId", connection);

                command.Parameters.AddWithValue("@LicenseClassId", licenseClass.LicenseClassId);
                command.Parameters.AddWithValue("@LicenseClassName", licenseClass.LicenseClassName);
                command.Parameters.AddWithValue("@ClassDescription", licenseClass.ClassDescription);
                command.Parameters.AddWithValue("@MinimumAge", licenseClass.MinimumAge);
                command.Parameters.AddWithValue("@ValidityLength", licenseClass.ValidityLength);
                command.Parameters.AddWithValue("@ClassFee", licenseClass.ClassFee);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch
                {
                    return false;
                }
            }
        }

        public static bool DeleteLicenseClass(int licenseClassId)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand("DELETE FROM LicenseClass WHERE LicenseClassId = @LicenseClassId", connection);
                command.Parameters.AddWithValue("@LicenseClassId", licenseClassId);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch
                {
                    return false;
                }
            }
        }
        public static bool IsLicenseClassAvailableById(int licenseClassId)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM LicenseClass WHERE LicenseClassId = @LicenseClassId", connection);
                command.Parameters.AddWithValue("@LicenseClassId", licenseClassId);
                try
                {
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
