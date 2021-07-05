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
        public string MedicalCondition { get; set; }

        //yes/no - like IsFixed in CarShop
        public bool CurrentlyTreated { get; set; }

        //if treated - not yet treated, currently treated, cured, refused treatment, unavailable treatment - like 'Keyword' in Batlle Cards
        public string HealthStatus { get; set; }

        //history of condition and treatment applied so far
        public string Description { get; set; }

        public virtual ICollection<UserDiagnosis> Patients { get; set; } = new HashSet<UserDiagnosis>();
    }
}
