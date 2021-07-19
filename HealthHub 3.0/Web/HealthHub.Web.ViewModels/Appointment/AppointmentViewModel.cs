namespace HealthHub.Web.ViewModels.Appointment
{
    using System;

    using HealthHub.Data.Models.Enums;

    public class AppointmentViewModel
    {
        public string DoctorId { get; set; }

        public string ClinicId { get; set; }

        public string Location { get; set; }

        public string ProcedureBookedId { get; set; }

        public DateTime AppointmentTime { get; set; }

        public AppointmentStatus AppointmentStatus { get; set; }

        public string Message { get; set; }

        public bool HasBeenVoted { get; set; }

        public string RatingId { get; set; }
    }
}
