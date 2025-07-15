using System;
using System.Collections.Generic;
using System.Data;
using ContactsDataAccessLayer; // Assuming this namespace contains the clsAppointmentData class


namespace ContactsBusinessLayer
{
    public class clsAppointment
    {
        public int AppointmentId { get; set; }
        public string TestId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string CreatedByUserId { get; set; }

        //private clsAppointmentData _dataAccess = new ContactsDataAccessLayer.clsAppointmentData();

        // Get Appointment By Id
        public static bool LoadAppointmentById(int appointmentId)
        {
            return clsAppointmentData.GetAppointmentById(appointmentId);

            
        }

        // Add Appointment
        public bool Save()
        {
            if (AppointmentId <= 0 || string.IsNullOrEmpty(TestId) || string.IsNullOrEmpty(Status))
            {
                throw new ArgumentException("Invalid appointment data.");
            }

            return clsAppointmentData.AddAppointment(AppointmentId, TestId, AppointmentDate, Status, Description, CreatedByUserId);
        }

        // Update Appointment
        public bool Update()
        {
            if (AppointmentId <= 0)
            {
                throw new ArgumentException("Invalid appointment ID.");
            }

            return clsAppointmentData.UpdateAppointment(AppointmentId, TestId, AppointmentDate, Status, Description, CreatedByUserId);
        }

        // Delete Appointment
        public bool Delete()
        {
            if (AppointmentId <= 0)
            {
                throw new ArgumentException("Invalid appointment ID.");
            }

            return clsAppointmentData.DeleteAppointment(AppointmentId);
        }

        // Get All Appointments (Just call DAL)
        public void DisplayAllAppointments()
        {
            clsAppointmentData.GetAllAppointments();
        }

        // Get Statuses
        public List<string> GetStatuses()
        {
            return clsAppointmentData.GetAppointmentStatuses();
        }

        // Get Appointments by CreatedByUserId
        public void GetAppointmentsByUser(int createdByUserId)
        {
            if (createdByUserId <= 0)
            {
                throw new ArgumentException("Invalid user ID.");
            }

            clsAppointmentData.GetAppointmentsByCreatedByUserId(createdByUserId);
        }

        // Get Appointments by TestId
        public void GetAppointmentsByTest(string testId)
        {
            if (string.IsNullOrEmpty(testId))
            {
                throw new ArgumentException("Test ID cannot be empty.");
            }

            clsAppointmentData.GetAppointmentsByTestId(testId);
        }
    }
}
