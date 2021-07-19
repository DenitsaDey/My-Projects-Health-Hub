namespace HealthHub.Web.Controllers
{
    using System.Linq;

    using HealthHub.Services.Data;
    using HealthHub.Web.ViewModels.Appointment;
    using Microsoft.AspNetCore.Mvc;

    public class AppointmentController : BaseController
    {
        private readonly IServicesService servicesService;
        private readonly IAppointmentsService appointmentService;

        public AppointmentController(
            IServicesService servicesService,
            IAppointmentsService appointmentService)
        {
            this.servicesService = servicesService;
            this.appointmentService = appointmentService;
        }

        public IActionResult Book()
        {
            var viewModel = new AppointmentInputModel();
            viewModel.ServicesItems = this.servicesService.GetAllServices();
            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult Book(AppointmentInputModel input)
        {
            //TODO if user is not signed in redirect to login page
            if(!this.servicesService.GetAllServices().Any(s=>s.Id == input.ServiceId))
            {
                this.ModelState.AddModelError(nameof(input.ServiceId), "Service does not exist.");
            }

            if (!this.ModelState.IsValid)
            {
                input.ServicesItems = this.servicesService.GetAllServices();
                return this.View(input);
            }

            //TODO: patient and doctor Id
            //var patientId = this.UserManager.GetId();
            //var doctorId = ...

             //this.appointmentService.AddAppointment(input, doctorId, patientId)
            //TODO return message "You have successfully requested an appointment"
            return this.Redirect("/Appointment/All");
        }

        public IActionResult All(string patientId)
        {
            var viewModel = new AppointmentListViewModel();
            viewModel.AppointmentList = this.appointmentService.GetAll(patientId);
            return this.View(viewModel);
        }

        public IActionResult Details(string appointmentId)
        {
            var model = this.appointmentService.GetById(appointmentId);
            return this.View(model);
        }

        public IActionResult Cancel(string appointmentId)
        {
            var model = this.appointmentService.ChangeAppointmentStaus(appointmentId, "Cancelled");
            return this.View();
        }

        public IActionResult Edit()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Edit(string appointmentId, string message)
        {
            //TODO if user is not signed in redirect to login page

            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            this.appointmentService.EditMessage(appointmentId, message);
            //TODO return message "You have successfully edited your appointment"
            return this.Redirect("/Appointment/All");
        }
    }
}
