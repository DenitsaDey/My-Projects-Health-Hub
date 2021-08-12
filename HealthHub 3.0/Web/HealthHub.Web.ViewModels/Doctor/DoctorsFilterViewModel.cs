namespace HealthHub.Web.ViewModels.Doctor
{
    using System.Collections.Generic;

    using HealthHub.Data.Models.Enums;

    public class DoctorsFilterViewModel : HeaderSearchQueryModel
    {
        public IEnumerable<DoctorsViewModel> Doctors { get; set; }

        public string CityAreaId { get; set; }

        public IEnumerable<CityAreasViewModel> CityAreas { get; set; }

        public string SpecialtyId { get; set; }

        public IEnumerable<SpecialtyViewModel> Specialties { get; set; }

        public int DoctorsCount { get; set; }

        public PagingViewModel Paging { get; set; }

        public SearchSorting Sorting { get; set; }

        public bool OnlineConsultation { get; set; }

        public bool WorksWithChildren { get; set; }

        public string InsuranceId { get; set; }

        public IEnumerable<InsuranceViewModel> InsuranceCompanies { get; set; }

        public Gender Gender { get; set; }
    }
}
