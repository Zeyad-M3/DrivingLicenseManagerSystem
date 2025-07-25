using System;
using System.Collections.Generic;
using ContactsDataAccessLayer;

namespace ContactsBusinessLayer
{
    public class clsInternationalLicense
    {
        public int Id { get; set; }
        public int LicenseID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Status { get; set; }
        public int PreviousInternationalLicenseID { get; set; }

        // Mapping Methods
        private static clsInternationalLicense FromDataObject(ContactsDataAccessLayer.clsInternationalLicensesData.clsInternationalLicense dataObj)
        {
            return new clsInternationalLicense
            {
                Id = dataObj.Id,
                LicenseID = dataObj.LicenseID,
                IssueDate = dataObj.IssueDate,
                ExpiryDate = dataObj.ExpiryDate,
                Status = dataObj.Status,
                PreviousInternationalLicenseID = dataObj.PreviousInternationalLicenseID
            };
        }

        private ContactsDataAccessLayer.clsInternationalLicensesData.clsInternationalLicense ToDataObject()
        {
            return new ContactsDataAccessLayer.clsInternationalLicensesData.clsInternationalLicense
            {
                Id = this.Id,
                LicenseID = this.LicenseID,
                IssueDate = this.IssueDate,
                ExpiryDate = this.ExpiryDate,
                Status = this.Status,
                PreviousInternationalLicenseID = this.PreviousInternationalLicenseID
            };
        }

        // Static Methods for Data Access
        public static bool Exists(int id)
        {
            return clsInternationalLicensesData.IsInternationalLicenseAvailableByID(id);
        }

        public static List<clsInternationalLicense> GetAll()
        {
            var dataList = clsInternationalLicensesData.GetAllInternationalLicenses();
            var resultList = new List<clsInternationalLicense>();

            foreach (var dataObj in dataList)
            {
                resultList.Add(FromDataObject(dataObj));
            }
            return resultList;
        }
        // add New International License
        public static bool AddNewInternationalLicense(clsInternationalLicense license)
        {
            if (license == null) return false;
            var dataObj = license.ToDataObject();
            return clsInternationalLicensesData.AddInternationalLicense(dataObj);
        
        
        }

        // check if International License exists by LicenseID

        public static bool ExistsByLicenseID(int licenseID)
        {
            return clsInternationalLicensesData.IsInternationalLicenseAvailableByLicenseID(licenseID);
        }

        public static clsInternationalLicense FindByID(int id)
        {
            var dataObj = clsInternationalLicensesData.GetInternationalLicenseByID(id);
            return dataObj != null ? FromDataObject(dataObj) : null;
        }

        public static List<clsInternationalLicense> FindByLicenseID(int licenseID)
        {
            var dataList = clsInternationalLicensesData.GetInternationalLicensesByLicenseID(licenseID);
            var resultList = new List<clsInternationalLicense>();

            foreach (var dataObj in dataList)
            {
                resultList.Add(FromDataObject(dataObj));
            }
            return resultList;
        }

        public static bool Delete(int id)
        {
            return clsInternationalLicensesData.DeleteInternationalLicense(id);
        }

        // Instance Method for Save (Insert/Update)
        public bool Save()
        {
            if (this.Id == 0)
            {
                // Insert
                return clsInternationalLicensesData.AddInternationalLicense(this.ToDataObject());
            }
            else
            {
                // Update
                return clsInternationalLicensesData.UpdateInternationalLicense(this.ToDataObject());
            }
        }
    }
}
