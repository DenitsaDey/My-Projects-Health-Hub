namespace HealthHub.Web.Controllers
{
    using System.Threading.Tasks;

    using HealthHub.Services.Data;
    using HealthHub.Services.Data.Clinics;
    using HealthHub.Web.ViewModels;
    using HealthHub.Web.ViewModels.Doctor;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class DoctorsController : BaseController
    {
        private readonly IDoctorsService doctorsService;
        private readonly ISpecialtiesService specialtiesService;
        private readonly ICityAreasService cityAreasService;
        private readonly IInsuranceService insuranceService;
        private readonly IClinicsService clinicsService;

        public DoctorsController(
            IDoctorsService doctorsService,
            ISpecialtiesService specialtiesService,
            ICityAreasService cityAreasService,
            IInsuranceService insuranceService,
            IClinicsService clinicsService)
        {
            this.doctorsService = doctorsService;
            this.specialtiesService = specialtiesService;
            this.cityAreasService = cityAreasService;
            this.insuranceService = insuranceService;
            this.clinicsService = clinicsService;
        }

        public async Task<IActionResult> All(
            string clinicId,
            [FromQuery] DoctorsFilterViewModel query,
            string searchName,
            int pageId = 1)
        {
            if (pageId <= 0)
            {
                return this.NotFound();
            }

            if (!string.IsNullOrEmpty(clinicId))
            {
                query.ClinicId = clinicId;
            }

            if (!string.IsNullOrEmpty(searchName))
            {
                query.SearchName = searchName;
            }

            const int ItemsPerPage = 8;

            var viewModel = await this.doctorsService.GetAllSearchedAsync(
                query.SearchName,
                query.ClinicId,
                query.SpecialtyId,
                query.CityAreaId,
                query.InsuranceId,
                query.WorksWithChildren,
                query.OnlineConsultation,
                query.Gender,
                query.Sorting,
                pageId,
                ItemsPerPage);

            if (viewModel == null)
            {
                return this.RedirectToAction("Error404", "Home");
            }

            viewModel.Clinics = this.clinicsService.GetAll();
            viewModel.CityAreas = await this.cityAreasService.GetAllCityAreasAsync<CityAreasViewModel>();
            viewModel.InsuranceCompanies = this.insuranceService.GetAllInsuranceCompanies<InsuranceViewModel>();
            viewModel.Specialties = await this.specialtiesService.GetAllSpecialtiesAsync<SpecialtyViewModel>();

            this.ViewData["CityAreas"] = new SelectList(viewModel.CityAreas, "Id", "Name");
            this.ViewData["InsuranceCompanies"] = new SelectList(viewModel.InsuranceCompanies, "Id", "Name");
            this.ViewData["Specialties"] = new SelectList(viewModel.Specialties, "Id", "Name");

            viewModel.Paging = new PagingViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = pageId,
                DataCount = viewModel.DoctorsCount,
            };

            query.Doctors = viewModel.Doctors;
            query.CityAreas = viewModel.CityAreas;
            query.Specialties = viewModel.Specialties;
            query.InsuranceCompanies = viewModel.InsuranceCompanies;
            query.Clinics = viewModel.Clinics;
            query.Paging = viewModel.Paging;

            return this.View(query);
        }

        public async Task<IActionResult> Details(string doctorId)
        {
            if (doctorId == null)
            {
                return this.NotFound();
            }

            var model = await this.doctorsService.GetByIdAsync<DoctorsViewModel>(doctorId);

            if (model == null)
            {
                return this.RedirectToAction("Error404", "Home");
            }

            model.Clinics = this.clinicsService.GetAll();
            return this.View(model);
        }
    }
}
