namespace HealthHub.Web.ViewModels.Clinics
{
    using System.Collections.Generic;

    using HealthHub.Web.ViewModels.Doctor;

    public class ClinicViewModel : HeaderSearchQueryModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string MapUrl { get; set; }

        public string AreaId { get; set; }

        public string AreaName { get; set; }

        public string Address { get; set; }

        public IEnumerable<DoctorsViewModel> MedicalStaff { get; set; }

        public IEnumerable<InsuranceClinicsViewModel> InsuranceCompanies { get; set; }

        public double AverageRating { get; set; }

        public int RatingCount { get; set; }
    }
}
