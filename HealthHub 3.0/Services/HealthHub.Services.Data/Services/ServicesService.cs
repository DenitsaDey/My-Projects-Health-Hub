namespace HealthHub.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using HealthHub.Data.Common.Repositories;
    using HealthHub.Data.Models;
    using HealthHub.Web.ViewModels;

    public class ServicesService : IServicesService
    {
        private readonly IDeletableEntityRepository<Service> servicesRepository;

        public ServicesService(IDeletableEntityRepository<Service> servicesRepository)
        {
            this.servicesRepository = servicesRepository;
        }

        public IEnumerable<ServicesViewModel> GetAllServices()
        {
            return this.servicesRepository.All()
                .Select(p => new ServicesViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                }).ToList()
                .OrderBy(x => x.Id);
        }
    }
}
