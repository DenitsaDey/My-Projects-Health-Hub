namespace HealthHub.Data.Models
{
    using System;
    using System.Collections.Generic;

    using HealthHub.Data.Common.Models;

    public class CityArea : BaseDeletableModel<string>
    {
        public CityArea() => this.Id = Guid.NewGuid().ToString();

        public string Name { get; set; }

        public virtual ICollection<Clinic> Clinics { get; set; } = new HashSet<Clinic>();
    }
}
