namespace HealthHub.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using HealthHub.Services.Data;
    using HealthHub.Services.Data.Ratings;
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

        public async Task<IActionResult> RatePastAppointment(RatingInputModel input)
        {
            var viewModel = await this.appointmentsService.GetByIdAsync(input.AppointmentId);
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Rate(RatingInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("RatePastAppointment", input);
            }

            await this.ratingsService.SetRatingAsync(input.AppointmentId, input.RateValue, input.AdditionalComments);

            return this.RedirectToAction("Details", "Doctors", new { id = this.doctorsService.GetByAppointment(input.AppointmentId).Id });

            // Niki's template

            // var doctorId = this.doctorsService.GetByAppointment(input.AppointmentId).Id;

            // var docsAverage = this.ratingsService.GetDoctorAverageRating(doctorId);

            // return new RatingResponseViewModel { AverageRating = docsAverage };
        }
    }
}
