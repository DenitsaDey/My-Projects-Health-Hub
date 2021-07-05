using HealthHub.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthHub.Data.Models
{
    public class Image : BaseDeletableModel<string>
    {
        public Image() => this.Id = Guid.NewGuid().ToString();

        public string AddedByUserId { get; set; }

        public virtual ApplicationUser AddedByUser { get; set; }
    }
}
