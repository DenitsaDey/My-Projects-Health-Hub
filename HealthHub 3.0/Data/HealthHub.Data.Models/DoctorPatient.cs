namespace HealthHub.Data.Models
{
    using HealthHub.Data.Common.Models;

    public class DoctorPatient : BaseDeletableModel<int>
    {
        public string PatientId { get; set; }

        public virtual ApplicationUser Patient { get; set; }

        public string DoctorId { get; set; }

        public virtual Doctor Doctor { get; set; }
    }
}
