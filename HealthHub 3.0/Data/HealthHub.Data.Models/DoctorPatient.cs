namespace HealthHub.Data.Models
{
    using HealthHub.Data.Common.Models;

    public class DoctorPatient : BaseDeletableModel<int>
    {
        public string PatientId { get; set; }

        public virtual ApplicationUser Patient { get; set; }

        public string MedicalProfessionalId { get; set; }

        public virtual ApplicationUser MedicalProfessional { get; set; }
    }
}
