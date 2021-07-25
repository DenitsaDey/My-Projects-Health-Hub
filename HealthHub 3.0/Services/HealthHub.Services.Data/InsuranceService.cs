namespace HealthHub.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using HealthHub.Data.Common.Repositories;
    using HealthHub.Data.Models;
    using HealthHub.Web.ViewModels;

    public class InsuranceService : IInsuranceService
    {
        private readonly IDeletableEntityRepository<Insurance> insuranceRepository;

        public InsuranceService(IDeletableEntityRepository<Insurance> insuranceRepository)
        {
            this.insuranceRepository = insuranceRepository;
        }

        public IEnumerable<InsuranceViewModel> GetAllInsuranceCompanies()
        {
            return this.insuranceRepository.All()
                .Select(p => new InsuranceViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                }).ToList()
                .OrderBy(x => x.Name);
        }
    }
}
