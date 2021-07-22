using HealthHub.Data.Models.Enums;
using HealthHub.Web.ViewModels.Doctor;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthHub.Web.ViewModels
{
    public class HeaderSearchQueryModel
    {
        public IEnumerable<SpecialtyViewModel> Specialties { get; set; }

        public IEnumerable<CityAreasViewModel> CityAreas { get; set; }

        public IEnumerable<string> SearchName { get; set; }

        public SearchSorting Sorting { get; set; }

        public IEnumerable<DoctorsSummaryViewModel> Doctors { get; set; }
    }
}
