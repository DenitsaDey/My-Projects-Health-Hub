namespace HealthHub.Web.Controllers
{
    using HealthHub.Services.Data;
    using Microsoft.AspNetCore.Mvc;

    public class DoctorsController : BaseController
    {
        private readonly IDoctorsService doctorsService;
        private readonly ISpecialtiesService specialtiesService;
        private readonly ICityAreasService cityAreasService;

        public DoctorsController(IDoctorsService doctorsService, ISpecialtiesService specialtiesService, ICityAreasService cityAreasService)
        {
            this.doctorsService = doctorsService;
            this.specialtiesService = specialtiesService;
            this.cityAreasService = cityAreasService;
        }

        public IActionResult All()
        {
            var viewModel = this.doctorsService.GetAll();
            viewModel.Specialties = this.specialtiesService.GetAllSpecialties();
            viewModel.CityAreas = this.cityAreasService.GetAllCityAreas();
            return this.View(viewModel);
        }

        public IActionResult Searched(string specialty, string area, string name)
        {
            var viewModel = this.doctorsService.GetAllSearched(specialty, area, name);
            return this.View(viewModel);
        }

        public IActionResult Details(string doctorId)
        {
            var model = this.doctorsService.GetById(doctorId);
            return this.View(model);
        }
    }
}
