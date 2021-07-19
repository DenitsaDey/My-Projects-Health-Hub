using System.ComponentModel.DataAnnotations;

namespace HealthHub.Web.ViewModels.Rating
{
    public class RatingInputModel
    {
        [Required]
        public string AppointmentId { get; set; }

        [Required]
        [Range(1, 5)]
        public int Value { get; set; }

        [MaxLength(200)]
        public string AdditionalComments { get; set; }
    }
}
