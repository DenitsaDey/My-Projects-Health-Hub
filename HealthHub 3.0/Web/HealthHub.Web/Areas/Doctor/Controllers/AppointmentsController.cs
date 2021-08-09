namespace HealthHub.Web.Areas.Doctor.Controllers
{
    using HealthHub.Data.Common.Repositories;
    using HealthHub.Data.Models;
    using HealthHub.Services;
    using HealthHub.Services.Data;
    using HealthHub.Services.Data.Clinics;
    using HealthHub.Web.ViewModels.Appointment;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Threading.Tasks;

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

        public async Task<IActionResult> Index()
        {
            // var doctor = await this.userManager.GetUserAsync(this.HttpContext.User);
            // var doctorId = await this.userManager.GetUserIdAsync(doctor);
            // for demo purposes the doctorId will be asigned manually for the doctor who happened to have most seeded appointments
            var doctorId = this.doctorsRepository.All()
                .OrderByDescending(d => d.ScheduledAppointments.Count)
                .FirstOrDefault()
                .Id;

            var viewModel = new AppointmentListViewModel
            {
                AppointmentList = this.appointmentsService.GetUpcomingByDoctor<AppointmentViewModel>(doctorId),
            };

            viewModel.Clinics = this.clinicsService.GetAllClinics();
            return this.View(viewModel);
        }

        public IActionResult Details()
        {
            return this.View();
        }
    }
}
