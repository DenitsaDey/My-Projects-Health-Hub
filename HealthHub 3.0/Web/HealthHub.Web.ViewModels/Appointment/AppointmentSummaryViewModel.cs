namespace HealthHub.Web.ViewModels.Appointment
{
    using System;

    using HealthHub.Data.Models.Enums;

    public class AppointmentSummaryViewModel
    {
        public string Id { get; set; }

        public string DoctorId { get; set; }

        public DateTime AppointmentTime { get; set; }

        public string AppointmentStatus { get; set; }

        public int RatingValue { get; set; }
    }
}
