namespace HealthHub.Web.Infrastructure.ViewComponents
{
    using System.Threading.Tasks;

    using HealthHub.Services.Data;
    using HealthHub.Services.Data.Clinics;
    using HealthHub.Web.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class HeaderSearchViewComponent : ViewComponent
    {
        private readonly IClinicsService clinicsService;

        public HeaderSearchViewComponent(IClinicsService clinicsService)
        {
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewModel = new HeaderSearchQueryModel
            {
                Clinics = await this.clinicsService.GetAllClinicsAsync(),
            };

            return this.View(viewModel);
        }
    }
}
