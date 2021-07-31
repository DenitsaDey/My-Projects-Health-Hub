namespace HealthHub.Web.Controllers
{
    using HealthHub.Services.Data;
    using HealthHub.Services.Data.Clinics;
    using Microsoft.AspNetCore.Mvc;

    public class ClinicsController : BaseController
    {
        private readonly IClinicsService clinicsService;
        private readonly IDoctorsService doctorsService;

        public ClinicsController(
            IClinicsService clinicsService,
            IDoctorsService doctorsService)
        {
            this.clinicsService = clinicsService;
            this.doctorsService = doctorsService;
        }

        public IActionResult Index()
        {
            var viewModel = this.clinicsService.GetHeader();
            return this.View(viewModel);
        }

        public IActionResult Details(string clinicId)
        {
            var viewModel = this.clinicsService.GetHeader();
            viewModel.Clinic = this.clinicsService.GetById(clinicId);

            if (viewModel.Clinic == null)
            {
                return new StatusCodeResult(404);
            }

            return this.View(viewModel);
        }
    }
}
