namespace HealthHub.Web.ViewModels.Clinics
{
    using System.Collections.Generic;

    using HealthHub.Data.Models.Enums;

    public class ClinicFilterViewModel : HeaderSearchQueryModel
    {
        public ClinicViewModel Clinic { get; set; }

        public PagingViewModel Paging { get; set; }

        public IEnumerable<ClinicViewModel> FilteredClinics { get; set; }

        public string CityAreaId { get; set; }

        public IEnumerable<CityAreasViewModel> CityAreas { get; set; }

        public string SpecialtyId { get; set; }

        public IEnumerable<SpecialtyViewModel> Specialties { get; set; }

        public int ClinicsCount { get; set; }

        public SearchSorting Sorting { get; set; }

        public string InsuranceId { get; set; }

        public IEnumerable<InsuranceViewModel> InsuranceCompanies { get; set; }
    }
}
