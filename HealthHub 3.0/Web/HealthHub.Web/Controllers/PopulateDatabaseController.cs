namespace HealthHub.Web.Controllers
{
    using HealthHub.Services;
    using HealthHub.Services.Data.Clinics;
    using Microsoft.AspNetCore.Mvc;

    public class PopulateDatabaseController : BaseController
    {
        private readonly ICityAreasScraperService cityAreasScraperService;
        private readonly IInsuranceScraperService insuranceScraperService;

        public PopulateDatabaseController(
            ICityAreasScraperService cityAreasScraperService,
            IInsuranceScraperService insuranceScraperService,
            IClinicsService clinicsService)
        {
            this.cityAreasScraperService = cityAreasScraperService;
            this.insuranceScraperService = insuranceScraperService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        // public async Task<IActionResult> Add()
        // {
        //    // await this.cityAreasScraperService.ImportCityAreas();
        //    // await this.insuranceScraperService.ImportInsuranceCompanies();
        //    return this.View();
        // }
    }
}
