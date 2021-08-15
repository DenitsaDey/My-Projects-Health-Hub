namespace HealthHub.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using HealthHub.Services.Data;
    using HealthHub.Services.Data.Clinics;
    using HealthHub.Services.Data.Ratings;
    using HealthHub.Web.ViewModels.Appointment;
    using HealthHub.Web.ViewModels.Doctor;
    using HealthHub.Web.ViewModels.Rating;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    // [ApiController]
    // [Route("api/[controller]")]
    [Authorize]
    public class RatingController : BaseController
    {
        private readonly IRatingsService ratingsService;
        private readonly IDoctorsService doctorsService;
        private readonly IAppointmentsService appointmentsService;
        private readonly IClinicsService clinicsService;

        public RatingController(
            IRatingsService ratingsService,
            IDoctorsService doctorsService,
            IAppointmentsService appointmentsService,
            IClinicsService clinicsService)
        {
            this.ratingsService = ratingsService;
            this.doctorsService = doctorsService;
            this.appointmentsService = appointmentsService;
            this.clinicsService = clinicsService;
        }

        public async Task<IActionResult> RatePastAppointment(string appointmentId)
        {
            var viewModel = await this.appointmentsService.GetByIdAsync<AppointmentRatingViewModel>(appointmentId);
            viewModel.Clinics = this.clinicsService.GetAll();
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Rate(AppointmentRatingViewModel model, string appointmentId)
        {
            var viewModel = await this.appointmentsService.GetByIdAsync<AppointmentRatingViewModel>(appointmentId);
            viewModel.Clinics = this.clinicsService.GetAll();

            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("RatePastAppointment", viewModel);
            }

            await this.ratingsService.SetRatingAsync(appointmentId, model.RateValue, model.AdditionalComments);

            return this.RedirectToAction("Details", "Doctors", new { doctorId = this.doctorsService.GetByAppointment<DoctorsViewModel>(appointmentId).Id });
        }
    }
}
