namespace HealthHub.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using HealthHub.Web.ViewModels.Doctor;

    public class HomeHeaderViewModel : HeaderSearchQueryModel
    {
        public CountsViewModel DataCounts { get; set; }

        public IEnumerable<DoctorsSummaryViewModel> Doctors { get; set; }
    }
}
