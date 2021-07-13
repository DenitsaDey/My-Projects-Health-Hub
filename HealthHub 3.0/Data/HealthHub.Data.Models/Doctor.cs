using HealthHub.Data.Common.Models;
using HealthHub.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HealthHub.Data.Models
{
    public class Doctor : BaseDeletableModel<string>
    {
        public Doctor() => this.Id = Guid.NewGuid().ToString();

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        public string ImageUrl { get; set; }

        public virtual ICollection<Appointment> ScheduledAppointments { get; set; } = new HashSet<Appointment>();

        public string ClinicId { get; set; }

        public virtual Clinic Clinic { get; set; }

        public string SpecialtyId { get; set; }

        public virtual Specialty Specialty { get; set; }

        public int YearsOFExperience { get; set; }

        public bool WorksWithChildren { get; set; }

        public bool OnlineConsultation { get; set; }
    }
}
