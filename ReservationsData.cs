using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ContactsDataAccessLayer
{
    public class clsReservation
    {
        public int ReservationId { get; set; }
        public int LicenseId { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime ResolveDate { get; set; }
        public string Description { get; set; }
        public string Reason { get; set; }
        public string Penalty { get; set; }
        public string CreatedBy { get; set; }
    }

    public class clsReservationsData
    {
        public static List<clsReservation> GetAllReservationsData()
        {
            List<clsReservation> reservations = new List<clsReservation>();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand("SELECT * FROM Reservations", connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    clsReservation reservation = new clsReservation
                    {
                        ReservationId = reader.GetInt32(0),
                        LicenseId = reader.GetInt32(1),
                        ReservationDate = reader.GetDateTime(2),
                        ResolveDate = reader.GetDateTime(3),
                        Description = reader.GetString(4),
                        Reason = reader.GetString(5),
                        Penalty = reader.GetString(6),
                        CreatedBy = reader.GetString(7)
                    };
                    reservations.Add(reservation);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return reservations;
        }

        public static clsReservation GetReservationById(int reservationId)
        {
            clsReservation reservation = null;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Reservations WHERE ReservationId = @ReservationId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ReservationId", reservationId);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    reservation = new clsReservation
                    {
                        ReservationId = reader.GetInt32(0),
                        LicenseId = reader.GetInt32(1),
                        ReservationDate = reader.GetDateTime(2),
                        ResolveDate = reader.GetDateTime(3),
                        Description = reader.GetString(4),
                        Reason = reader.GetString(5),
                        Penalty = reader.GetString(6),
                        CreatedBy = reader.GetString(7)
                    };
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return reservation;
        }

        public static List<clsReservation> GetReservationsByCreatedBy(string createdBy)
        {
            List<clsReservation> reservations = new List<clsReservation>();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Reservations WHERE CreatedBy = @CreatedBy";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CreatedBy", createdBy);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    clsReservation reservation = new clsReservation
                    {
                        ReservationId = reader.GetInt32(0),
                        LicenseId = reader.GetInt32(1),
                        ReservationDate = reader.GetDateTime(2),
                        ResolveDate = reader.GetDateTime(3),
                        Description = reader.GetString(4),
                        Reason = reader.GetString(5),
                        Penalty = reader.GetString(6),
                        CreatedBy = reader.GetString(7)
                    };
                    reservations.Add(reservation);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return reservations;
        }

        public static void AddReservation(clsReservation reservation)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "INSERT INTO Reservations (ReservationId, LicenseId, ReservationDate, ResolveDate, Description, Reason, Penalty, CreatedBy) " +
                           "VALUES (@ReservationId, @LicenseId, @ReservationDate, @ResolveDate, @Description, @Reason, @Penalty, @CreatedBy)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ReservationId", reservation.ReservationId);
            command.Parameters.AddWithValue("@LicenseId", reservation.LicenseId);
            command.Parameters.AddWithValue("@ReservationDate", reservation.ReservationDate);
            command.Parameters.AddWithValue("@ResolveDate", reservation.ResolveDate);
            command.Parameters.AddWithValue("@Description", reservation.Description);
            command.Parameters.AddWithValue("@Reason", reservation.Reason);
            command.Parameters.AddWithValue("@Penalty", reservation.Penalty);
            command.Parameters.AddWithValue("@CreatedBy", reservation.CreatedBy);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public static void UpdateReservation(clsReservation reservation)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "UPDATE Reservations SET LicenseId = @LicenseId, ReservationDate = @ReservationDate, ResolveDate = @ResolveDate, " +
                           "Description = @Description, Reason = @Reason, Penalty = @Penalty, CreatedBy = @CreatedBy WHERE ReservationId = @ReservationId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ReservationId", reservation.ReservationId);
            command.Parameters.AddWithValue("@LicenseId", reservation.LicenseId);
            command.Parameters.AddWithValue("@ReservationDate", reservation.ReservationDate);
            command.Parameters.AddWithValue("@ResolveDate", reservation.ResolveDate);
            command.Parameters.AddWithValue("@Description", reservation.Description);
            command.Parameters.AddWithValue("@Reason", reservation.Reason);
            command.Parameters.AddWithValue("@Penalty", reservation.Penalty);
            command.Parameters.AddWithValue("@CreatedBy", reservation.CreatedBy);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public static void DeleteReservation(int reservationId)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "DELETE FROM Reservations WHERE ReservationId = @ReservationId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ReservationId", reservationId);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
