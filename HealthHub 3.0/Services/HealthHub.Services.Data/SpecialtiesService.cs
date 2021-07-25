namespace HealthHub.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using HealthHub.Data.Common.Repositories;
    using HealthHub.Data.Models;
    using HealthHub.Web.ViewModels;

    public class SpecialtiesService : ISpecialtiesService
    {
        private readonly IDeletableEntityRepository<Specialty> specialtiesRepository;

        public SpecialtiesService(IDeletableEntityRepository<Specialty> specialtiesRepository)
        {
            this.specialtiesRepository = specialtiesRepository;
        }

        public IEnumerable<SpecialtyViewModel> GetAllSpecialties()
        {
            return this.specialtiesRepository.All()
                .Select(s => new SpecialtyViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                })
                .ToList()
                .OrderBy(x => x.Name);
        }

        public IEnumerable<string> GetAllSpecialtiesNames()
        {
            return this.specialtiesRepository.All()
                .OrderBy(s => s.Name)
                .Select(s => s.Name)
                .ToList();
        }
    }
}
