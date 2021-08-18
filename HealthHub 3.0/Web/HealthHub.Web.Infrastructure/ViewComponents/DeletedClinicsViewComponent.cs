namespace HealthHub.Web.Infrastructure.ViewComponents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HealthHub.Services.Data.Clinics;
    using HealthHub.Web.ViewModels.Clinics;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class DeletedClinicsViewComponent : ViewComponent
    {
        private readonly IClinicsService clinicsService;

        public DeletedClinicsViewComponent(IClinicsService clinicsService)
        {
            this.clinicsService = clinicsService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewModel = this.clinicsService.GetDeleted();

            return this.View(viewModel);
        }
    }
}
