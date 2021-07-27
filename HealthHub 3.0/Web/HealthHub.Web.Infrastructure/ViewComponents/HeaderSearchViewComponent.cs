namespace HealthHub.Web.Infrastructure.ViewComponents
{
    using System.Threading.Tasks;

    using HealthHub.Services.Data;
    using HealthHub.Web.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class HeaderSearchViewComponent : ViewComponent
    {
        private readonly ICityAreasService cityAreasService;
        private readonly ISpecialtiesService specialtiesService;

        public HeaderSearchViewComponent(ICityAreasService cityAreasService, ISpecialtiesService specialtiesService)
        {
            this.cityAreasService = cityAreasService;
            this.specialtiesService = specialtiesService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewModel = new HeaderSearchQueryModel
            {
                Specialties = await this.specialtiesService.GetAllSpecialtiesAsync(),
                CityAreas = await this.cityAreasService.GetAllCityAreasAsync(),
            };

            return this.View(viewModel);
        }
    }
}
