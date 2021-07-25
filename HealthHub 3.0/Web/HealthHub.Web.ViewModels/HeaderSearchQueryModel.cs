namespace HealthHub.Web.ViewModels
{
    using System.Collections.Generic;

    public class HeaderSearchQueryModel
    {
        public string SpecialtyId { get; set; }

        public IEnumerable<SpecialtyViewModel> Specialties { get; set; }

        public string CityAreaId { get; set; }

        public IEnumerable<CityAreasViewModel> CityAreas { get; set; }

        public string SearchName { get; set; }
    }
}
