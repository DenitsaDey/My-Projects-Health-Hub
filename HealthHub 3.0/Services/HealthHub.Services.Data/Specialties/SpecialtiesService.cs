namespace HealthHub.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using HealthHub.Data.Common.Repositories;
    using HealthHub.Data.Models;
    using HealthHub.Services.Mapping;
    using HealthHub.Web.ViewModels;
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

        public IEnumerable<string> GetAllSpecialtiesNames()
        {
            return this.specialtiesRepository.All()
                .OrderBy(s => s.Name)
                .Select(s => s.Name)
                .ToList();
        }

        // for Admin Area / Doctors Controller/ Create
        public IEnumerable<string> GetAllSpecialtiesIds()
        {
            return this.specialtiesRepository.All()
                .Select(x => x.Id)
                .ToList();
        }

        public async Task<T> GetByIdAsync<T>(string specialtyId)
        {
            var specialty = await this.specialtiesRepository.All()
                .Where(s => s.Id == specialtyId)
                .To<T>()
                .FirstOrDefaultAsync();

            return specialty;
        }
    }
}
