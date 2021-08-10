namespace HealthHub.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using HealthHub.Data.Common.Models;

    using static HealthHub.Data.Common.DataConstants;

    public class Service : BaseDeletableModel<string>
    {
        public Service() => this.Id = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(DefaultMaxLength)]
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();
    }
}
