namespace HealthHub.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using HealthHub.Data.Models.Enums;
    using HealthHub.Services.Data;
    using HealthHub.Services.Data.Clinics;
    using HealthHub.Web.ViewModels;
    using HealthHub.Web.ViewModels.Clinics;
    using HealthHub.Web.ViewModels.Doctor;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class DoctorsController : BaseController
    {
        private readonly IDoctorsService doctorsService;
        private readonly ISpecialtiesService specialtiesService;
        private readonly ICityAreasService cityAreasService;
        private readonly IGetCountsService getCountsService;
        private readonly IInsuranceService insuranceService;
        private readonly IClinicsService clinicsService;

        public DoctorsController(
            IDoctorsService doctorsService,
            ISpecialtiesService specialtiesService,
            ICityAreasService cityAreasService,
            IGetCountsService getCountsService,
            IInsuranceService insuranceService,
            IClinicsService clinicsService)
        {
            this.doctorsService = doctorsService;
            this.specialtiesService = specialtiesService;
            this.cityAreasService = cityAreasService;
            this.getCountsService = getCountsService;
            this.insuranceService = insuranceService;
            this.clinicsService = clinicsService;
        }

        // [Route("Doctors/All/{specialtyId}&{cityAreaId}&{name}&{pageNumber}")]
        public async Task<IActionResult> All(
            [FromQuery] DoctorsFilterViewModel query,
            string searchName,
            int pageId = 1)
        {
            if (pageId <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 8;

            var viewModel = await this.doctorsService.GetAllSearchedAsync(
                query.SpecialtyId,
                query.CityAreaId,
                query.InsuranceId,
                query.WorksWithChildren,
                query.OnlineConsultation,
                query.Gender,
                query.Sorting,
                query.ClinicId,
                searchName,
                pageId,
                ItemsPerPage);

            viewModel.Clinics = this.clinicsService.GetAllClinics();
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
            var model = await this.doctorsService.GetByIdAsync<DoctorsViewModel>(doctorId);

            if (model == null)
            {
                return new StatusCodeResult(404);
            }

            model.Clinics = this.clinicsService.GetAllClinics();
            return this.View(model);
        }
    }
}
