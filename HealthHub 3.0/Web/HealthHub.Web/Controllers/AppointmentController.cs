namespace HealthHub.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using HealthHub.Data.Models;
    using HealthHub.Services;
    using HealthHub.Services.Data;
    using HealthHub.Web.ViewModels.Appointment;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class AppointmentController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDateTimeParserService dateTimeParserService;
        private readonly IServicesService servicesService;
        private readonly IAppointmentsService appointmentService;

        public AppointmentController(
            UserManager<ApplicationUser> userManger,
            IDateTimeParserService dateTimeParserService,
            IServicesService servicesService,
            IAppointmentsService appointmentService)
        {
            this.userManager = userManger;
            this.dateTimeParserService = dateTimeParserService;
            this.servicesService = servicesService;
            this.appointmentService = appointmentService;
        }

        public async Task<IActionResult> Index()
        {
            var patient = await this.userManager.GetUserAsync(this.HttpContext.User);
            var patientId = await this.userManager.GetUserIdAsync(patient);

            var viewModel = new AppointmentListViewModel
            {
                AppointmentList = this.appointmentService.GetUpcomingByPatient(patientId),
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
                return this.RedirectToAction("Book", new { input.DoctorId });
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

            var patient = await this.userManager.GetUserAsync(this.HttpContext.User);
            var patientId = await this.userManager.GetUserIdAsync(patient);

            await this.appointmentService.AddAppointmentAsync(input, patientId);
 
            //TODO return message "You have successfully requested an appointment"
            return this.RedirectToAction("Index");
        }

        public IActionResult All(string patientId)
        {
            var viewModel = new AppointmentListViewModel();
            viewModel.AppointmentList = this.appointmentService.GetUpcomingByPatient(patientId);
            return this.View(viewModel);
        }

        public async Task<IActionResult> Details(string appointmentId)
        {
            var model = await this.appointmentService.GetByIdAsync(appointmentId);
            return this.View(model);
        }

        public async Task<IActionResult> Reschedule(string appointmentId, string doctorId)
        {
            var model = await this.appointmentService.GetByIdAsync(appointmentId);
            return this.Redirect($"Doctors/Book/{model.DoctorId}");
        }

        public IActionResult Cancel(string appointmentId)
        {
            this.appointmentService.ChangeAppointmentStatusAsync(appointmentId, "Cancelled");
            return this.Redirect("/Appointment/All");
        }

        public async Task<IActionResult> Edit(string appointmentId)
        {
            var model = await this.appointmentService.GetByIdAsync(appointmentId);
            return this.View(model);
        }

        [HttpPost]
        public IActionResult Edit(string appointmentId, string message)
        {
            //TODO if user is not signed in redirect to login page

            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            this.appointmentService.EditMessageAsync(appointmentId, message);
            //TODO return message "You have successfully edited your appointment"
            return this.Redirect("/Appointment/Index");
        }
    }
}
