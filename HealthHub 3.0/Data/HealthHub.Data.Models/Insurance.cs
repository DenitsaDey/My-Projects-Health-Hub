namespace HealthHub.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using HealthHub.Data.Common.Models;

    using static HealthHub.Data.Common.DataConstants;

    public class Insurance : BaseDeletableModel<string>
    {
        public Insurance() => this.Id = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(DefaultMaxLength)]
        public string Name { get; set; }

        public virtual ICollection<InsuranceClinic> Clinics { get; set; } = new HashSet<InsuranceClinic>();
    }
}
