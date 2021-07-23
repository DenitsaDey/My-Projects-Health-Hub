namespace HealthHub.Web.ViewModels.Home
{
    using HealthHub.Web.ViewModels.Doctor;
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public IEnumerable<SpecialtyViewModel> Specialties { get; set; }

        public IEnumerable<CityAreasViewModel> CityAreas { get; set; }

        public IEnumerable<DoctorsSummaryViewModel> Doctors { get; set; }

        public CountsViewModel DataCounts { get; set; }
    }
}
