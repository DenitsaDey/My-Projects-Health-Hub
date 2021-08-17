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
        private readonly IDeletableEntityRepository<InsuranceClinic> insuranceClinicsRepository;

        public InsuranceService(
            IDeletableEntityRepository<Insurance> insuranceRepository,
            IDeletableEntityRepository<InsuranceClinic> insuranceClinicsRepository)
        {
            this.insuranceRepository = insuranceRepository;
            this.insuranceClinicsRepository = insuranceClinicsRepository;
        }

        public IEnumerable<T> GetAllInsuranceCompanies<T>()
        {
            return this.insuranceRepository.All()
                .OrderBy(x => x.Name)
                .To<T>()
                .ToList();
        }

        // for Admin / Clinics / Edit 
        public IEnumerable<T> GetAllByClinicId<T>(string clinicId)
        {
            return this.insuranceClinicsRepository.All()
                .Where(ic => ic.ClinicId == clinicId)
                .To<T>()
                .ToList();
        }
    }
}
