namespace HealthHub.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using HealthHub.Data.Common.Models;

    using static HealthHub.Data.Common.DataConstants;

    public class Procedure : BaseDeletableModel<string>
    {
        public Procedure() => this.Id = Guid.NewGuid().ToString();

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<ClinicProcedure> Clinics { get; set; } = new HashSet<ClinicProcedure>();
    }
}
