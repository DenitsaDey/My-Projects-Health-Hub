namespace HealthHub.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using HealthHub.Services;
    using HealthHub.Services.Data.Clinics;
    using HealthHub.Web.Controllers;
    using Microsoft.AspNetCore.Mvc;

    public class GatherInsuranceCompaniesController : BaseController
    {
        private readonly IInsuranceScraperService insuranceScraperService;

        public GatherInsuranceCompaniesController(IClinicsService clinicsService, IInsuranceScraperService insuranceScraperService)
        {
            this.insuranceScraperService = insuranceScraperService;
        }

        public IActionResult Index()
        {
            return this.Redirect("/");
        }

        public async Task<IActionResult> Add()
        {
            await this.insuranceScraperService.ImportInsuranceCompanies();

            return this.Redirect("/");
        }
    }
}
