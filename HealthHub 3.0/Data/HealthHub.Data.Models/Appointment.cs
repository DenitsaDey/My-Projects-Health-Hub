namespace HealthHub.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using HealthHub.Data.Common.Models;
    using HealthHub.Data.Models.Enums;

    using static HealthHub.Data.Common.DataConstants;

    public class Appointment : BaseDeletableModel<string>
    {
        public Appointment() => this.Id = Guid.NewGuid().ToString();

        public DateTime AppointmentTime { get; set; }

        [Required]
        public string ServiceId { get; set; }

        public virtual Service ProcedureBooked { get; set; }

        [MaxLength(MessageMaxLength)]
        public string Message { get; set; }

        [Required]
        public string PatientId { get; set; }

        public ApplicationUser Patient { get; set; }

        [Required]
        public string DoctorId { get; set; }

        public Doctor Doctor { get; set; }

        public AppointmentStatus AppointmentStatus { get; set; }

        // For every past (and confirmed) appointment the Patient can Rate the Doctor
        // But rating can be given only once for each appointment
        public bool HasBeenVoted { get; set; }

        public string RatingId { get; set; }

        public virtual Rating Rating { get; set; }
    }
}
