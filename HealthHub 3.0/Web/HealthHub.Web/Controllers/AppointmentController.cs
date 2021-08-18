﻿namespace HealthHub.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using HealthHub.Common;
    using HealthHub.Data.Models;
    using HealthHub.Data.Models.Enums;
    using HealthHub.Services;
    using HealthHub.Services.Data;
    using HealthHub.Services.Data.Clinics;
    using HealthHub.Services.Messaging;
    using HealthHub.Web.ViewModels;
    using HealthHub.Web.ViewModels.Appointment;
    using HealthHub.Web.ViewModels.Clinics;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class AppointmentController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDateTimeParserService dateTimeParserService;
        private readonly IServicesService servicesService;
        private readonly IAppointmentsService appointmentService;
        private readonly IClinicsService clinicsService;
        private readonly IEmailSender emailSender;
        private readonly IViewRenderService viewRenderService;

        public AppointmentController(
            UserManager<ApplicationUser> userManager,
            IDateTimeParserService dateTimeParserService,
            IServicesService servicesService,
            IAppointmentsService appointmentService,
            IClinicsService clinicsService,
            IEmailSender emailSender,
            IViewRenderService viewRenderService)
        {
            this.userManager = userManager;
            this.dateTimeParserService = dateTimeParserService;
            this.servicesService = servicesService;
            this.appointmentService = appointmentService;
            this.clinicsService = clinicsService;
            this.emailSender = emailSender;
            this.viewRenderService = viewRenderService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await this.userManager.GetUserAsync(this.HttpContext.User);
            var userId = await this.userManager.GetUserIdAsync(user);
            var viewModel = new AppointmentListViewModel();

            if (user.UserName == "doctor@doctor.com")
            {
                viewModel.AppointmentList = this.appointmentService.GetUpcomingByDoctor<AppointmentViewModel>(userId);
            }
            else
            {
                viewModel.AppointmentList = this.appointmentService.GetUpcomingByPatient<AppointmentViewModel>(userId);
            }

            viewModel.Clinics = this.clinicsService.GetAll();
            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.PatientRoleName)]
        public IActionResult Book(string doctorId)
        {
            var viewModel = new AppointmentInputModel();
            viewModel.Clinics = this.clinicsService.GetAll();
            viewModel.DoctorId = doctorId;
            viewModel.ServicesItems = this.servicesService.GetAllServices<ServicesViewModel>();
            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.PatientRoleName)]
        [HttpPost]
        public async Task<IActionResult> Book(AppointmentInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("Book", input);
            }

            if (!this.servicesService.GetAllServices<ServicesViewModel>().Any(s => s.Id == input.ServiceId))
            {
                this.ModelState.AddModelError(nameof(input.ServiceId), "Service does not exist.");
            }

            if (!this.ModelState.IsValid)
            {
                input.ServicesItems = this.servicesService.GetAllServices<ServicesViewModel>();
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

            string appointmentId = await this.appointmentService.AddAppointmentAsync(patientId, input.DoctorId, input.ServiceId, input.Message, dateTime);

            // automatically sending email with appointment details after user has requested an appointment through the HealthHub system
            var patientEmail = await this.userManager.GetEmailAsync(patient);
            var htmlModel = await this.appointmentService.GetByIdAsync<AppointmentViewModel>(appointmentId);
            htmlModel.Clinics = this.clinicsService.GetAll(); // as the partial Header View requires a list of clinics for the dropdown
            var htmlContent = await this.viewRenderService.RenderToStringAsync("~/Views/Appointment/Details.cshtml", htmlModel);

            await this.emailSender.SendEmailAsync("healthhub@healthhub.com", "Health Hub", patientEmail, "Your Appointment Request", htmlContent);

            // TODO return message "You have successfully requested an appointment"
            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> Details(string appointmentId)
        {
            var viewModel = await this.appointmentService.GetByIdAsync<AppointmentViewModel>(appointmentId);
            viewModel.Clinics = this.clinicsService.GetAll();
            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.PatientRoleName)]
        public async Task<IActionResult> Reschedule(string appointmentId)
        {
            var viewModel = await this.appointmentService.GetByIdAsync<AppointmentViewModel>(appointmentId);
            if (viewModel == null)
            {
                return new StatusCodeResult(404);
            }

            viewModel.Clinics = this.clinicsService.GetAll();
            return this.View(viewModel);
        }

        // rescheduling appointment by deleting completely the appointment and redirecting to the same doctor to book a new one
        [Authorize(Roles = GlobalConstants.PatientRoleName)]
        [HttpPost]
        public async Task<IActionResult> Delete(string appointmentId, string doctorId)
        {
            await this.appointmentService.RescheduleAppointmentAsync(appointmentId);
            return this.RedirectToAction(nameof(this.Book), new { doctorId });
        }

        [Authorize(Roles = GlobalConstants.PatientRoleName + "," + GlobalConstants.DoctorRoleName)]
        [HttpPost]
        public async Task<IActionResult> Cancel(string appointmentId)
        {
            var viewModel = new HeaderSearchQueryModel();
            viewModel.Clinics = this.clinicsService.GetAll();
            await this.appointmentService.ChangeAppointmentStatusAsync(appointmentId, "Cancelled");

            // automatically sending email with appointment status after patient has cancelled an appointment
            var patient = await this.userManager.GetUserAsync(this.HttpContext.User);
            var patientEmail = await this.userManager.GetEmailAsync(patient);
            var htmlModel = await this.appointmentService.GetByIdAsync<AppointmentViewModel>(appointmentId);
            htmlModel.Clinics = this.clinicsService.GetAll(); // as the partial Header View requires a list of clinics for the dropdown
            var htmlContent = await this.viewRenderService.RenderToStringAsync("~/Views/Appointment/Details.cshtml", htmlModel);

            await this.emailSender.SendEmailAsync("healthhub@healthhub.com", "Health Hub", patientEmail, "Your Appointment Cancellation", htmlContent);

            return this.RedirectToAction(nameof(this.Index));
        }

        [Authorize(Roles = GlobalConstants.PatientRoleName)]
        public IActionResult Edit()
        {
            var viewModel = new AppointmentEditInputModel();
            viewModel.Clinics = this.clinicsService.GetAll();
            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.PatientRoleName)]
        [HttpPost]
        public async Task<IActionResult> Edit(AppointmentEditInputModel input, string appointmentId)
        {
            if (!this.ModelState.IsValid)
            {
                var model = new AppointmentEditInputModel();
                model.Clinics = this.clinicsService.GetAll();
                return this.View(model);
            }

            await this.appointmentService.EditMessageAsync(appointmentId, input.Message);

            return this.RedirectToAction(nameof(this.Details), new { appointmentId });
        }
    }
}
