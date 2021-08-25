namespace HealthHub.Web.Infrastructure.ViewComponents
{
    using System.Threading.Tasks;

    using HealthHub.Services.Data.Clinics;
    using HealthHub.Web.ViewModels.Clinics;
    using Microsoft.AspNetCore.Mvc;

    public class DeletedClinicsViewComponent : ViewComponent
    {
        private readonly IClinicsService clinicsService;

        public DeletedClinicsViewComponent(IClinicsService clinicsService)
        {
            this.clinicsService = clinicsService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewModel = this.clinicsService.GetDeleted<ClinicSimpleViewModel>();

            return this.View(viewModel);
        }
    }
}
