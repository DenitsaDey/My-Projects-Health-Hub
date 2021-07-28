namespace HealthHub.Web.ViewModels.Clinics
{
    using System.Collections.Generic;

    using HealthHub.Web.ViewModels.Doctor;

    public class ClinicViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string MapUrl { get; set; }

        public string AreaId { get; set; }

        public CityAreasViewModel Area { get; set; }

        public string Address { get; set; }

        public string DoctorId { get; set; }

        public IEnumerable<DoctorsViewModel> MedicalStaff { get; set; }

        //collection of Insurance companies the clicl works with
        public IEnumerable<InsuranceViewModel> InsuranceCompanies { get; set; }

        public double AverageRating { get; set; }
    }
}
