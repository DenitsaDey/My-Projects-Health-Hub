namespace HealthHub.Data.Models
{
    using System;
    using System.Collections.Generic;

    using HealthHub.Data.Common.Models;

    public class Insurance : BaseDeletableModel<string>
    {
        public Insurance() => this.Id = Guid.NewGuid().ToString();

        public string Name { get; set; }

        public virtual ICollection<InsuranceClinic> Clinics { get; set; } = new HashSet<InsuranceClinic>();
    }
}
