namespace HealthHub.Web.ViewModels
{
    using HealthHub.Web.ViewModels.Clinics;
    using HealthHub.Web.ViewModels.Doctor;
    using System.Collections.Generic;

    public class HeaderSearchQueryModel
    {
        public string ClinicId { get; set; }

        public IEnumerable<ClinicViewModel> Clinics { get; set; }

        public string SearchName { get; set; }
    }
}
