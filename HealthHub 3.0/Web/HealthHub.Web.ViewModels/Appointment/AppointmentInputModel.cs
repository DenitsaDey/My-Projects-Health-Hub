﻿namespace HealthHub.Web.ViewModels.Appointment
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AppointmentInputModel
    {
        [Required]
        [Display(Name = "Choose A Service")]
        public string ServiceId { get; set; }

        [Required]
        [Display(Name = "Appointent Time")]

        public string AppointmentTime { get; set; }

        //[Required]
        [Display(Name ="Additional Notes")]
        //[StringLength(200, MinimumLength = 10, ErrorMessage ="Text should be between {2} and {1} characters.")]
        //[MinLength(10)]
        public string Message { get; set; }

        public IEnumerable<ServicesViewModel> ServicesItems { get; set; }
    }
}