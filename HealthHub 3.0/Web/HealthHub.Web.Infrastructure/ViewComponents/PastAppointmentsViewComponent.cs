namespace HealthHub.Web.Infrastructure.ViewComponents
{
    using System.Linq;
    using System.Threading.Tasks;

    using HealthHub.Data.Models;
    using HealthHub.Data.Models.Enums;
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

            var viewModel = new AppointmentListViewModel();

            var appointmentList = await this.appointmentsService.GetPastByPatientAsync<AppointmentViewModel>(patientId);
           

            // in the cases when the appointment has not been confirmed or cancelled by the Doctor in the due time and the appointment has passed
            if (appointmentList.Any(a => a.AppointmentStatus == AppointmentStatus.Requested))
            {
                foreach (var appointment in appointmentList)
                {
                    await this.appointmentsService.ChangeAppointmentStatusAsync(appointment.Id, "Cancelled");
                }
            }

            // in case of confirmed appointment that has passed we assume it has been completed and it automatically changes its status to "Completed"
            // However here the doctor has the option to change the status to "NoShow" if the patient did not show up, to prevent the option of rating the appointment
            if (appointmentList.Any(a => a.AppointmentStatus == AppointmentStatus.Confirmed))
            {
                foreach (var appointment in appointmentList)
                {
                    await this.appointmentsService.ChangeAppointmentStatusAsync(appointment.Id, "Completed");
                }
            }

            viewModel.AppointmentList = appointmentList;

            return this.View(viewModel);
        }
    }
}
