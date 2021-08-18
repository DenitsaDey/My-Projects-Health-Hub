using HealthHub.Services.Data;
using HealthHub.Web.ViewModels.Doctor;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthHub.Web.Infrastructure.ViewComponents
{
    public class DeletedDoctorsViewComponent : ViewComponent
    {
        private readonly IDoctorsService doctorsService;

        public DeletedDoctorsViewComponent(IDoctorsService doctorsService)
        {
            this.doctorsService = doctorsService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewModel = this.doctorsService.GetDeleted<DoctorsViewModel>();

            return this.View(viewModel);
        }
    }
}
