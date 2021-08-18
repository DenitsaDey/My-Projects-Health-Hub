namespace HealthHub.Web.Infrastructure.ViewComponents
{
    using System.Linq;
    using System.Threading.Tasks;

    using HealthHub.Data.Common.Repositories;
    using HealthHub.Data.Models;
    using HealthHub.Data.Models.Enums;
    using HealthHub.Services.Data;
    using HealthHub.Web.ViewModels.Appointment;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class DoctorPastAppointmentsViewComponent : ViewComponent
    {
        private readonly IAppointmentsService appointmentsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDeletableEntityRepository<Doctor> doctorsRepository;

        public DoctorPastAppointmentsViewComponent(
            IAppointmentsService appointmentsService,
            UserManager<ApplicationUser> userManager,
            IDeletableEntityRepository<Doctor> doctorsRepository)
        {
            this.appointmentsService = appointmentsService;
            this.userManager = userManager;
            this.doctorsRepository = doctorsRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // var user = await this.userManager.GetUserAsync(this.HttpContext.User);
            // var userId = await this.userManager.GetUserIdAsync(user);
            // for demo purposes the doctorId will be asigned manually for the doctor who happened to have the most seeded appointments
            var doctorId = this.doctorsRepository.All()
                .OrderByDescending(d => d.ScheduledAppointments.Count)
                .FirstOrDefault()
                .Id;

            var viewModel = new DoctorAppointmentListViewModel();

            var appointmentList = await this.appointmentsService.GetPastByDoctorAsync<DoctorAppointmentViewModel>(doctorId);

            // in the cases when the appointment has not been confirmed or cancelled by the Doctor in the due time and the appointment has passed
            if (appointmentList.Any(a => a.AppointmentStatus == AppointmentStatus.Requested))
            {
                foreach (var appointment in appointmentList.Where(a => a.AppointmentStatus == AppointmentStatus.Requested))
                {
                    await this.appointmentsService.ChangeAppointmentStatusAsync(appointment.Id, "Cancelled");
                }
            }

            // in case of confirmed appointment that has passed we assume it has been completed and it automatically changes its status to "Completed"
            // However here the doctor has the option to change the status to "NoShow" if the patient did not show up, to prevent the option of rating the appointment
            if (appointmentList.Any(a => a.AppointmentStatus == AppointmentStatus.Confirmed))
            {
                foreach (var appointment in appointmentList.Where(a => a.AppointmentStatus == AppointmentStatus.Confirmed))
                {
                    await this.appointmentsService.ChangeAppointmentStatusAsync(appointment.Id, "Completed");
                }
            }

            viewModel.AppointmentList = appointmentList;
            return this.View(viewModel);
        }
    }
}
