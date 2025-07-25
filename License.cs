using System;
using System.Collections.Generic;
using ContactsDataAccessLayer;

namespace ContactsBusinessLayer
{
    public class clsLicense
    {
        public int LicenseId { get; set; }
        public int PersonId { get; set; }
        public int LicenseNumber { get; set; }
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
