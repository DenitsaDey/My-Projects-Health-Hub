namespace HealthHub.Web.ViewModels.Clinics
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using HealthHub.Web.ViewModels.Doctor;

    using static HealthHub.Data.Common.DataConstants;

    public class ClinicInputModel
    {
        [Required]
        [MaxLength(ClinicNameMaxLength)]
        public string Name { get; set; }

        [Required]
        public string MapUrl { get; set; }

        [Required]
        public string AreaId { get; set; }

        public string AreaName { get; set; }

        [Required]
        [MaxLength(AddressMaxLength)]
        public string Address { get; set; }

        public IEnumerable<DoctorsViewModel> MedicalStaff { get; set; }

        public IEnumerable<InsuranceClinicsViewModel> InsuranceCompanies { get; set; }
    }
}
