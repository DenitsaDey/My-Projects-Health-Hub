namespace HealthHub.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using HealthHub.Data.Common.Repositories;
    using HealthHub.Data.Models;
    using HealthHub.Services.Mapping;
    using HealthHub.Web.ViewModels;

    public class InsuranceService : IInsuranceService
    {
        private readonly IDeletableEntityRepository<Insurance> insuranceRepository;

        public InsuranceService(IDeletableEntityRepository<Insurance> insuranceRepository)
        {
            this.insuranceRepository = insuranceRepository;
        }

        public IEnumerable<T> GetAllInsuranceCompanies<T>()
        {
            return this.insuranceRepository.All()
                .OrderBy(x => x.Name)
                .To<T>()
                .ToList();
        }
    }
}
