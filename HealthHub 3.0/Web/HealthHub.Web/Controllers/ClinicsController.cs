using HealthHub.Services.Data.Clinics;
using Microsoft.AspNetCore.Mvc;

namespace HealthHub.Web.Controllers
{
    public class ClinicsController : BaseController
    {
        private readonly IClinicsService clinicsService;

        public ClinicsController(IClinicsService clinicsService)
        {
            this.clinicsService = clinicsService;
        }

        public IActionResult Index()
        {
            var viewModel = this.clinicsService.GetAllClinicsAsync();
            return this.View(viewModel);
        }

        public IActionResult Details(string clinicId)
        {
            var viewModel = this.clinicsService.GetByIdAsync(clinicId);

            if (viewModel == null)
            {
                return new StatusCodeResult(404);
            }

            return this.View(viewModel);
        }
    }
}
