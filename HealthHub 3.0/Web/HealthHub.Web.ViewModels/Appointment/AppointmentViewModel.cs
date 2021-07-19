namespace HealthHub.Web.ViewModels.Appointment
{
    using System;

    using HealthHub.Data.Models.Enums;

    public class AppointmentViewModel
    {
        public string Id { get; set; }

        public string Doctor { get; set; }

        public string Clinic { get; set; }

        public string Address { get; set; }

        public string Location { get; set; }

        public string ProcedureBooked { get; set; }

        public DateTime AppointmentTime { get; set; }

        public AppointmentStatus AppointmentStatus { get; set; }

        public string Message { get; set; }

        public bool HasBeenVoted { get; set; }

        public int Rating { get; set; }
    }
}
