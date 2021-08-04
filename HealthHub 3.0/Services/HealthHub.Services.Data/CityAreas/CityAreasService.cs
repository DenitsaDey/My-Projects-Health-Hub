namespace HealthHub.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using HealthHub.Data.Common.Repositories;
    using HealthHub.Data.Models;
    using HealthHub.Web.ViewModels;
    using Microsoft.EntityFrameworkCore;

    public class CityAreasService : ICityAreasService
    {
        private readonly IDeletableEntityRepository<CityArea> cityAreasRepository;

        public CityAreasService(IDeletableEntityRepository<CityArea> cityAreasRepository)
        {
            this.cityAreasRepository = cityAreasRepository;
        }

        public async Task<string> AddAsync(string name)
        {
            await this.cityAreasRepository.AddAsync(new CityArea { Name = name });
            await this.cityAreasRepository.SaveChangesAsync();

            return this.cityAreasRepository.All().FirstOrDefault(ca => ca.Name == name).Id;
        }

        public async Task<IEnumerable<CityAreasViewModel>> GetAllCityAreasAsync()
        {
            return await this.cityAreasRepository.All()
                .Select(p => new CityAreasViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                })
                .OrderBy(x => x.Id)
                .ToListAsync();
        }

        public async Task<CityAreasViewModel> GetByIdAsync(string cityAreaId)
        {
            var cityArea = await this.cityAreasRepository.All()
                .Where(s => s.Id == cityAreaId)
                .Select(s => new CityAreasViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                })
                .FirstOrDefaultAsync();

            return cityArea;
        }
    }
}
