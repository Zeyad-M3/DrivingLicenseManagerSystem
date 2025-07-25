using ContactsDataAccessLayer;
using System;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace ContactsBusinessLayer
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
        public int? PersonId { get; set; }

        public clsTest() { }

        public clsTest(ContactsDataAccessLayer.clsTest dataTest)
        {
            this.TestId = dataTest.TestId;
            this.ApplicationId = dataTest.ApplicationId;
            this.TestTypeId = dataTest.TestTypeId;
            this.TestDate = dataTest.TestDate;
            this.Score = dataTest.Score;
            this.TestResult = dataTest.TestResult;
            this.RetryCount = dataTest.RetryCount;
            this.QuestionCount = dataTest.QuestionCount;
            this.Description = dataTest.Description;
            this.PersonId = dataTest.PersonId;
        }

        private ContactsDataAccessLayer.clsTest ToDataLayerObject()
        {
            return new ContactsDataAccessLayer.clsTest
            {
                TestId = this.TestId,
                ApplicationId = this.ApplicationId,
                TestTypeId = this.TestTypeId,
                TestDate = this.TestDate,
                Score = this.Score,
                TestResult = this.TestResult,
                RetryCount = this.RetryCount,
                QuestionCount = this.QuestionCount,
                Description = this.Description,
                PersonId = this.PersonId
            };
        }

        public static List<clsTest> GetAllTests()
        {
            List<ContactsDataAccessLayer.clsTest> dataTests = clsTestsData.GetAllTestsData();
            List<clsTest> tests = new List<clsTest>();

            foreach (var dataTest in dataTests)
                tests.Add(new clsTest(dataTest));

            return tests;
        }

        public static clsTest GetTestById(int testId)
        {
            var dataTest = clsTestsData.GetTestById(testId);
            return dataTest == null ? null : new clsTest(dataTest);
        }

        public static List<clsTest> GetTestsByPersonId(int personId)
        {
            List<ContactsDataAccessLayer.clsTest> dataTests = clsTestsData.GetTestsByPersonId(personId);
            List<clsTest> tests = new List<clsTest>();

            foreach (var dataTest in dataTests)
                tests.Add(new clsTest(dataTest));

            return tests;
        }

        public static List<clsTest> GetTestsByApplicationId(int applicationId)
        {
            List<ContactsDataAccessLayer.clsTest> dataTests = clsTestsData.GetTestsByApplicationId(applicationId);
            List<clsTest> tests = new List<clsTest>();

            foreach (var dataTest in dataTests)
                tests.Add(new clsTest(dataTest));

            return tests;
        }

        public static List<clsTest> GetTestsByTestTypeId(int testTypeId)
        {
            List<ContactsDataAccessLayer.clsTest> dataTests = clsTestsData.GetTestsByTestTypeId(testTypeId);
            List<clsTest> tests = new List<clsTest>();

            foreach (var dataTest in dataTests)
                tests.Add(new clsTest(dataTest));

            return tests;
        }

        public bool Save()
        {
            if (clsTestsData.GetTestById(this.TestId) == null)
            {
                // إضافة جديدة
                return clsTestsData.AddTest(this.ToDataLayerObject());
            }
            else
            {
                // تعديل موجود
                return clsTestsData.UpdateTest(this.ToDataLayerObject());
            }
        }
        public static bool ValidateTestBeforeIssue(clsTest test)
        {
            if (test == null || test.ApplicationId <= 0)
            {
                Console.WriteLine("Error: Invalid test object or ApplicationId.");
                return false;
            }

            // Convert ContactsBusinessLayer.clsTest to ContactsDataAccessLayer.clsTest before passing
            var dataTest = test.ToDataLayerObject();
            bool isValid = clsTestsData.ValidateTestBeforeIssue(dataTest);
            if (isValid)
            {
                Console.WriteLine($"Validation successful for ApplicationId {test.ApplicationId}: All 3 tests passed.");
            }
            else
            {
                Console.WriteLine($"Validation failed for ApplicationId {test.ApplicationId}: Not all 3 tests passed.");
            }
            return isValid;
        }

        public static bool AddTest(clsTest clsTest)
        {
            try
            {
                return clsTest.Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding application: " + ex.Message);
                return false;
            }


        }

        public static bool DeleteTest(int testId)
        {
            return clsTestsData.DeleteTest(testId);
        }
    }
}
