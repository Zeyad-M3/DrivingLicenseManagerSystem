using System;
using System.Collections.Generic;
using ContactsDataAccessLayer;

namespace ContactsBusinessLayer
{
    public class clsTestTypeBusiness
    {
        public int TestTypeID { get; set; }
        public string TestTypeName { get; set; }
        public string TestDescription { get; set; }
        public decimal TestFees { get; set; }

        public static List<clsTestTypeBusiness> GetAllTestTypes()
        {
            List<clsTestType> dataList = clsTestTypeData.GetAllTestTypeData();
            List<clsTestTypeBusiness> businessList = new List<clsTestTypeBusiness>();

            foreach (var data in dataList)
            {
                businessList.Add(new clsTestTypeBusiness
                {
                    TestTypeID = data.TestTypeID,
                    TestTypeName = data.TestTypeName,
                    TestDescription = data.TestDescription,
                    TestFees = data.TestFees
                });
            }

            return businessList;
        }

        public static clsTestTypeBusiness GetTestTypeById(int testTypeId)
        {
            clsTestType data = clsTestTypeData.GetTestTypeById(testTypeId);
            if (data != null)
            {
                return new clsTestTypeBusiness
                {
                    TestTypeID = data.TestTypeID,
                    TestTypeName = data.TestTypeName,
                    TestDescription = data.TestDescription,
                    TestFees = data.TestFees
                };
            }
            return null;
        }

        // Update test type
        public static bool UpdateTestType(clsTestTypeBusiness testType)
        {
            clsTestType data = new clsTestType
            {
                TestTypeID = testType.TestTypeID,
                TestTypeName = testType.TestTypeName,
                TestDescription = testType.TestDescription,
                TestFees = testType.TestFees
            };

            return clsTestTypeData.UpdateTestType(data);
        }

        public bool AddTestType()
        {
            clsTestType data = new clsTestType
            {
                TestTypeID = this.TestTypeID,
                TestTypeName = this.TestTypeName
            };

            return clsTestTypeData.AddTestTypeData(data);
        }
    }
}
