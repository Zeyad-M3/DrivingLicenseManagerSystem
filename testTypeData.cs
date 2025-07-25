using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ContactsDataAccessLayer
{
    public class clsTestType
    {
        public int TestTypeID { get; set; }
        public string TestTypeName { get; set; }
    }

    public class clsTestTypeData
    {
        public static List<clsTestType> GetAllTestTypeData()
        {
            List<clsTestType> testTypes = new List<clsTestType>();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SELECT * FROM TestType", connection))
            {
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        clsTestType testType = new clsTestType
                        {
                            TestTypeID = reader.GetInt32(0),
                            TestTypeName = reader.GetString(1)
                        };
                        testTypes.Add(testType);
                    }
                    reader.Close();
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

            return testTypes;
        }

        public static bool AddTestTypeData(clsTestType testType)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("INSERT INTO TestType (TestTypeID, TestTypeName) VALUES (@TestTypeID, @TestTypeName)", connection))
            {
                command.Parameters.AddWithValue("@TestTypeID", testType.TestTypeID);
                command.Parameters.AddWithValue("@TestTypeName", testType.TestTypeName);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("SQL Error: " + ex.Message);
                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    return false;
                }
            }
        }
    }
}
