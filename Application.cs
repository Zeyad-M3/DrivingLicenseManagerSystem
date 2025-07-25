
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using ContactsDataAccessLayer;

namespace ContactsBusinessLayer
{
    public class clsApplication
    {
        public int Id { get; set; }
        public int PersonID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public int ApplicationType { get; set; }
        public string Status { get; set; }
        public decimal ApplicationFee { get; set; }
        public DateTime PaymentDate { get; set; }
        public int LicenseClassID { get; set; }
        public int? Score { get; set; } // إضافة Score كقابل للـ null

        // Business Methods:

        public static List<clsApplication> GetAllApplications()
        {
            List<clsApplication> applications = new List<clsApplication>();
            var dataList = clsApplicationData.GetAllApplicationData();
            foreach (var item in dataList)
            {
                applications.Add(MapToBusiness(item));
            }
            return applications;
        }

        public static clsApplication GetApplicationById(int id)
        {
            var dataApplication = clsApplicationData.GetApplicationById(id);
            return dataApplication != null ? MapToBusiness(dataApplication) : null;
        }

        public static List<clsApplication> GetApplicationsByPersonID(int personId)
        {
            List<clsApplication> applications = new List<clsApplication>();
            var dataList = clsApplicationData.GetApplicationByPersonID(personId);
            foreach (var item in dataList)
            {
                applications.Add(MapToBusiness(item));
            }
            return applications;
        }
        // gitPersonIDbyApplication


        public bool Save()
        {
            try
            {
                // Map this business-layer object to a data-layer object before passing to data layer
                ContactsDataAccessLayer.clsApplication data = MapToDataLayer(this);
                if (clsApplicationData.GetApplicationById(this.Id) == null)
                {
                    int newId = clsApplicationData.AddApplication(data); // احصل على Id الجديد
                    if (newId > 0)
                    {
                        this.Id = newId; // تحديث Id في الكائن
                        return true;
                    }
                    return false;
                }
                else
                {
                    return clsApplicationData.UpdateApplication(data); // تحديث موجود
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving application: " + ex.Message);
                return false;
            }
        }

        public static bool UpdateApplication(clsApplication application)
        {
            try
            {
                return application.Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating application: " + ex.Message);
                return false;
            }
        }

        public static bool Delete(int id)
        {
            try
            {
                return clsApplicationData.DeleteApplication(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting application: " + ex.Message);
                return false;
            }
        }

        public static bool AddApplication(clsApplication application)
        {
            try
            {
                return application.Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding application: " + ex.Message);
                return false;
            }
        }

        // Fix MapToBusiness and MapToDataLayer to convert between DateTime and string

        private static clsApplication MapToBusiness(ContactsDataAccessLayer.clsApplication data)
        {
            if (data == null) return null;
            return new clsApplication
            {
                Id = data.Id,
                PersonID = data.PersonID,
                ApplicationDate = data.ApplicationDate,
                ApplicationType = data.ApplicationType,
                Status = data.Status ?? string.Empty,
                ApplicationFee = data.ApplicationFee,
                PaymentDate = data.PaymentDate,
                LicenseClassID = data.LicenseClassID,
                Score = data.Score,
            };
        }

        // Fix MapToDataLayer to assign DateTime properties directly, not as strings
        private static ContactsDataAccessLayer.clsApplication MapToDataLayer(clsApplication business)
        {
            if (business == null) return null;
            return new ContactsDataAccessLayer.clsApplication
            {
                Id = business.Id,
                PersonID = business.PersonID,
                ApplicationDate = business.ApplicationDate,
                ApplicationType = business.ApplicationType,
                Status = business.Status ?? string.Empty,
                ApplicationFee = business.ApplicationFee,
                PaymentDate = business.PaymentDate,
                LicenseClassID = business.LicenseClassID
            };
        }
    }
}
