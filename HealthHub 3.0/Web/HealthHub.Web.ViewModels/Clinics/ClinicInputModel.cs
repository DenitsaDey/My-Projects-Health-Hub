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
        [Display(Name = "Map Url")]
        public string MapUrl { get; set; }

        [Required]
        [Display(Name = "Area")]
        public string AreaId { get; set; }

        public string AreaName { get; set; }

        [Required]
        [MaxLength(AddressMaxLength)]
        public string Address { get; set; }

        [Display(Name = "Medical Staff")]
        public IEnumerable<DoctorsViewModel> MedicalStaff { get; set; }

        [Display(Name = "Insurance Companies")]
        public IEnumerable<InsuranceClinicsViewModel> InsuranceCompanies { get; set; }
    }
}
