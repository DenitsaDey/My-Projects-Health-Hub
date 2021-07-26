namespace HealthHub.Web.Infrastructure.ViewComponents
{
    using System.Threading.Tasks;

    using HealthHub.Data.Models;

    using HealthHub.Services.Data;
    using HealthHub.Web.ViewModels.Appointment;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class PastAppointmentsViewComponent : ViewComponent
    {
        private readonly IAppointmentsService appointmentsService;
        private readonly UserManager<ApplicationUser> userManager;

        public PastAppointmentsViewComponent(
            IAppointmentsService appointmentsService,
            UserManager<ApplicationUser> userManager)
        {
            this.appointmentsService = appointmentsService;
            this.userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var patient = await this.userManager.GetUserAsync(this.HttpContext.User);
            var patientId = await this.userManager.GetUserIdAsync(patient);

            var viewModel = new AppointmentListViewModel
            {
                AppointmentList = await this.appointmentsService.GetPastByPatientAsync(patientId),
            };

            return this.View(viewModel);
        }
    }
}
