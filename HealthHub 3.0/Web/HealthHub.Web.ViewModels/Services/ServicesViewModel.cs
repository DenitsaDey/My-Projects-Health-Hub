namespace HealthHub.Web.ViewModels
{
    using HealthHub.Data.Models;
    using HealthHub.Services.Mapping;

    public class ServicesViewModel : IMapFrom<Service>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
