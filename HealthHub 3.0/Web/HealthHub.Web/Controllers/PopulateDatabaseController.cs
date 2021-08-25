namespace HealthHub.Web.Controllers
{
    using System.Threading.Tasks;

    using HealthHub.Services;
    using HealthHub.Services.Data;
    using HealthHub.Services.Data.Clinics;
    using HealthHub.Web.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class PopulateDatabaseController : BaseController
    {
        private readonly ICityAreasScraperService cityAreasScraperService;
        private readonly IInsuranceScraperService insuranceScraperService;
        private readonly IClinicsService clinicsService;
        private readonly IRatingPopulatingService ratingPopulatingService;

        public PopulateDatabaseController(
            ICityAreasScraperService cityAreasScraperService,
            IInsuranceScraperService insuranceScraperService,
            IClinicsService clinicsService,
            IRatingPopulatingService ratingPopulatingService)
        {
            this.cityAreasScraperService = cityAreasScraperService;
            this.insuranceScraperService = insuranceScraperService;
            this.clinicsService = clinicsService;
            this.ratingPopulatingService = ratingPopulatingService;
        }

        public async Task<IActionResult> Index()
        {
            await this.ratingPopulatingService.ImportRatings();
            return this.View();
        }

        public async Task<IActionResult> Add()
        {
            // initially CityAreas and Insurances were scraped, later were just seeded
            // await this.cityAreasScraperService.ImportCityAreas();
            // await this.insuranceScraperService.ImportInsuranceCompanies();
            return this.RedirectToAction("Index", "Home");
        }
    }
}
