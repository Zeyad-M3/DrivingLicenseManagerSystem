using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ContactsDataAccessLayer
{
    public class clsTestType
    {
        public int TestTypeID { get; set; }
        public string TestTypeName { get; set; }
        public string TestDescription { get; set; }
        public decimal TestFees { get; set; }
    }

    public class clsTestTypeData
    {
        public static List<clsTestType> GetAllTestTypeData()
        {
            List<clsTestType> testTypes = new List<clsTestType>();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("select * from testType", connection))
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
                            TestTypeName = reader.GetString(1),
                            TestDescription = reader.GetString(2) ,
                            TestFees = reader.GetDecimal(3)

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

        // get test type by ID  
        public static clsTestType GetTestTypeById(int testTypeId)
        {
            clsTestType testType = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SELECT * FROM TestType WHERE TestTypeID = @TestTypeID", connection))
            {
                command.Parameters.AddWithValue("@TestTypeID", testTypeId);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        testType = new clsTestType
                        {
                            TestTypeID = reader.GetInt32(0),
                            TestTypeName = reader.GetString(1),
                            TestDescription = reader.GetString(2),
                            TestFees = reader.GetDecimal(3)
                        };
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
            return testType;
        }

        // update test type by ID
        public static bool UpdateTestType(clsTestType testType)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("UPDATE TestType SET TestName = @TestTypeName, description = @TestDescription, Fees = @TestFees WHERE TestTypeID = @TestTypeID", connection))
            {
                command.Parameters.AddWithValue("@TestTypeID", testType.TestTypeID);
                command.Parameters.AddWithValue("@TestTypeName", testType.TestTypeName);
                command.Parameters.AddWithValue("@TestDescription", testType.TestDescription);
                command.Parameters.AddWithValue("@TestFees", testType.TestFees);
                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
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
