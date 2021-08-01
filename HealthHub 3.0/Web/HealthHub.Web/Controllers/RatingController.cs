namespace HealthHub.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using HealthHub.Services.Data;
    using HealthHub.Services.Data.Ratings;
    using HealthHub.Web.ViewModels.Rating;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class RatingController : BaseController
    {
        private readonly IRatingsService ratingsService;
        private readonly IDoctorsService doctorsService;

        public RatingController(
            IRatingsService ratingsService,
            IDoctorsService doctorsService)
        {
            this.ratingsService = ratingsService;
            this.doctorsService = doctorsService;
        }

        public IActionResult Rate()
        {
            var viewModel = new RatingInputModel();
            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<RatingResponseViewModel>> Rate(RatingInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var patientId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this.ratingsService.SetRatingAsync(input.AppointmentId, patientId, input.Value, input.AdditionalComments);

            var doctorId = this.doctorsService.GetByAppointment(input.AppointmentId).Id;

            var docsAverage = this.ratingsService.GetDoctorAverageRating(doctorId);

            return new RatingResponseViewModel { AverageRating = docsAverage };
        }
    }
}
