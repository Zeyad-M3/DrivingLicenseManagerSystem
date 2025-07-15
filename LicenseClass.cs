using System;
using System.Collections.Generic;
using ContactsDataAccessLayer;

namespace ContactsBusinessLayer
{
    public class clsLicenseClass
    {
        public int LicenseClassId { get; set; }
        public string LicenseClassName { get; set; }
        public string ClassDescription { get; set; }
        public short MinimumAge { get; set; }
        public short ValidityLength { get; set; }
        public short ClassFee { get; set; }

        // ✅ Get All
        public static List<clsLicenseClass> GetAllLicenseClasses()
        {
            List<clsLicenseClass> licenseClasses = new List<clsLicenseClass>();
            var dataList = clsLicenseClassData.GetAllLicenseClassData();

            foreach (var dataObj in dataList)
            {
                licenseClasses.Add(MapDataToBusiness(dataObj));
            }

            return licenseClasses;
        }

        // ✅ Get by ID
        public static clsLicenseClass GetLicenseClassById(int licenseClassId)
        {
            var dataObj = clsLicenseClassData.GetLicenseClassDataById(licenseClassId);
            if (dataObj == null) return null;

            return MapDataToBusiness(dataObj);
        }

        // ✅ Get by Name
        public static clsLicenseClass GetLicenseClassByName(string licenseClassName)
        {
            var dataObj = clsLicenseClassData.GetLicenseClassDataByName(licenseClassName);
            if (dataObj == null) return null;

            return MapDataToBusiness(dataObj);
        }

        // ✅ Insert or Update (Save)
        public bool Save()
        {
            var dataObj = MapBusinessToData();

            if (this.LicenseClassId == 0) // Insert
            {
                return clsLicenseClassData.InsertLicenseClass(dataObj);
            }
            else // Update
            {
                return clsLicenseClassData.UpdateLicenseClass(dataObj);
            }
        }

        // ✅ Delete
        public static bool Delete(int licenseClassId)
        {
            return clsLicenseClassData.DeleteLicenseClass(licenseClassId);
        }

        // ✅ Check Exists
        public static bool IsLicenseClassExists(int licenseClassId)
        {
            return clsLicenseClassData.IsLicenseClassAvailableById(licenseClassId);
        }

        // ✅ Mapping Methods
        private static clsLicenseClass MapDataToBusiness(clsLicenseClassData.clsLicenseClass dataObj)
        {
            return new clsLicenseClass
            {
                LicenseClassId = dataObj.LicenseClassId,
                LicenseClassName = dataObj.LicenseClassName,
                ClassDescription = dataObj.ClassDescription,
                MinimumAge = dataObj.MinimumAge,
                ValidityLength = dataObj.ValidityLength,
                ClassFee = dataObj.ClassFee
            };
        }

        private clsLicenseClassData.clsLicenseClass MapBusinessToData()
        {
            return new clsLicenseClassData.clsLicenseClass
            {
                LicenseClassId = this.LicenseClassId,
                LicenseClassName = this.LicenseClassName,
                ClassDescription = this.ClassDescription,
                MinimumAge = this.MinimumAge,
                ValidityLength = this.ValidityLength,
                ClassFee = this.ClassFee
            };
        }
    }
}
