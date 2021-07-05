namespace HealthHub.Data.Models
{
    using HealthHub.Data.Common.Models;

    public class UserDiagnosis : BaseDeletableModel<int>
    {
        public string DiagnosisId { get; set; }

        public virtual Diagnosis Diagnosis { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser Patient { get; set; }
    }
}
