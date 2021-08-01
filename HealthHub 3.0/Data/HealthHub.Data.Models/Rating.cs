namespace HealthHub.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using HealthHub.Data.Common.Models;

    public class Rating : BaseDeletableModel<string>
    {
        public Rating() => this.Id = Guid.NewGuid().ToString();

        public int Value { get; set; }

        [ForeignKey("Appointment")]
        public string AppointmentId { get; set; }

        public virtual Appointment Appointment { get; set; }

        public string PatientId { get; set; }

        public virtual ApplicationUser Patient { get; set; }

        public string AdditionalComments { get; set; }
    }
}
