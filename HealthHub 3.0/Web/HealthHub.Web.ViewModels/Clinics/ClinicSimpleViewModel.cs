namespace HealthHub.Web.ViewModels.Clinics
{
    using HealthHub.Data.Models;
    using HealthHub.Services.Mapping;

    public class ClinicSimpleViewModel : HeaderSearchQueryModel, IMapFrom<Clinic>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
