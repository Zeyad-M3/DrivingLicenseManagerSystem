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
        public DateTime TestDate { get; set; }
        public int Score { get; set; }
        public string TestResult { get; set; }
        public int? RetryCount { get; set; }
        public int QuestionCount { get; set; }
        public string Description { get; set; }
        public int? PersonId { get; set; } // تغيير إلى nullable
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
            return GetTestsByCondition("ApplicationID", applicationId);
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
                string query = @"INSERT INTO Tests (ApplicationId, TestTypeId, TestDate, Score, Result, RetryCount, QuestionCount, Description, PersonId) 
                         VALUES (@ApplicationId, @TestTypeId, @TestDate, @Score, @Result, @RetryCount, @QuestionCount, @Description, @PersonId)";
                SqlCommand command = new SqlCommand(query, connection);

                // تسجيل القيمة المرسلة لتتبع المشكلة

                // إضافة المعاملات
                command.Parameters.AddWithValue("@ApplicationId", test.ApplicationId);
                command.Parameters.AddWithValue("@TestTypeId", test.TestTypeId);
                command.Parameters.AddWithValue("@TestDate", test.TestDate);
                command.Parameters.AddWithValue("@Score", (object)test.Score ?? DBNull.Value);
                command.Parameters.AddWithValue("@Result", (object)test.TestResult ?? DBNull.Value);
                command.Parameters.AddWithValue("@RetryCount", (object)test.RetryCount ?? DBNull.Value);
                command.Parameters.AddWithValue("@QuestionCount", test.QuestionCount);
                command.Parameters.AddWithValue("@Description", (object)test.Description ?? DBNull.Value);
                command.Parameters.AddWithValue("@PersonId", (object)test.PersonId ?? DBNull.Value); // تعيين NULL إذا كان null

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"Insert successful, rows affected: {rowsAffected}");
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    return false;
                }
            }
        }

        // validate the test before make new Issue 
        public static bool ValidateTestBeforeIssue(clsTest test)
        {
            bool isValid = false;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"
            SELECT COUNT(*) 
            FROM Tests
            WHERE ApplicationID = @ApplicationId
            AND Result = 'Pass'
            AND TestTypeID IN (1, 2, 3)
            AND EXISTS (
                SELECT 1
                FROM Tests t2
                WHERE t2.ApplicationID = Tests.ApplicationID
                AND t2.Result = 'Pass'
                AND t2.TestTypeID IN (1, 2, 3)
                GROUP BY t2.ApplicationID
                HAVING COUNT(DISTINCT t2.TestTypeID) = 3
            )";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ApplicationId", test.ApplicationId);
                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        isValid = Convert.ToInt32(result) > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error validating test for ApplicationId {test.ApplicationId}: {ex.Message}");
                }
            }
            return isValid;
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
                TestId = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                ApplicationId = reader.IsDBNull(1) ? 0 : reader.GetInt32(1),
                TestTypeId = reader.IsDBNull(2) ? 0 : reader.GetInt32(2),
                TestDate = reader.IsDBNull(3) ? DateTime.MinValue : reader.GetDateTime(3),
                Score = reader.IsDBNull(4) ? 0 : reader.GetInt32(4),
                TestResult = reader.IsDBNull(5) ? null : reader.GetString(5),
                RetryCount = reader.IsDBNull(6) ? 0 : reader.GetInt32(6), // استخدام GetInt32 بدلاً من GetInt16
                QuestionCount = reader.IsDBNull(7) ? 0 : reader.GetInt32(7), // استخدام GetInt32 بدلاً من GetInt16
                Description = reader.IsDBNull(8) ? null : reader.GetString(8),
                PersonId = reader.IsDBNull(9) ? 0 : reader.GetInt32(9)
            };
        }

        private static void AddTestParameters(SqlCommand command, clsTest test)
        {
            command.Parameters.AddWithValue("@TestId", test.TestId);
            command.Parameters.AddWithValue("@ApplicationID", test.ApplicationId);
            command.Parameters.AddWithValue("@TestTypeId", test.TestTypeId);
            command.Parameters.AddWithValue("@TestDate", test.TestDate);
            command.Parameters.AddWithValue("@Score", test.Score);
            command.Parameters.AddWithValue("@TestResult", test.TestResult);
            command.Parameters.AddWithValue("@RetryCount", test.RetryCount);
            command.Parameters.AddWithValue("@QuestionCount", test.QuestionCount);
            command.Parameters.AddWithValue("@Description", test.Description);
            command.Parameters.AddWithValue("@PersonId", test.PersonId);
        }
    }
}
