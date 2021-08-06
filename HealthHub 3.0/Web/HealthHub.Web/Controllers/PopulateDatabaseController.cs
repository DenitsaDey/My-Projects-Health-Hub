namespace HealthHub.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using HealthHub.Common;
    using HealthHub.Data.Models;
    using HealthHub.Services;
    using HealthHub.Services.Data.Clinics;
    using HealthHub.Web.ViewModels;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class PopulateDatabaseController : BaseController
    {
        private readonly ICityAreasScraperService cityAreasScraperService;
        private readonly IInsuranceScraperService insuranceScraperService;
        private readonly IClinicsService clinicsService;
        private readonly IRatingPopulatingService ratingPopulatingService;
        private readonly UserManager<ApplicationUser> userManager;

        public PopulateDatabaseController(
            ICityAreasScraperService cityAreasScraperService,
            IInsuranceScraperService insuranceScraperService,
            IClinicsService clinicsService,
            IRatingPopulatingService ratingPopulatingService,
            UserManager<ApplicationUser> userManager)
        {
            this.cityAreasScraperService = cityAreasScraperService;
            this.insuranceScraperService = insuranceScraperService;
            this.clinicsService = clinicsService;
            this.ratingPopulatingService = ratingPopulatingService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            var viewModel = new HeaderSearchQueryModel();
            viewModel.Clinics = this.clinicsService.GetAllClinics();
            return this.View(viewModel);
        }

        public async Task<IActionResult> Add()
        {
            // Get Patient Id
            var patientId = this.userManager.Users.Where(x => x.Email == GlobalConstants.AccountsSeeding.PatientEmail).FirstOrDefault().Id;
            await this.ratingPopulatingService.ImportRatings(patientId);

            // await this.cityAreasScraperService.ImportCityAreas();
            // await this.insuranceScraperService.ImportInsuranceCompanies();
            return this.RedirectToAction("Index", "Home");
        }

    }
}
