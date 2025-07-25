using System;
using System.Collections.Generic;
using ContactsDataAccessLayer;

namespace ContactsBusinessLayer
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

        public static List<clsReservation> GetAllReservations()
        {
            List<clsReservation> reservations = new List<clsReservation>();
            var dalReservations = clsReservationsData.GetAllReservationsData();

            foreach (var dalReservation in dalReservations)
            {
                clsReservation reservation = new clsReservation
                {
                    ReservationId = dalReservation.ReservationId,
                    LicenseId = dalReservation.LicenseId,
                    ReservationDate = dalReservation.ReservationDate,
                    ResolveDate = dalReservation.ResolveDate,
                    Description = dalReservation.Description,
                    Reason = dalReservation.Reason,
                    Penalty = dalReservation.Penalty,
                    CreatedBy = dalReservation.CreatedBy
                };
                reservations.Add(reservation);
            }

            return reservations;
        }

        public static clsReservation GetReservationById(int reservationId)
        {
            var dalReservation = clsReservationsData.GetReservationById(reservationId);
            if (dalReservation == null)
                return null;

            return new clsReservation
            {
                ReservationId = dalReservation.ReservationId,
                LicenseId = dalReservation.LicenseId,
                ReservationDate = dalReservation.ReservationDate,
                ResolveDate = dalReservation.ResolveDate,
                Description = dalReservation.Description,
                Reason = dalReservation.Reason,
                Penalty = dalReservation.Penalty,
                CreatedBy = dalReservation.CreatedBy
            };
        }

        public static List<clsReservation> GetReservationsByCreatedBy(string createdBy)
        {
            List<clsReservation> reservations = new List<clsReservation>();
            var dalReservations = clsReservationsData.GetReservationsByCreatedBy(createdBy);

            foreach (var dalReservation in dalReservations)
            {
                clsReservation reservation = new clsReservation
                {
                    ReservationId = dalReservation.ReservationId,
                    LicenseId = dalReservation.LicenseId,
                    ReservationDate = dalReservation.ReservationDate,
                    ResolveDate = dalReservation.ResolveDate,
                    Description = dalReservation.Description,
                    Reason = dalReservation.Reason,
                    Penalty = dalReservation.Penalty,
                    CreatedBy = dalReservation.CreatedBy
                };
                reservations.Add(reservation);
            }

            return reservations;
        }

        public void AddReservation()
        {
            ContactsDataAccessLayer.clsReservation dalReservation = new ContactsDataAccessLayer.clsReservation
            {
                ReservationId = this.ReservationId,
                LicenseId = this.LicenseId,
                ReservationDate = this.ReservationDate,
                ResolveDate = this.ResolveDate,
                Description = this.Description,
                Reason = this.Reason,
                Penalty = this.Penalty,
                CreatedBy = this.CreatedBy
            };

            clsReservationsData.AddReservation(dalReservation);
        }

        public void UpdateReservation()
        {
            ContactsDataAccessLayer.clsReservation dalReservation = new ContactsDataAccessLayer.clsReservation
            {
                ReservationId = this.ReservationId,
                LicenseId = this.LicenseId,
                ReservationDate = this.ReservationDate,
                ResolveDate = this.ResolveDate,
                Description = this.Description,
                Reason = this.Reason,
                Penalty = this.Penalty,
                CreatedBy = this.CreatedBy
            };

            clsReservationsData.UpdateReservation(dalReservation);
        }

        public static void DeleteReservation(int reservationId)
        {
            clsReservationsData.DeleteReservation(reservationId);
        }
    }
}
