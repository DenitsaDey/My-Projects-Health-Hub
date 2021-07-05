namespace HealthHub.Data.Models
{
    using System;

    using HealthHub.Data.Common.Models;

    public class Setting : BaseDeletableModel<string>
    {
        public Setting() => this.Id = Guid.NewGuid().ToString();
        
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
