namespace HealthHub.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HealthHub.Data.Common.Repositories;
    using HealthHub.Data.Models;
    using HealthHub.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class SpecialtiesService : ISpecialtiesService
    {
        private readonly IDeletableEntityRepository<Specialty> specialtiesRepository;

        public SpecialtiesService(IDeletableEntityRepository<Specialty> specialtiesRepository)
        {
            this.specialtiesRepository = specialtiesRepository;
        }

        public async Task AddAsync(string name)
        {
            await this.specialtiesRepository.AddAsync(new Specialty { Name = name });
            await this.specialtiesRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllSpecialtiesAsync<T>()
        {
            return await this.specialtiesRepository.All()
                .OrderBy(x => x.Name)
                .To<T>()
                .ToListAsync();
        }

        public async Task<T> GetByIdAsync<T>(string specialtyId)
        {
            var specialty = await this.specialtiesRepository.AllAsNoTracking()
                .Where(s => s.Id == specialtyId)
                .To<T>()
                .FirstOrDefaultAsync();

            return specialty;
        }
    }
}
