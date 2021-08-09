namespace HealthHub.Web.Infrastructure.ViewComponents
{
    using System.Linq;
    using System.Threading.Tasks;

    using HealthHub.Data.Common.Repositories;
    using HealthHub.Data.Models;
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

            var viewModel = await this.appointmentsService.GetPastByDoctorAsync<DoctorAppointmentViewModel>(doctorId);

            return this.View(viewModel);
        }
    }
}
