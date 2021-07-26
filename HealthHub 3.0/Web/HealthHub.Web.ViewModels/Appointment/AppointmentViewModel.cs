namespace HealthHub.Web.ViewModels.Appointment
{
    using System;

    using HealthHub.Data.Models.Enums;

    public class AppointmentViewModel
    {
        public string Id { get; set; }

        public string DoctorId { get; set; }

        public string DoctorName { get; set; }

        public string ClinicId { get; set; }

        public string Clinic { get; set; }

        public string Address { get; set; }

        public string Location { get; set; }

        public string ServiceId { get; set; }

        public string ProcedureBooked { get; set; }

        public DateTime AppointmentTime { get; set; }

        public string AppointmentStatus { get; set; }

        public string Message { get; set; }

        public bool? HasBeenVoted { get; set; }

        public int RatingValue { get; set; }
    }
}
