namespace HealthHub.Web.Controllers
{
    using System.Diagnostics;

    using HealthHub.Services.Data;
    using HealthHub.Web.ViewModels;
    using HealthHub.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IGetCountsService getCountsService;
        private readonly ISpecialtiesService specialtiesService;
        private readonly ICityAreasService cityAreasService;

        public HomeController(
            IGetCountsService getCountsService,
            ISpecialtiesService specialtiesService,
            ICityAreasService cityAreasService)
        {
          this.getCountsService = getCountsService;
          this.specialtiesService = specialtiesService;
          this.cityAreasService = cityAreasService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel();
            viewModel.DataCounts = this.getCountsService.GetCounts();
            viewModel.CityAreas = this.cityAreasService.GetAllCityAreas();
            viewModel.Specialties = this.specialtiesService.GetAllSpecialties();

            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
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
