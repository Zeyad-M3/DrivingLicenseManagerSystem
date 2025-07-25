using System;
using System.Collections.Generic;
using ContactsDataAccessLayer;

namespace ContactsBusinessLayer
{
    public class clsTestTypeBusiness
    {
        public int TestTypeID { get; set; }
        public string TestTypeName { get; set; }

        public static List<clsTestTypeBusiness> GetAllTestTypes()
        {
            List<clsTestType> dataList = clsTestTypeData.GetAllTestTypeData();
            List<clsTestTypeBusiness> businessList = new List<clsTestTypeBusiness>();

            foreach (var data in dataList)
            {
                businessList.Add(new clsTestTypeBusiness
                {
                    TestTypeID = data.TestTypeID,
                    TestTypeName = data.TestTypeName
                });
            }

            return businessList;
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
