using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ContactsDataAccessLayer
{
    public class ApplicationTypeData
    {
        public int ApplicationTypeId { get; set; }
        public string ApplicationTypeName { get; set; }
        public decimal ApplicationTypefee { get; set; }

        // Constructor
        public ApplicationTypeData(int id, string name, decimal ApplicationTypefees)
        {
            ApplicationTypeId = id;
            ApplicationTypeName = name;
            ApplicationTypefee = ApplicationTypefees;
           
        }


        public static List<ApplicationTypeData> GetAllApplicationTypesList()
        {
            List<ApplicationTypeData> applicationTypes = new List<ApplicationTypeData>();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT ApplicationTypeId, ApplicationTypeName, ApplicationTypefee FROM ApplicationType", connection);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            decimal fee = reader.GetDecimal(2);
                            ApplicationTypeData applicationType = new ApplicationTypeData(id, name, fee);
                            applicationTypes.Add(applicationType);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            return applicationTypes;
        }

        // Update one of the applicationType
        public static List<ApplicationTypeData> UpdateApplicationType(ApplicationTypeData updatedData)
        {
            List<ApplicationTypeData> applicationTypes = new List<ApplicationTypeData>();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand("UPDATE ApplicationType SET ApplicationTypeName = @Name, ApplicationTypefee = @Fee WHERE ApplicationTypeId = @Id; SELECT ApplicationTypeId, ApplicationTypeName, ApplicationTypefee FROM ApplicationType WHERE ApplicationTypeId = @Id", connection);
                try
                {
                    connection.Open();
                    // تعيين المعاملات من كائن updatedData
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = updatedData.ApplicationTypeId;
                    command.Parameters.Add("@Name", SqlDbType.NVarChar, 100).Value = (object)updatedData.ApplicationTypeName ?? DBNull.Value;
                    command.Parameters.Add("@Fee", SqlDbType.SmallMoney).Value = (object)updatedData.ApplicationTypefee ?? DBNull.Value;
                    using (SqlDataReader reader = command.ExecuteNonQuery() > 0 ? command.ExecuteReader() : null)
                    {
                        if (reader != null && reader.Read())
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("ApplicationTypeId"));
                            string name = reader.IsDBNull(reader.GetOrdinal("ApplicationTypeName")) ? string.Empty : reader.GetString(reader.GetOrdinal("ApplicationTypeName"));
                            decimal fee = reader.IsDBNull(reader.GetOrdinal("ApplicationTypefee")) ? 0m : reader.GetDecimal(reader.GetOrdinal("ApplicationTypefee"));
                            ApplicationTypeData applicationType = new ApplicationTypeData(id, name, fee);
                            applicationTypes.Add(applicationType);
                        }
                        else
                        {
                            Console.WriteLine("No rows updated or found.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            return applicationTypes;
        }

        //get application by id
        public static List<ApplicationTypeData> GetApplicationTypeByID(int id)
        {
            List<ApplicationTypeData> resultList = new List<ApplicationTypeData>();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT ApplicationTypeId, ApplicationTypeName, ApplicationTypefee FROM ApplicationType WHERE ApplicationTypeId = @Id", connection);
                try
                {
                    connection.Open();
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int applicationId = reader.GetInt32(reader.GetOrdinal("ApplicationTypeId"));
                            string name = reader.IsDBNull(reader.GetOrdinal("ApplicationTypeName")) ? string.Empty : reader.GetString(reader.GetOrdinal("ApplicationTypeName"));
                            decimal fee = reader.IsDBNull(reader.GetOrdinal("ApplicationTypefee")) ? 0m : reader.GetDecimal(reader.GetOrdinal("ApplicationTypefee"));
                            ApplicationTypeData applicationType = new ApplicationTypeData(applicationId, name, fee);
                            resultList.Add(applicationType);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            return resultList;
        }

    }
}
