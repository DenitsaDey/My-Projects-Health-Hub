namespace HealthHub.Web.ViewModels
{
    using HealthHub.Data.Models;
    using HealthHub.Services.Mapping;

    public class CityAreasViewModel : IMapFrom<CityArea>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
