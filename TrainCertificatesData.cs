using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ContactsDataAccessLayer
{
    public class clsTrainCertificate
    {
        public int CertificateID { get; set; }
        public int PersonID { get; set; }
        public string CourseName { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ValidUntil { get; set; }
        public string Description { get; set; }
    }

    public class clsTrainCertificateData
    {
        public static List<clsTrainCertificate> GetAllTrainCertificatesData()
        {
            List<clsTrainCertificate> certificates = new List<clsTrainCertificate>();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM TrainCertificates", connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        clsTrainCertificate certificate = new clsTrainCertificate
                        {
                            CertificateID = reader.GetInt32(0),
                            PersonID = reader.GetInt32(1),
                            CourseName = reader.GetString(2),
                            IssueDate = reader.GetDateTime(3),
                            ValidUntil = reader.GetDateTime(4),
                            Description = reader.GetString(5)
                        };
                        certificates.Add(certificate);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            return certificates;
        }

        public static List<clsTrainCertificate> GetTrainCertificatesByPersonID(int personID)
        {
            List<clsTrainCertificate> certificates = new List<clsTrainCertificate>();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM TrainCertificates WHERE PersonID = @PersonID", connection);
                command.Parameters.AddWithValue("@PersonID", personID);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        clsTrainCertificate certificate = new clsTrainCertificate
                        {
                            CertificateID = reader.GetInt32(0),
                            PersonID = reader.GetInt32(1),
                            CourseName = reader.GetString(2),
                            IssueDate = reader.GetDateTime(3),
                            ValidUntil = reader.GetDateTime(4),
                            Description = reader.GetString(5)
                        };
                        certificates.Add(certificate);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            return certificates;
        }

        public static bool AddTrainCertificateData(clsTrainCertificate certificate)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(
                    @"INSERT INTO TrainCertificates 
                    (CertificateID, PersonID, CourseName, IssueDate, ValidUntil, Description) 
                    VALUES (@CertificateID, @PersonID, @CourseName, @IssueDate, @ValidUntil, @Description)",
                    connection);

                command.Parameters.AddWithValue("@CertificateID", certificate.CertificateID);
                command.Parameters.AddWithValue("@PersonID", certificate.PersonID);
                command.Parameters.AddWithValue("@CourseName", certificate.CourseName);
                command.Parameters.AddWithValue("@IssueDate", certificate.IssueDate);
                command.Parameters.AddWithValue("@ValidUntil", certificate.ValidUntil);
                command.Parameters.AddWithValue("@Description", certificate.Description);

                try
                {
                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    return false;
                }
            }
        }
    }
}
