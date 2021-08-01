namespace HealthHub.Web.ViewModels.Rating
{
    using System.ComponentModel.DataAnnotations;

    using HealthHub.Common;

    public class RatingInputModel
    {
        public string Id { get; set; }

        [Required]
        public string AppointmentId { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = GlobalConstants.ErrorMessages.Rating)]
        public int RateValue { get; set; }

        [MaxLength(200)]
        public string AdditionalComments { get; set; }

        public string DoctorId { get; set; }

        public string DoctorName { get; set; }

        public string DoctorSpecialty { get; set; }

        public string DoctorClinic { get; set; }

        public string DoctorImg { get; set; }

        public bool? HasBeenVoted { get; set; }
    }
}
