namespace HealthHub.Web.Areas.Doctor.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using HealthHub.Data.Common.Repositories;
    using HealthHub.Data.Models;
    using HealthHub.Services;
    using HealthHub.Services.Data;
    using HealthHub.Services.Data.Clinics;
    using HealthHub.Services.Messaging;
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
        private readonly IDoctorsService doctorsService;
        private readonly IEmailSender emailSender;
        private readonly IViewRenderService viewRenderService;

        public AppointmentsController(
            UserManager<ApplicationUser> userManager,
            IDateTimeParserService dateTimeParserService,
            IServicesService servicesService,
            IAppointmentsService appointmentsService,
            IClinicsService clinicsService,
            IDoctorsService doctorsService,
            IEmailSender emailSender,
            IViewRenderService viewRenderService)
        {
            this.userManager = userManager;
            this.dateTimeParserService = dateTimeParserService;
            this.servicesService = servicesService;
            this.appointmentsService = appointmentsService;
            this.clinicsService = clinicsService;
            this.doctorsService = doctorsService;
            this.emailSender = emailSender;
            this.viewRenderService = viewRenderService;
        }

        public IActionResult Index()
        {
            // var doctor = await this.userManager.GetUserAsync(this.HttpContext.User);
            // var doctorId = await this.userManager.GetUserIdAsync(doctor);
            // for demo purposes the doctorId will be asigned manually for the doctor who happened to have the most seeded appointments
            var doctorId = this.doctorsService.GetIdByMostAppointments();

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
            await this.SendEmail(appointmentId);

            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpPost]
        public async Task<IActionResult> Cancel(string appointmentId)
        {
            await this.appointmentsService.ChangeAppointmentStatusAsync(appointmentId, "Cancelled");
            await this.SendEmail(appointmentId);

            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStatus(string appointmentId)
        {
            await this.appointmentsService.ChangeAppointmentStatusAsync(appointmentId, "Completed");
            await this.SendEmail(appointmentId);

            return this.RedirectToAction(nameof(this.Index));
        }

        private async Task SendEmail(string appointmentId)
        {
            // automatically sending email with appointment status after patient has cancelled an appointment
            var patient = await this.userManager.GetUserAsync(this.HttpContext.User);
            var patientEmail = await this.userManager.GetEmailAsync(patient);
            var htmlModel = await this.appointmentsService.GetByIdAsync<AppointmentViewModel>(appointmentId);
            htmlModel.Clinics = this.clinicsService.GetAllClinics(); // as the partial Header View requires a list of clinics for the dropdown
            var htmlContent = await this.viewRenderService.RenderToStringAsync("~/Views/Appointment/Details.cshtml", htmlModel);

            await this.emailSender.SendEmailAsync("healthhub@healthhub.com", "Health Hub", patientEmail, "Appointment Status Changed", htmlContent);
        }
    }
}
