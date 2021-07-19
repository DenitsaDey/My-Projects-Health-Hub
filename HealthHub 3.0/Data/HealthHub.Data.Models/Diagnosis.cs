namespace HealthHub.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using HealthHub.Data.Common.Models;

    using static HealthHub.Data.Common.DataConstants;

    public class Diagnosis : BaseDeletableModel<string>
    {
        public Diagnosis() => this.Id = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(DefaultMaxLength)]
        public string Name { get; set; }

        public virtual ICollection<UserDiagnosis> Patients { get; set; } = new HashSet<UserDiagnosis>();
    }
}
