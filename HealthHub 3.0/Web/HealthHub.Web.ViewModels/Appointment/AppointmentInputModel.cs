namespace HealthHub.Web.ViewModels.Appointment
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using HealthHub.Common;
    using HealthHub.Web.ViewModels.Common.CustomValidationAttributes;

    public class AppointmentInputModel
    {
        public string DoctorId { get; set; }

        [Required]
        [Display(Name = "Choose A Service")]
        public string ServiceId { get; set; }

        [Required]
        [Display(Name = "Appointent Date")]
        [ValidateDateString(ErrorMessage = GlobalConstants.ErrorMessages.DateTime)]
        public string AppointmentDate { get; set; }

        [Required]
        [Display(Name = "Appointent Time")]
        [ValidateTimeString(ErrorMessage = GlobalConstants.ErrorMessages.DateTime)]
        public string AppointmentTime { get; set; }

        [Display(Name ="Additional Notes")]

        // [StringLength(200, MinimumLength = 10, ErrorMessage ="Text should be between {2} and {1} characters.")]
        public string Message { get; set; }

        public IEnumerable<ServicesViewModel> ServicesItems { get; set; }
    }
}
