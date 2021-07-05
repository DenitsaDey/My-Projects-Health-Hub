namespace HealthHub.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using HealthHub.Data.Common.Models;

    using static HealthHub.Data.Common.DataConstants;

    public class Appointment : BaseDeletableModel<string>
    {
        public Appointment() => this.Id = Guid.NewGuid().ToString();
        
        //зa да показва само дата без час използваме атрибут [DataType(...Date)] 
        //или във view-то направо с DateTime.UTCNow.ToString("dd-MM-yyyy")
        public DateTime AppointmentTime { get; set; }

        public string ClinicId { get; set; }

        public virtual Clinic Location { get; set; }

        //patients issue description
        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        //not required
        public string ReferralId { get; set; }

        public virtual Referral Referral { get; set; }
    }
}
