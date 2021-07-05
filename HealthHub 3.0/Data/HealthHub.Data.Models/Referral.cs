namespace HealthHub.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using HealthHub.Data.Common.Models;

    using static HealthHub.Data.Common.DataConstants;

    public class Referral : BaseDeletableModel<string>
    {
        public Referral() => this.Id = Guid.NewGuid().ToString();

        [Required]
        public string Description { get; set; }

        public DateTime IssuedOn { get; set; }

        public DateTime ValidUntil { get; set; }

        public bool IsAssigned { get; set; }

        [Required]
        //patient
        public string PatientId { get; set; }

        public virtual ApplicationUser Patient { get; set; }

        //medical professional if assigned
        public string MedicalProfessionalId { get; set; }

        public virtual ApplicationUser MedicalProfessional { get; set; }

        //if used for an appointment yet
        public string AppointmendId { get; set; }

        public virtual Appointment ScheduledAppointment { get; set; }
    }
}
