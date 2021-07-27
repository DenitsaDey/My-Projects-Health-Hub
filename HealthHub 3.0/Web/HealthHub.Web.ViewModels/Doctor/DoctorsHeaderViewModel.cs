namespace HealthHub.Web.ViewModels.Doctor
{
    using System.Collections.Generic;

    using HealthHub.Data.Models.Enums;

    public class DoctorsHeaderViewModel : HeaderSearchQueryModel
    {
        public IEnumerable<DoctorsViewModel> Doctors { get; set; }

        public PagingViewModel Paging { get; set; }

        public SearchSorting Sorting { get; set; }

        public bool OnlineConsultation { get; set; }

        public bool WorksWithChildren { get; set; }

        public string InsuranceId { get; set; }

        public IEnumerable<InsuranceViewModel> InsuranceCompanies { get; set; }

        public Gender Gender { get; set; }
    }
}
