namespace HealthHub.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using HealthHub.Services.Data;
    using HealthHub.Web.ViewModels;
    using HealthHub.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IGetCountsService getCountsService;
        private readonly ISpecialtiesService specialtiesService;
        private readonly ICityAreasService cityAreasService;
        private readonly IDoctorsService doctorsService;

        public HomeController(
            IGetCountsService getCountsService,
            ISpecialtiesService specialtiesService,
            ICityAreasService cityAreasService,
            IDoctorsService doctorsService)
        {
          this.getCountsService = getCountsService;
          this.specialtiesService = specialtiesService;
          this.cityAreasService = cityAreasService;
          this.doctorsService = doctorsService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new HomeHeaderViewModel();
            viewModel.DataCounts = this.getCountsService.GetCounts();
            viewModel.CityAreas = await this.cityAreasService.GetAllCityAreasAsync();
            viewModel.Specialties = await this.specialtiesService.GetAllSpecialtiesAsync();
            viewModel.Doctors = this.doctorsService.GetAll();

            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [Route("/Home/Error/404")]
        public IActionResult Error404()
        {
            return this.View();
        }

        [Route("/Home/Error/{code:int}")]
        public IActionResult Error(int code)
        {
            // Could handle different codes here
            // or just return the default error view
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
