namespace HealthHub.Data.Models
{
    using HealthHub.Data.Common.Models;

    public class DoctorClinic : BaseDeletableModel<int>
    {
        public string MedicalProfessionalId { get; set; }

        public virtual ApplicationUser MedicalProfessional { get; set; }

        public string ClinicId { get; set; }

        public virtual Clinic Clinic { get; set; }
    }
}
