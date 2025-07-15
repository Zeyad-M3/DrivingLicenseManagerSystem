using System;
using System.Data;
using ContactsDataAccessLayer;

namespace ContactsBusinessLayer
{
    public class clsApplication
    {
        // Properties
        public int Id { get; set; }
        public int PersonID { get; set; }
        public string ApplicationDate { get; set; }

        public string ApplicationType { get; set; }
        public string Status { get; set; }
        public string ApplicationFee { get; set; }
        public string PaymentDate { get; set; }
        public string LicenseClassID { get; set; }
        // Constructor
        public clsApplication()
        {
            Id = 0;
            PersonID = 0;
            ApplicationDate = string.Empty;
            ApplicationType = string.Empty;
            Status = string.Empty;
            ApplicationFee = string.Empty;
            PaymentDate = string.Empty;
            LicenseClassID = string.Empty;
        }
        // Constructor with parameters
        public clsApplication(int id, int personID, string applicationDate, string applicationType, string status,
            string applicationFee, string paymentDate, string licenseClassID)
        {
            this.Id = id;
            this.PersonID = personID;
            this.ApplicationDate = applicationDate;
            this.ApplicationType = applicationType;
            this.Status = status;
            this.ApplicationFee = applicationFee;
            this.PaymentDate = paymentDate;
            this.LicenseClassID = licenseClassID;
        }


        // Method to get application by ID
        public bool GetApplicationByID(int id)
        {
            int personID = 0;
            string applicationDate = string.Empty;
            string applicationType = string.Empty;
            string status = string.Empty;
            string applicationFee = string.Empty;
            string paymentDate = string.Empty;
            string licenseClassID = string.Empty;

            bool isFound = clsApplicationDataAccess.GetApplicationByID(id, ref personID, ref applicationDate, ref applicationType,
                ref status, ref applicationFee, ref paymentDate, ref licenseClassID);

            if (isFound)
            {
                this.Id = id;
                this.PersonID = personID;
                this.ApplicationDate = applicationDate;
                this.ApplicationType = applicationType;
                this.Status = status;
                this.ApplicationFee = applicationFee;
                this.PaymentDate = paymentDate;
                this.LicenseClassID = licenseClassID;
            }

            return isFound;
        }


        public static DataTable GetAllApplications()
        {
            return clsApplicationDataAccess.GetAllApplications();

        }
        //test connection
        public bool testconnection()
        {
            if (clsApplicationDataAccess.TestConnection())
                return false;
            else
                return true;
        }

        //count applications
        public static int CountApplications()
        {
            return clsApplicationDataAccess.CountApplications();
        }

        // Method to add a new application
        public void AddApplication(int personID, string applicationDate, string applicationType, string status,
            string applicationFee, string paymentDate, string licenseClassID)
        {
            clsApplicationDataAccess.AddAnewApplication(personID, applicationDate, applicationType, status,
               applicationFee, paymentDate, licenseClassID);
        }

        // Method to update an existing application
        public void UpdateApplication(int id, int personID, string applicationDate, string applicationType, string status,
            string applicationFee, string paymentDate, string licenseClassID)
        {
            clsApplicationDataAccess.UpdateApplication(id, personID, applicationDate, applicationType, status,
                applicationFee, paymentDate, licenseClassID);
        }

        // Method to delete an application
        public void DeleteApplication(int id)
        {
            clsApplicationDataAccess.DeleteApplication(id);
        }
        // Method to search applications by person ID
        public static bool SearchApplicationsByPersonID(int personID, string applicationDate, string applicationType, string status,
            string applicationFee, string paymentDate, string licenseClassID)
        {

            return clsApplicationDataAccess.GetApplicationByPersonID(personID);
        }

        // Method to search applications by application type
        public static bool SearchApplicationsByApplicationType(string applicationType)
        {
            return clsApplicationDataAccess.GetApplicationTypebyName(applicationType);
        }




    }
}
