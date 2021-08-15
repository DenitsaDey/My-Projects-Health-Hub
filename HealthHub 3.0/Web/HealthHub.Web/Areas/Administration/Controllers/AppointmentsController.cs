namespace HealthHub.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HealthHub.Data.Models;
    using HealthHub.Services;
    using HealthHub.Services.Data;
    using HealthHub.Services.Data.Clinics;
    using HealthHub.Services.Messaging;
    using HealthHub.Web.ViewModels.Appointment;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class AppointmentsController : AdministrationController
    {
        private readonly IAppointmentsService appointmentsService;

        public AppointmentsController(
            IAppointmentsService appointmentsService)
        {
            this.appointmentsService = appointmentsService;
        }

        public IActionResult Index()
        {
            var viewModel = new AppointmentListViewModel
            {
                AppointmentList = this.appointmentsService.GetAll<AppointmentViewModel>(),
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> Details(string appointmentId)
        {
            var viewModel = await this.appointmentsService.GetByIdAsync<AppointmentViewModel>(appointmentId);
            return this.View(viewModel);
        }
    }
}
