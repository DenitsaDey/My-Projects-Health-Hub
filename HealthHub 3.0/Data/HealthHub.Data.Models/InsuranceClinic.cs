namespace HealthHub.Data.Models
{
    using HealthHub.Data.Common.Models;

    public class InsuranceClinic : BaseDeletableModel<int>
    {
        public string InsuranceId { get; set; }

        public virtual Insurance Insurance { get; set; }

        public string ClinicId { get; set; }

        public virtual Clinic Clinic { get; set; }
    }
}