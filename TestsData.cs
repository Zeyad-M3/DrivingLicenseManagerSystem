using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ContactsDataAccessLayer
{
    public class clsTest
    {
        public int TestId { get; set; }
        public int ApplicationId { get; set; }
        public int TestTypeId { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime TestDate { get; set; }
        public int Score { get; set; }
        public string TestResult { get; set; }
        public short RetryCount { get; set; }
        public short QuestionCount { get; set; }
        public short Fees { get; set; }
        public string Description { get; set; }
        public int PersonId { get; set; }
    }
    public class clsTestsData
    {
        public static List<clsTest> GetAllTestsData()
        {
            List<clsTest> tests = new List<clsTest>();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Tests", connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        clsTest test = ReadTest(reader);
                        tests.Add(test);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            return tests;
        }

        public static clsTest GetTestById(int testId)
        {
            clsTest test = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Tests WHERE TestId = @TestId", connection);
                command.Parameters.AddWithValue("@TestId", testId);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        test = ReadTest(reader);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            return test;
        }

        public static List<clsTest> GetTestsByPersonId(int personId)
        {
            return GetTestsByCondition("PersonId", personId);
        }

        public static List<clsTest> GetTestsByApplicationId(int applicationId)
        {
            return GetTestsByCondition("ApplicationId", applicationId);
        }

        public static List<clsTest> GetTestsByTestTypeId(int testTypeId)
        {
            return GetTestsByCondition("TestTypeId", testTypeId);
        }

        private static List<clsTest> GetTestsByCondition(string fieldName, int value)
        {
            List<clsTest> tests = new List<clsTest>();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = $"SELECT * FROM Tests WHERE {fieldName} = @{fieldName}";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue($"@{fieldName}", value);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        clsTest test = ReadTest(reader);
                        tests.Add(test);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            return tests;
        }

        public static bool AddTest(clsTest test)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"INSERT INTO Tests (TestId, ApplicationId, TestTypeId, PaymentStatus, TestDate, Score, TestResult, RetryCount, QuestionCount, Fees, Description, PersonId) 
                                 VALUES (@TestId, @ApplicationId, @TestTypeId, @PaymentStatus, @TestDate, @Score, @TestResult, @RetryCount, @QuestionCount, @Fees, @Description, @PersonId)";
                SqlCommand command = new SqlCommand(query, connection);
                AddTestParameters(command, test);
                try
                {
                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    return false;
                }
            }
        }

        public static bool UpdateTest(clsTest test)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"UPDATE Tests SET ApplicationId = @ApplicationId, TestTypeId = @TestTypeId, PaymentStatus = @PaymentStatus, 
                                 TestDate = @TestDate, Score = @Score, TestResult = @TestResult, RetryCount = @RetryCount, QuestionCount = @QuestionCount, 
                                 Fees = @Fees, Description = @Description, PersonId = @PersonId WHERE TestId = @TestId";
                SqlCommand command = new SqlCommand(query, connection);
                AddTestParameters(command, test);
                try
                {
                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    return false;
                }
            }
        }

        public static bool DeleteTest(int testId)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "DELETE FROM Tests WHERE TestId = @TestId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TestId", testId);
                try
                {
                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    return false;
                }
            }
        }

        private static clsTest ReadTest(SqlDataReader reader)
        {
            return new clsTest
            {
                TestId = reader.GetInt32(0),
                ApplicationId = reader.GetInt32(1),
                TestTypeId = reader.GetInt32(2),
                PaymentStatus = reader.GetString(3),
                TestDate = reader.GetDateTime(4),
                Score = reader.GetInt32(5),
                TestResult = reader.GetString(6),
                RetryCount = reader.GetInt16(7),
                QuestionCount = reader.GetInt16(8),
                Fees = reader.GetInt16(9),
                Description = reader.GetString(10),
                PersonId = reader.GetInt32(11)
            };
        }

        private static void AddTestParameters(SqlCommand command, clsTest test)
        {
            command.Parameters.AddWithValue("@TestId", test.TestId);
            command.Parameters.AddWithValue("@ApplicationId", test.ApplicationId);
            command.Parameters.AddWithValue("@TestTypeId", test.TestTypeId);
            command.Parameters.AddWithValue("@PaymentStatus", test.PaymentStatus);
            command.Parameters.AddWithValue("@TestDate", test.TestDate);
            command.Parameters.AddWithValue("@Score", test.Score);
            command.Parameters.AddWithValue("@TestResult", test.TestResult);
            command.Parameters.AddWithValue("@RetryCount", test.RetryCount);
            command.Parameters.AddWithValue("@QuestionCount", test.QuestionCount);
            command.Parameters.AddWithValue("@Fees", test.Fees);
            command.Parameters.AddWithValue("@Description", test.Description);
            command.Parameters.AddWithValue("@PersonId", test.PersonId);
        }
    }
}
