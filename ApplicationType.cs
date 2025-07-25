using ContactsDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsBusinessLayer
{
    public class ApplicationType
    {

        public int ApplicationTypeId { get; set; }
        public string ApplicationTypeName { get; set; }
        public decimal ApplicationTypefee { get; set; }
        // Constructor
        public ApplicationType(int id, string name, decimal ApplicationTypefees)
        {
            ApplicationTypeId = id;
            ApplicationTypeName = name;
            ApplicationTypefee = ApplicationTypefees;
        }

       

        public static List<ApplicationType> GetAllApplicationTypes()
        {
            var dataList = ApplicationTypeData.GetAllApplicationTypesList();
            List<ApplicationType> resultList = new List<ApplicationType>();
            foreach (var dataObj in dataList)
            {
                ApplicationType businessObj = new ApplicationType(
                    dataObj.ApplicationTypeId,
                    dataObj.ApplicationTypeName,
                    dataObj.ApplicationTypefee
                );
                resultList.Add(businessObj);

            }
            return resultList;
        }
        // Update one of the applicationType
        public static List<ApplicationType> UpdateApplicationType(ApplicationType updatedData)
        {
            var dataList = ApplicationTypeData.UpdateApplicationType(new ApplicationTypeData(
                updatedData.ApplicationTypeId,
                updatedData.ApplicationTypeName,
                updatedData.ApplicationTypefee
            ));
            List<ApplicationType> resultList = new List<ApplicationType>();
            foreach (var dataObj in dataList)
            {
                ApplicationType businessObj = new ApplicationType(
                    dataObj.ApplicationTypeId,
                    dataObj.ApplicationTypeName,
                    dataObj.ApplicationTypefee
                );
                resultList.Add(businessObj);
            }
            return resultList;
        }

        // Update one of the applicationType by ID
        public static ApplicationType GetApplicationTypeById(int id)
        {
            var dataObj = ApplicationTypeData.GetApplicationTypeByID(id);
          
            foreach (var data in dataObj)
            {
                return new ApplicationType(
                    data.ApplicationTypeId,
                    data.ApplicationTypeName,
                    data.ApplicationTypefee
                );
            }

            return null;
        }








    }
}
