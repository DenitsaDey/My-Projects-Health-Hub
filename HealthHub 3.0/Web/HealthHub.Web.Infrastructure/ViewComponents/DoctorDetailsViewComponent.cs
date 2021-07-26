namespace HealthHub.Web.Infrastructure.ViewComponents
{
    using System.Threading.Tasks;

    using HealthHub.Services.Data;
    using Microsoft.AspNetCore.Mvc;

    public class DoctorDetailsViewComponent : ViewComponent
    {
        private readonly IDoctorsService doctorsService;

        public DoctorDetailsViewComponent(IDoctorsService doctorsService)
        {
            this.doctorsService = doctorsService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string doctorId)
        {
            var viewModel = await this.doctorsService.GetByIdAsync(doctorId);

            return this.View(viewModel);
        }
    }
}
