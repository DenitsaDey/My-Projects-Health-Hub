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

        public IActionResult All(string specialtyId, string cityAreaId, string name)
        {
            var viewModel = this.doctorsService.GetAll(specialtyId, cityAreaId, name);
            viewModel.Specialties = this.specialtiesService.GetAllSpecialties();
            viewModel.CityAreas = this.cityAreasService.GetAllCityAreas();
            return this.View(viewModel);
        }

        public IActionResult Searched(string specialtyId, string areaId, string name)
        {
            var viewModel = this.doctorsService.GetAllSearched(specialtyId, areaId, name);
            return this.View(viewModel);
        }

        public IActionResult Details(string doctorId)
        {
            var model = this.doctorsService.GetById(doctorId);
            return this.View(model);
        }
    }
}
