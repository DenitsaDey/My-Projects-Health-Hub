using HealthHub.Data.Common.Models;

namespace HealthHub.Data.Models
{
    public class InsuranceClinic : BaseDeletableModel<int>
    {
        public string InsuranceID { get; set; }

        public virtual Insurance Insurance { get; set; }

        public string ClinicId { get; set; }

        public virtual Clinic Clinic { get; set; }
    }
}