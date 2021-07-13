namespace HealthHub.Data.Models
{
    using HealthHub.Data.Common.Models;
    using HealthHub.Data.Models.Enums;

    public class UserDiagnosis : BaseDeletableModel<int>
    {
        public string DiagnosisId { get; set; }

        public virtual Diagnosis Diagnosis { get; set; }

        public string PatientId { get; set; }

        public virtual ApplicationUser Patient { get; set; }

        //yes/no - like IsFixed in CarShop or like 'Keyword' in Batlle Cards
        public MedicalConditionStatus CurrentStatus { get; set; }

        public string AdditionalDetails { get; set; }
    }
}
