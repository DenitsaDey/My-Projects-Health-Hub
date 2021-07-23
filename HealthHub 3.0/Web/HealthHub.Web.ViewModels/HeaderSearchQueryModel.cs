using HealthHub.Data.Models.Enums;
using HealthHub.Web.ViewModels.Doctor;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthHub.Web.ViewModels
{
    public class HeaderSearchQueryModel
    {
        public string SpecialtyId { get; set; }

        public IEnumerable<SpecialtyViewModel> Specialties { get; set; }

        public string CityAreaId { get; set; }

        public IEnumerable<CityAreasViewModel> CityAreas { get; set; }

        public string SearchName { get; set; }

        public SearchSorting Sorting { get; set; }

        public IEnumerable<DoctorsSummaryViewModel> Doctors { get; set; }
    }
}
