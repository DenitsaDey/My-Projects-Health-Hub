namespace HealthHub.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using HealthHub.Data.Models;
    using HealthHub.Services;
    using HealthHub.Services.Data;
    using HealthHub.Services.Data.Clinics;
    using HealthHub.Web.ViewModels.Appointment;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class AppointmentController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDateTimeParserService dateTimeParserService;
        private readonly IServicesService servicesService;
        private readonly IAppointmentsService appointmentService;
        private readonly IClinicsService clinicsService;

        public AppointmentController(
            UserManager<ApplicationUser> userManger,
            IDateTimeParserService dateTimeParserService,
            IServicesService servicesService,
            IAppointmentsService appointmentService,
            IClinicsService clinicsService)
        {
            this.userManager = userManger;
            this.dateTimeParserService = dateTimeParserService;
            this.servicesService = servicesService;
            this.appointmentService = appointmentService;
            this.clinicsService = clinicsService;
        }

        public async Task<IActionResult> Index()
        {
            var patient = await this.userManager.GetUserAsync(this.HttpContext.User);
            var patientId = await this.userManager.GetUserIdAsync(patient);

            var viewModel = new AppointmentListViewModel
            {
                AppointmentList = this.appointmentService.GetUpcomingByPatient<AppointmentViewModel>(patientId),
            };

            return this.View(viewModel);
        }

        public IActionResult Book(string doctorId)
        {
            var viewModel = new AppointmentInputModel();
            viewModel.DoctorId = doctorId;
            viewModel.ServicesItems = this.servicesService.GetAllServices();
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Book(AppointmentInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("Book", input);
            }

            if (!this.servicesService.GetAllServices().Any(s => s.Id == input.ServiceId))
            {
                this.ModelState.AddModelError(nameof(input.ServiceId), "Service does not exist.");
            }

            if (!this.ModelState.IsValid)
            {
                input.ServicesItems = this.servicesService.GetAllServices();
                return this.View(input);
            }

            DateTime dateTime;
            try
            {
                dateTime = this.dateTimeParserService.ConvertStrings(input.AppointmentDate, input.AppointmentTime);
            }
            catch (System.Exception)
            {
                return this.RedirectToAction("Book", input);
            }

            var patient = await this.userManager.GetUserAsync(this.HttpContext.User);
            var patientId = await this.userManager.GetUserIdAsync(patient);

            await this.appointmentService.AddAppointmentAsync(patientId, input.DoctorId, input.ServiceId, input.Message, dateTime);
 
            // TODO return message "You have successfully requested an appointment"
            return this.RedirectToAction(nameof(this.Index));
        }

        public IActionResult All(string patientId)
        {
            var viewModel = new AppointmentListViewModel();
            viewModel.AppointmentList = this.appointmentService.GetUpcomingByPatient<AppointmentViewModel>(patientId);
            return this.View(viewModel);
        }

        public async Task<IActionResult> Details(string appointmentId)
        {
            var model = await this.appointmentService.GetByIdAsync<AppointmentViewModel>(appointmentId);
            return this.View(model);
        }

        public async Task<IActionResult> Reschedule(string appointmentId, string doctorId)
        {
            await this.appointmentService.RescheduleAppointmentAsync(appointmentId);
            return this.RedirectToAction("Book", "Doctors", new { doctorId });
        }

        public async Task<IActionResult> Cancel(string appointmentId)
        {
            await this.appointmentService.ChangeAppointmentStatusAsync(appointmentId, "Cancelled");
            return this.View();
        }

        public IActionResult Edit()
        {
            var model = new AppointmentEditInputModel();
            model.Clinics = this.clinicsService.GetAllClinics();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AppointmentEditInputModel input)
        {
            // TODO if user is not signed in redirect to login page
            if (!this.ModelState.IsValid)
            {
                var model = new AppointmentEditInputModel();
                model.Clinics = this.clinicsService.GetAllClinics();
                return this.View(model);
            }

            await this.appointmentService.EditMessageAsync(input.AppointmentId, input.Message);

            // TODO return message "You have successfully edited your appointment"
            return this.RedirectToAction(nameof(this.Details), new { input.AppointmentId });
        }
    }
}
