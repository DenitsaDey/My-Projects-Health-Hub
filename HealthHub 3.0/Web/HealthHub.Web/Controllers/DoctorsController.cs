namespace HealthHub.Web.Controllers
{
    using System.Threading.Tasks;

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
        public async Task<IActionResult> All(
            string specialtyId,
            string cityAreaId,
            string searchName,
            //string insuranceId,
            //SearchSorting sorting = SearchSorting.DateCreated,
            //Gender gender = Gender.Female,
            int pageNumber = 1)
        {
            if (pageNumber <= 0)
            {
                return this.NotFound();
            }

            if (!string.IsNullOrEmpty(specialtyId))
            {
                var specialty = await this.specialtiesService.GetByIdAsync(specialtyId);

                if (specialty == null)
                {
                    return new StatusCodeResult(404);
                }

                this.ViewData["SpecialtyName"] = specialty.Name;
            }

            this.ViewData["CurentSort"] = specialtyId;

            if (!string.IsNullOrEmpty(cityAreaId))
            {
                var cityArea = await this.cityAreasService.GetByIdAsync(cityAreaId);

                if (cityArea == null)
                {
                    return new StatusCodeResult(404);
                }

                this.ViewData["CityAreaName"] = cityArea.Name;
            }

            this.ViewData["CurentSort"] = cityAreaId;

            const int ItemsPerPage = 8;

            var viewModel = await this.doctorsService.GetAllSearchedAsync(specialtyId, cityAreaId, searchName, pageNumber);  /*sorting, gender, insuranceId*/

            viewModel.Specialties = await this.specialtiesService.GetAllSpecialtiesAsync();
            viewModel.CityAreas = await this.cityAreasService.GetAllCityAreasAsync();
            viewModel.Paging = new ViewModels.PagingViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = pageNumber,
                DataCount = this.getCountsService.GetCounts().DoctorsCount,
            };
            viewModel.InsuranceCompanies = this.insuranceService.GetAllInsuranceCompanies();

            return this.View(viewModel);
        }

        public async Task<IActionResult> Details(string doctorId)
        {
            var model = await this.doctorsService.GetByIdAsync(doctorId);

            if (model == null)
            {
                return new StatusCodeResult(404);
            }

            return this.View(model);
        }
    }
}
