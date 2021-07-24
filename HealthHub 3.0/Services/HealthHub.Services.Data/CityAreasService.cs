namespace HealthHub.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using HealthHub.Data.Common.Repositories;
    using HealthHub.Data.Models;
    using HealthHub.Web.ViewModels;

    public class CityAreasService : ICityAreasService
    {
        private readonly IDeletableEntityRepository<CityArea> cityAreasRepository;

        public CityAreasService(IDeletableEntityRepository<CityArea> cityAreasRepository)
        {
            this.cityAreasRepository = cityAreasRepository;
        }

        public IEnumerable<CityAreasViewModel> GetAllCityAreas()
        {
            return this.cityAreasRepository.All()
                .Select(p => new CityAreasViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                }).ToList()
                .OrderBy(x => x.Id);
        }
    }
}
