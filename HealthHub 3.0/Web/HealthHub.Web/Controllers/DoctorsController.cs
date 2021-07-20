namespace HealthHub.Web.Controllers
{
    using HealthHub.Services.Data;
    using Microsoft.AspNetCore.Mvc;

    public class DoctorsController : BaseController
    {
        private readonly IDoctorsService doctorsService;

        public DoctorsController(IDoctorsService doctorsService)
        {
            this.doctorsService = doctorsService;
        }

        public IActionResult All()
        {
            var viewModel = this.doctorsService.GetAll();
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
