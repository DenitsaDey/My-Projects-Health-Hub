namespace HealthHub.Web.ViewModels.Home
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public IEnumerable<IndexSpecialtyViewModel> Specialties { get; set; }

        public IEnumerable<IndexCityAreaViewModel> CityAreas { get; set; }

        public CountsViewModel DataCounts { get; set; }
    }
}
