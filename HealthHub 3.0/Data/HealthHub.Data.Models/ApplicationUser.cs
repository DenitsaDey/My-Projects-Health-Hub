// ReSharper disable VirtualMemberCallInConstructor
namespace HealthHub.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using HealthHub.Data.Common.Models;
    using HealthHub.Data.Models.Enums;
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        public string ImageUrl { get; set; }

        [InverseProperty("Patient")]
        public virtual ICollection<Appointment> BookedAppointments { get; set; } = new HashSet<Appointment>();

        [InverseProperty("Doctor")]
        public virtual ICollection<Appointment> ScheduledAppointments { get; set; } = new HashSet<Appointment>();

        //properties of Doctors only 
        public string ClinicId { get; set; }

        public virtual Clinic Clinic { get; set; }

        public string SpecialtyId { get; set; }

        public virtual Specialty Specialty { get; set; }

        public int YearsOFExperience { get; set; }

        public bool WorksWithChildren { get; set; }

        public bool OnlineConsultation { get; set; }

        public ICollection<Rating> Rating { get; set; } = new HashSet<Rating>();

        public double OverallRating => this.Rating.Any() ? 0 : this.Rating.Average(r => r.Value);

        //properties of Patients only
        public virtual ICollection<UserDiagnosis> MedicalHistory { get; set; } = new HashSet<UserDiagnosis>();

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

    }
}
