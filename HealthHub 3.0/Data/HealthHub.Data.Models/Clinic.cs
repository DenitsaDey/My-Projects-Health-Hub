namespace HealthHub.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using HealthHub.Data.Common.Models;

    using static HealthHub.Data.Common.DataConstants;

    public class Clinic : BaseDeletableModel<string>
    {
        public Clinic() => this.Id = Guid.NewGuid().ToString();

        public string Name { get; set; }

        public string AreaId { get; set; }

        public virtual CityArea Area { get; set; }

        [Required]
        [MaxLength(AddressMaxLength)]
        public string Address { get; set; }

        //collection of users that are medical professionals
        public virtual ICollection<DoctorClinic> MedicalProfessionals { get; set; } = new HashSet<DoctorClinic>();

        //collection of appointments
        public virtual ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();

        //collection of Insurance companies the clicl works with
        public virtual ICollection<InsuranceClinic> InsuranceCompanies { get; set; } = new HashSet<InsuranceClinic>();
    }
}
