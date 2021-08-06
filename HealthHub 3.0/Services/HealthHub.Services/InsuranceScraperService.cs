namespace HealthHub.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using AngleSharp;
    using HealthHub.Data.Common.Repositories;
    using HealthHub.Data.Models;

    public class InsuranceScraperService : IInsuranceScraperService
    {
        private readonly IBrowsingContext context;
        private readonly IDeletableEntityRepository<Insurance> insuranceRepository;

        public InsuranceScraperService(IDeletableEntityRepository<Insurance> insuranceRepository)
        {
            this.insuranceRepository = insuranceRepository;

            var config = Configuration.Default.WithDefaultLoader();
            this.context = BrowsingContext.New(config);
        }

        public async Task ImportInsuranceCompanies()
        {
            var document = this.context.OpenAsync("https://dohaclinichospital.com/content/Insurance-Companies-2")
                .GetAwaiter()
                .GetResult();

            var insuranceCompanies = document.QuerySelectorAll(".content-sub > p")
                .Select(x => x.TextContent)
                .ToList();

            foreach (var company in insuranceCompanies.Skip(3))
            {
                var newInsuranceCompany = new Insurance { Name = company.Trim() };

                await this.insuranceRepository.AddAsync(newInsuranceCompany);
                await this.insuranceRepository.SaveChangesAsync();
            }
        }
    }
}
