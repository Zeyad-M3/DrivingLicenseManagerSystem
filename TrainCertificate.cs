using System;
using System.Collections.Generic;
using ContactsDataAccessLayer;

namespace ContactsBusinessLayer
{
    public class clsTrainCertificateBL
    {
        public int CertificateID { get; set; }
        public int PersonID { get; set; }
        public string CourseName { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ValidUntil { get; set; }
        public string Description { get; set; }

        public static List<clsTrainCertificateBL> GetAllCertificates()
        {
            List<clsTrainCertificateBL> certificates = new List<clsTrainCertificateBL>();
            var dalCertificates = clsTrainCertificateData.GetAllTrainCertificatesData();

            foreach (var dalCertificate in dalCertificates)
            {
                certificates.Add(new clsTrainCertificateBL
                {
                    CertificateID = dalCertificate.CertificateID,
                    PersonID = dalCertificate.PersonID,
                    CourseName = dalCertificate.CourseName,
                    IssueDate = dalCertificate.IssueDate,
                    ValidUntil = dalCertificate.ValidUntil,
                    Description = dalCertificate.Description
                });
            }

            return certificates;
        }

        public static List<clsTrainCertificateBL> GetCertificatesByPersonID(int personID)
        {
            List<clsTrainCertificateBL> certificates = new List<clsTrainCertificateBL>();
            var dalCertificates = clsTrainCertificateData.GetTrainCertificatesByPersonID(personID);

            foreach (var dalCertificate in dalCertificates)
            {
                certificates.Add(new clsTrainCertificateBL
                {
                    CertificateID = dalCertificate.CertificateID,
                    PersonID = dalCertificate.PersonID,
                    CourseName = dalCertificate.CourseName,
                    IssueDate = dalCertificate.IssueDate,
                    ValidUntil = dalCertificate.ValidUntil,
                    Description = dalCertificate.Description
                });
            }

            return certificates;
        }

        public bool Save()
        {
            var dalCertificate = new clsTrainCertificate
            {
                CertificateID = this.CertificateID,
                PersonID = this.PersonID,
                CourseName = this.CourseName,
                IssueDate = this.IssueDate,
                ValidUntil = this.ValidUntil,
                Description = this.Description
            };

            return clsTrainCertificateData.AddTrainCertificateData(dalCertificate);
        }
    }
}
