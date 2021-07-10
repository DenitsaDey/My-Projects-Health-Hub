namespace HealthHub.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using HealthHub.Data.Common.Models;
    using HealthHub.Data.Models.Enums;
    using static HealthHub.Data.Common.DataConstants;

    public class Diagnosis : BaseDeletableModel<string>
    {
        public Diagnosis() => this.Id = Guid.NewGuid().ToString();

        [Required]
        public string MedicalCondition { get; set; }

        public virtual ICollection<UserDiagnosis> Patients { get; set; } = new HashSet<UserDiagnosis>();
    }
}
