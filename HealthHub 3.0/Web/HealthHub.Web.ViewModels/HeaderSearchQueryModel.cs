namespace HealthHub.Web.ViewModels
{
    using System;
    using System.Collections.Generic;

    using HealthHub.Data.Models.Enums;
    using HealthHub.Web.ViewModels.Doctor;

    public class HeaderSearchQueryModel
    {
        public string SpecialtyId { get; set; }

        public IEnumerable<SpecialtyViewModel> Specialties { get; set; }

        public string CityAreaId { get; set; }

        public IEnumerable<CityAreasViewModel> CityAreas { get; set; }

        public string SearchName { get; set; }

        public SearchSorting Sorting { get; set; }

        public IEnumerable<DoctorsSummaryViewModel> Doctors { get; set; }

        //public int ItemsPerPage { get; set; }

        //public int PageNumber { get; set; }

        public PagingViewModel Paging { get; set; }

        public CountsViewModel DataCounts { get; set; }
    }
}
