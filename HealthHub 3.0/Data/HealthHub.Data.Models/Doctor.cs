namespace HealthHub.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using HealthHub.Data.Common.Models;
    using HealthHub.Data.Models.Enums;

    using static HealthHub.Data.Common.DataConstants;

    public class Doctor : BaseDeletableModel<string>
    {
        public Doctor() => this.Id = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(DefaultMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(DefaultMaxLength)]
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        public string PhoneNumber { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public virtual ICollection<Appointment> ScheduledAppointments { get; set; } = new HashSet<Appointment>();

        [Required]
        public string ClinicId { get; set; }

        public virtual Clinic Clinic { get; set; }

        [Required]
        public string SpecialtyId { get; set; }

        public virtual Specialty Specialty { get; set; }

        [Required]
        public int YearsOFExperience { get; set; }

        public bool WorksWithChildren { get; set; }

        public bool OnlineConsultation { get; set; }
    }
}
