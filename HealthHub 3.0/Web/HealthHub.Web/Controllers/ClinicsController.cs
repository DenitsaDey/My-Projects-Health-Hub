namespace HealthHub.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using HealthHub.Services.Data;
    using HealthHub.Services.Data.Clinics;
    using HealthHub.Web.ViewModels;
    using HealthHub.Web.ViewModels.Clinics;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class ClinicsController : BaseController
    {
        private readonly IClinicsService clinicsService;
        private readonly ICityAreasService cityAreasService;
        private readonly IInsuranceService insurancesService;
        private readonly ISpecialtiesService specialtiesService;
        private readonly IDoctorsService doctorsService;

        public ClinicsController(
            IClinicsService clinicsService,
            ICityAreasService cityAreasService,
            IInsuranceService insurancesService,
            ISpecialtiesService specialtiesService,
            IDoctorsService doctorsService)
        {
            this.clinicsService = clinicsService;
            this.cityAreasService = cityAreasService;
            this.insurancesService = insurancesService;
            this.specialtiesService = specialtiesService;
            this.doctorsService = doctorsService;
        }

        public async Task<IActionResult> Index(
        [FromQuery] ClinicFilterViewModel query,
        int pageId = 1)
        {
            if (pageId <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 8;

            var viewModel = await this.clinicsService.GetAllSearchedAsync(
                query.SpecialtyId,
                query.CityAreaId,
                query.InsuranceId,
                query.Sorting,
                pageId,
                ItemsPerPage);
            viewModel.CityAreas = await this.cityAreasService.GetAllCityAreasAsync<CityAreasViewModel>();
            viewModel.InsuranceCompanies = this.insurancesService.GetAllInsuranceCompanies<InsuranceViewModel>();
            viewModel.Specialties = await this.specialtiesService.GetAllSpecialtiesAsync<SpecialtyViewModel>();

            this.ViewData["CityAreas"] = new SelectList(viewModel.CityAreas, "Id", "Name");
            this.ViewData["InsuranceCompanies"] = new SelectList(viewModel.InsuranceCompanies, "Id", "Name");
            this.ViewData["Specialties"] = new SelectList(viewModel.Specialties, "Id", "Name");

            viewModel.Paging = new PagingViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = pageId,
                DataCount = viewModel.FilteredClinics.Count(),
            };

            query.Clinics = viewModel.Clinics;
            query.CityAreas = viewModel.CityAreas;
            query.Specialties = viewModel.Specialties;
            query.InsuranceCompanies = viewModel.InsuranceCompanies;
            query.FilteredClinics = viewModel.FilteredClinics;
            query.Paging = viewModel.Paging;

            return this.View(query);
        }

        public IActionResult Details(string clinicId)
        {
            var viewModel = this.clinicsService.GetById(clinicId);

            if (viewModel == null)
            {
                return new StatusCodeResult(404);
            }

            viewModel.Clinics = this.clinicsService.GetAll();
            return this.View(viewModel);
        }
    }
}
