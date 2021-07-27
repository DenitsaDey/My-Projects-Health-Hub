namespace HealthHub.Web.ViewModels.Home
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using HealthHub.Web.ViewModels.Doctor;

    public class HomeHeaderViewModel : HeaderSearchQueryModel
    {
        public CountsViewModel DataCounts { get; set; }

        public IEnumerable<DoctorsViewModel> Doctors { get; set; }
    }
}
