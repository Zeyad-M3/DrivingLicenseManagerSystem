using System;
using System.Collections.Generic;
using ContactsDataAccessLayer;

namespace ContactsBusinessLayer
{
    public class clsLicense
    {
        public int LicenseId { get; set; }
        public int PersonId { get; set; }
        public string LicenseNumber { get; set; }
        public string ApplicationType { get; set; }
        public string PhotoPath { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Conditions { get; set; }
        public string IssueStatus { get; set; }

        // ✅ Get All Licenses
        public static List<clsLicense> GetAllLicenses()
        {
            List<clsLicense> licenses = new List<clsLicense>();
            var dataList = clsLicenseData.GetAllLicenseData();

            foreach (var data in dataList)
            {
                licenses.Add(MapDataToBusiness(data));
            }

            return licenses;
        }

        // ✅ Get License by ID
        public static clsLicense GetLicenseById(int licenseId)
        {
            var data = clsLicenseData.GetLicenseById(licenseId);
            return data == null ? null : MapDataToBusiness(data);
        }

        // ✅ Get Licenses by Person ID
        public static List<clsLicense> GetLicensesByPersonId(int personId)
        {
            List<clsLicense> licenses = new List<clsLicense>();
            var dataList = clsLicenseData.GetLicensesByPersonId(personId);

            foreach (var data in dataList)
            {
                licenses.Add(MapDataToBusiness(data));
            }

            return licenses;
        }
        // Issue Replacement License
        public static bool IssueReplacementLicense(int licenseId)
        {
            // Get the existing license
            var existingLicense = clsLicenseData.GetLicenseById(licenseId);
            if (existingLicense == null) return false;
            // Create a new license object for the replacement
            clsLicenseData.clsLicense newLicense = new clsLicenseData.clsLicense
            {
                /*LicenseId = 0,*/ // New license, so ID should be 0 or auto-generated
                //auto-generated license ID

                PersonId = existingLicense.PersonId,
                LicenseNumber = existingLicense.LicenseNumber + "-R" + DateTime.Now.Ticks.ToString().Substring(0, 4), // Example unique suffix
                ApplicationType = "Replacement",
                PhotoPath = "",
                IssueDate = DateTime.Now,
                ExpiryDate = existingLicense.ExpiryDate, // Keep the same expiry date
                Conditions = existingLicense.Conditions,
                IssueStatus = "Active"
            };
            // Add the new license to the database
            return clsLicenseData.AddLicense(newLicense) > 0;
        }
        // add New License for renew 
        public static bool AddNewLicense(clsLicense license)
        {
            if (license == null) return false;
            // Map this business-layer object to a data-layer object before passing to data layer
            clsLicenseData.clsLicense data = new clsLicenseData.clsLicense
            {
                LicenseId = license.LicenseId,
                PersonId = license.PersonId,
                LicenseNumber = license.LicenseNumber,
                ApplicationType = license.ApplicationType,
                PhotoPath = license.PhotoPath,
                IssueDate = license.IssueDate,
                ExpiryDate = license.ExpiryDate,
                Conditions = license.Conditions,
                IssueStatus = license.IssueStatus
            };
            return clsLicenseData.AddLicense(data) > 0;
        }
        //UpdateLicense
        public static bool UpdateLicense(clsLicense license)
        {
            if (license == null) return false;
            // Map this business-layer object to a data-layer object before passing to data layer
            clsLicenseData.clsLicense data = new clsLicenseData.clsLicense
            {
                LicenseId = license.LicenseId,
                PersonId = license.PersonId,
                LicenseNumber = license.LicenseNumber,
                ApplicationType = license.ApplicationType,
                PhotoPath = license.PhotoPath,
                IssueDate = license.IssueDate,
                ExpiryDate = license.ExpiryDate,
                Conditions = license.Conditions,
                IssueStatus = license.IssueStatus
            };
            // Replace this line in UpdateLicense method:
            // return clsLicenseData.UpdateLicense(data) > 0;

            // With this line:
            return clsLicenseData.UpdateLicense(data);
        
        }

        // ✅ Get Licenses by License Number
        public static List<clsLicense> GetLicensesByLicenseNumber(int licenseNumber)
        {
            List<clsLicense> licenses = new List<clsLicense>();
            var dataList = clsLicenseData.GetLicensesByLicenseNumber(licenseNumber);

            foreach (var data in dataList)
            {
                licenses.Add(MapDataToBusiness(data));
            }

            return licenses;
        }
        // add New License
        public bool Save(clsLicense license)
        {
            try
            {
                // Map this business-layer object to a data-layer object before passing to data layer
                clsLicenseData.clsLicense data = new clsLicenseData.clsLicense
                {
                    LicenseId = this.LicenseId,
                    PersonId = this.PersonId,
                    LicenseNumber = this.LicenseNumber,
                    ApplicationType = this.ApplicationType,
                    PhotoPath = this.PhotoPath,
                    IssueDate = this.IssueDate,
                    ExpiryDate = this.ExpiryDate,
                    Conditions = this.Conditions,
                    IssueStatus = this.IssueStatus
                };

                return clsLicenseData.AddLicense(data) > 0;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving license: " + ex.Message);
                return false;
            }
        }

        // Fix for CS1579: foreach statement cannot operate on variables of type 'clsLicenseData.clsLicense' because 'clsLicenseData.clsLicense' does not contain a public instance or extension definition for 'GetEnumerator'

        // The error occurs because you are trying to use foreach on a single object instead of a collection.
        // Specifically, in the method GetLicenseByPersonId (the non-plural version), you are calling clsLicenseData.GetLicenseByPersonId(personId), which returns a single clsLicenseData.clsLicense object, not a collection.
        // You should check for null and add the single object to the list if it exists.

        public static List<clsLicense> GetLicenseByPersonId(int personId)
        {
            List<clsLicense> licenses = new List<clsLicense>();
            var data = clsLicenseData.GetLicenseByPersonId(personId);
            if (data != null)
            {
                licenses.Add(MapDataToBusiness(data));
            }
            return licenses;
        }

        // ✅ Mapping Method

        private static clsLicense MapDataToBusiness(clsLicenseData.clsLicense data)
        {
            return new clsLicense
            {
                LicenseId = data.LicenseId,
                PersonId = data.PersonId,
                LicenseNumber = data.LicenseNumber,
                ApplicationType = data.ApplicationType,
                PhotoPath = data.PhotoPath,
                IssueDate = data.IssueDate,
                ExpiryDate = data.ExpiryDate,
                Conditions = data.Conditions,
                IssueStatus = data.IssueStatus
            };
        }
    }
}
