namespace HealthHub.Web.Infrastructure.ViewComponents
{
    using System.Threading.Tasks;
    using HealthHub.Services.Data.Clinics;
    using HealthHub.Web.ViewModels.Clinics;
    using Microsoft.AspNetCore.Mvc;

    public class ClinicDetailsViewComponent : ViewComponent
    {
        private readonly IClinicsService clinicsService;

        public ClinicDetailsViewComponent(IClinicsService clinicsService)
        {
            this.clinicsService = clinicsService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string clinicId)
        {
            var viewModel = this.clinicsService.GetById<ClinicViewModel>(clinicId);

            return this.View(viewModel);
        }
    }
}
