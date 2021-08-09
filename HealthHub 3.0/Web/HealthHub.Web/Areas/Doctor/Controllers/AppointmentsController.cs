namespace HealthHub.Web.Areas.Doctor.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using HealthHub.Data.Common.Repositories;
    using HealthHub.Data.Models;
    using HealthHub.Services;
    using HealthHub.Services.Data;
    using HealthHub.Services.Data.Clinics;
    using HealthHub.Web.ViewModels.Appointment;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class AppointmentsController : DoctorBaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDateTimeParserService dateTimeParserService;
        private readonly IServicesService servicesService;
        private readonly IAppointmentsService appointmentsService;
        private readonly IClinicsService clinicsService;
        private readonly IDeletableEntityRepository<Doctor> doctorsRepository;

        public AppointmentsController(
            UserManager<ApplicationUser> userManager,
            IDateTimeParserService dateTimeParserService,
            IServicesService servicesService,
            IAppointmentsService appointmentsService,
            IClinicsService clinicsService,
            IDeletableEntityRepository<Doctor> doctorsRepository)
        {
            this.userManager = userManager;
            this.dateTimeParserService = dateTimeParserService;
            this.servicesService = servicesService;
            this.appointmentsService = appointmentsService;
            this.clinicsService = clinicsService;
            this.doctorsRepository = doctorsRepository;
        }

        public IActionResult Index()
        {
            // var doctor = await this.userManager.GetUserAsync(this.HttpContext.User);
            // var doctorId = await this.userManager.GetUserIdAsync(doctor);
            // for demo purposes the doctorId will be asigned manually for the doctor who happened to have the most seeded appointments
            var doctorId = this.doctorsRepository.All()
                .OrderByDescending(d => d.ScheduledAppointments.Count)
                .FirstOrDefault()
                .Id;

            var viewModel = this.appointmentsService.GetUpcomingByDoctor<DoctorAppointmentViewModel>(doctorId);

            return this.View(viewModel);
        }

        public async Task<IActionResult> Details(string appointmentId)
        {
            var viewModel = await this.appointmentsService.GetByIdAsync<DoctorAppointmentViewModel>(appointmentId);
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Confirm(string appointmentId)
        {
            await this.appointmentsService.ChangeAppointmentStatusAsync(appointmentId, "Confirmed");
            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpPost]
        public async Task<IActionResult> Cancel(string appointmentId)
        {
            await this.appointmentsService.ChangeAppointmentStatusAsync(appointmentId, "Cancelled");
            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStatus(string appointmentId)
        {
            await this.appointmentsService.ChangeAppointmentStatusAsync(appointmentId, "Completed");
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
