﻿namespace HealthHub.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using HealthHub.Data.Common.Models;

    public class Specialty : BaseDeletableModel<string>
    {
        public Specialty() => this.Id = Guid.NewGuid().ToString();

        public string Name { get; set; }

        public string DoctorId { get; set; }

        public virtual ApplicationUser Doctor { get; set; }
    }
}
