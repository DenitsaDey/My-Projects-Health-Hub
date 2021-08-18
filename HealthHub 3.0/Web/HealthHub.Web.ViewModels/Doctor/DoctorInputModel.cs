namespace HealthHub.Web.ViewModels.Doctor
{
    using System.ComponentModel.DataAnnotations;

    using HealthHub.Common;
    using HealthHub.Data.Models;
    using HealthHub.Data.Models.Enums;
    using HealthHub.Services.Mapping;

    public class DoctorInputModel
    {
        [Required]
        [Display(Name = "First name")]
        [StringLength(
            GlobalConstants.DataValidations.NameMaxLength,
            ErrorMessage = GlobalConstants.ErrorMessages.Name,
            MinimumLength = GlobalConstants.DataValidations.NameMinLength)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        [StringLength(
            GlobalConstants.DataValidations.NameMaxLength,
            ErrorMessage = GlobalConstants.ErrorMessages.Name,
            MinimumLength = GlobalConstants.DataValidations.NameMinLength)]
        public string LastName { get; set; }

        public Gender Gender { get; set; }

        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Image link")]
        [Url]
        public string ImageUrl { get; set; }

        [Required]
        [Display(Name = "Clinic")]
        public string ClinicId { get; set; }

        [Required]
        [Display(Name = "Specialty")]
        public string SpecialtyId { get; set; }

        [Required]
        [Display(Name = "Years of experience")]
        [Range(
            GlobalConstants.DataValidations.YearsofExperienceMinLength,
            GlobalConstants.DataValidations.YearsofExperienceMaxLength,
            ErrorMessage = GlobalConstants.ErrorMessages.YearsofExperience)]
        public int YearsOFExperience { get; set; }

        [Display(Name = "Works with children")]
        public bool WorksWithChildren { get; set; }

        [Display(Name = "Available for online consultations")]
        public bool OnlineConsultation { get; set; }

        [StringLength(
            GlobalConstants.DataValidations.AboutMaxLength,
            ErrorMessage = GlobalConstants.ErrorMessages.About)]
        public string About { get; set; }
    }
}
