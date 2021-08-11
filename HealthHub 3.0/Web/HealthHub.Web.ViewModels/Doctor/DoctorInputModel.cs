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
        [StringLength(
            GlobalConstants.DataValidations.NameMaxLength,
            ErrorMessage = GlobalConstants.ErrorMessages.Name,
            MinimumLength = GlobalConstants.DataValidations.NameMinLength)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(
            GlobalConstants.DataValidations.NameMaxLength,
            ErrorMessage = GlobalConstants.ErrorMessages.Name,
            MinimumLength = GlobalConstants.DataValidations.NameMinLength)]
        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public string PhoneNumber { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }

        [Required]
        public string ClinicId { get; set; }

        [Required]
        public string SpecialtyId { get; set; }

        [Required]
        [MaxLength(
            GlobalConstants.DataValidations.YearsofExperienceMaxLength,
            ErrorMessage = GlobalConstants.ErrorMessages.YearsofExperience)]
        [MinLength(
            GlobalConstants.DataValidations.YearsofExperienceMinLength,
            ErrorMessage = GlobalConstants.ErrorMessages.YearsofExperience)]
        public int YearsOFExperience { get; set; }

        public bool WorksWithChildren { get; set; }

        public bool OnlineConsultation { get; set; }

        [StringLength(
            GlobalConstants.DataValidations.AboutMaxLength,
            ErrorMessage = GlobalConstants.ErrorMessages.About)]
        public string About { get; set; }
    }
}
