namespace HealthHub.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using AngleSharp;
    using HealthHub.Data.Common.Repositories;
    using HealthHub.Data.Models;

    public class CityAreasScraperService : ICityAreasScraperService
    {
        private readonly IBrowsingContext context;
        private readonly IDeletableEntityRepository<CityArea> cityAreaRepository;

        public CityAreasScraperService(IDeletableEntityRepository<CityArea> cityAreaRepository)
        {
            this.cityAreaRepository = cityAreaRepository;

            var config = Configuration.Default.WithDefaultLoader();
            this.context = BrowsingContext.New(config);
        }

        public async Task ImportCityAreas()
        {
            var document = this.context.OpenAsync("https://www.moph.gov.qa/english/OurServices/eservices/Pages/Health-Facilities.aspx")
                .GetAwaiter()
                .GetResult();

            var cityAreas = document.QuerySelectorAll("#ddlAreas")
                .Select(x => x.TextContent)
                .FirstOrDefault()
                .Split("\n\t\t\t\t\t\t\t", StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            foreach (var area in cityAreas.Skip(1))
            {
                var parts = area.Split(new char[] { '/', '\\', }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var part in parts)
                {
                    var newCityArea = new CityArea { Name = part.Trim() };

                    await this.cityAreaRepository.AddAsync(newCityArea);
                    await this.cityAreaRepository.SaveChangesAsync();
                }
            }
        }
    }
}
