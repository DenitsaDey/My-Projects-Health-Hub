namespace HealthHub.Web.ViewModels.Home
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public IEnumerable<SpecialtyViewModel> Specialties { get; set; }

        public IEnumerable<CityAreasViewModel> CityAreas { get; set; }

        public CountsViewModel DataCounts { get; set; }
    }
}
