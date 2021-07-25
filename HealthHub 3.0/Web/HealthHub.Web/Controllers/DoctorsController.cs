namespace HealthHub.Web.Controllers
{
    using HealthHub.Data.Models.Enums;
    using HealthHub.Services.Data;
    using Microsoft.AspNetCore.Mvc;

    public class DoctorsController : BaseController
    {
        private readonly IDoctorsService doctorsService;
        private readonly ISpecialtiesService specialtiesService;
        private readonly ICityAreasService cityAreasService;
        private readonly IGetCountsService getCountsService;
        private readonly IInsuranceService insuranceService;

        public DoctorsController(
            IDoctorsService doctorsService,
            ISpecialtiesService specialtiesService,
            ICityAreasService cityAreasService,
            IGetCountsService getCountsService,
            IInsuranceService insuranceService)
        {
            this.doctorsService = doctorsService;
            this.specialtiesService = specialtiesService;
            this.cityAreasService = cityAreasService;
            this.getCountsService = getCountsService;
            this.insuranceService = insuranceService;
        }

        //[Route("Doctors/All/{specialtyId}&{cityAreaId}&{name}&{pageNumber}")]
        public IActionResult All(
            string specialtyId,
            string cityAreaId,
            string name,
            string insuranceId,
            SearchSorting sorting = SearchSorting.DateCreated,
            Gender gender = Gender.Female,
            int pageNumber = 1)
        {
            if (pageNumber <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 8;

            var viewModel = this.doctorsService.GetAll(specialtyId, cityAreaId, name, pageNumber, sorting, gender, insuranceId);

            viewModel.Specialties = this.specialtiesService.GetAllSpecialties();
            viewModel.CityAreas = this.cityAreasService.GetAllCityAreas();
            viewModel.Paging = new ViewModels.PagingViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = pageNumber,
                DataCount = this.getCountsService.GetCounts().DoctorsCount,
            };
            viewModel.InsuranceCompanies = this.insuranceService.GetAllInsuranceCompanies();

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
