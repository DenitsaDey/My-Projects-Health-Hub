namespace HealthHub.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using HealthHub.Data.Common.Models;

    using static HealthHub.Data.Common.DataConstants;

    public class CityArea : BaseDeletableModel<string>
    {
        public CityArea() => this.Id = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(CityAreaMaxLength)]
        public string Name { get; set; }

        public virtual ICollection<Clinic> Clinics { get; set; } = new HashSet<Clinic>();
    }
}
