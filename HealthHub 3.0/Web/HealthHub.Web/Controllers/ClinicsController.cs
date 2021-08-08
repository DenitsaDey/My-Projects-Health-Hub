namespace HealthHub.Web.Controllers
{
    using System.Linq;

    using HealthHub.Services.Data;
    using HealthHub.Services.Data.Clinics;
    using HealthHub.Web.ViewModels;
    using HealthHub.Web.ViewModels.Clinics;
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

        public IActionResult Index(int pageId = 1)
        {
            const int ItemsPerPage = 8;

            var viewModel = new ClinicHeaderViewModel();
            viewModel.Clinics = this.clinicsService.GetAllClinics();
            viewModel.Paging = new PagingViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = pageId,
                DataCount = viewModel.Clinics.Count(),
            };
            return this.View(viewModel);
        }

        public IActionResult Details(string clinicId)
        {
            var viewModel = this.clinicsService.GetById(clinicId);

            if (viewModel == null)
            {
                return new StatusCodeResult(404);
            }

            viewModel.Clinics = this.clinicsService.GetAllClinics();
            return this.View(viewModel);
        }
    }
}
