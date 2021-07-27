namespace HealthHub.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using HealthHub.Data.Common.Repositories;
    using HealthHub.Data.Models;
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

        public async Task<IEnumerable<SpecialtyViewModel>> GetAllSpecialtiesAsync()
        {
            return await this.specialtiesRepository.All()
                .Select(s => new SpecialtyViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                })
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public IEnumerable<string> GetAllSpecialtiesNames()
        {
            return this.specialtiesRepository.All()
                .OrderBy(s => s.Name)
                .Select(s => s.Name)
                .ToList();
        }

        public async Task<SpecialtyViewModel> GetByIdAsync(string specialtyId)
        {
            var specialty = await this.specialtiesRepository.All()
                .Where(s => s.Id == specialtyId)
                .Select(s => new SpecialtyViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                })
                .FirstOrDefaultAsync();

            return specialty;
        }
    }
}
