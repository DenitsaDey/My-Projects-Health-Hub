namespace HealthHub.Web.ViewModels.Rating
{
    using System.ComponentModel.DataAnnotations;

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
