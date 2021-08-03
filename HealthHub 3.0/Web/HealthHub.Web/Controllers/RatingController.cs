namespace HealthHub.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using HealthHub.Services.Data;
    using HealthHub.Services.Data.Ratings;
    using HealthHub.Web.ViewModels.Appointment;
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

        public RatingController(
            IRatingsService ratingsService,
            IDoctorsService doctorsService,
            IAppointmentsService appointmentsService)
        {
            this.ratingsService = ratingsService;
            this.doctorsService = doctorsService;
            this.appointmentsService = appointmentsService;
        }

        public async Task<IActionResult> RatePastAppointment(AppointmentRatingViewModel model)
        {
            var viewModel = await this.appointmentsService.GetByIdAsync<AppointmentViewModel>(model.Id);
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Rate(AppointmentRatingViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("RatePastAppointment", model);
            }

            await this.ratingsService.SetRatingAsync(model.Id, model.RateValue, model.AdditionalComments);

            return this.RedirectToAction("Details", "Doctors", new { id = this.doctorsService.GetByAppointment(model.Id).Id });

            // Niki's template

            // var doctorId = this.doctorsService.GetByAppointment(input.AppointmentId).Id;

            // var docsAverage = this.ratingsService.GetDoctorAverageRating(doctorId);

            // return new RatingResponseViewModel { AverageRating = docsAverage };
        }
    }
}
