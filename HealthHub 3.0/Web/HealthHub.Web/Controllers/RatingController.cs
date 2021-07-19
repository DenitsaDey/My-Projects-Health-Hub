namespace HealthHub.Web.Controllers
{
    using HealthHub.Services.Data;
    using HealthHub.Web.ViewModels.Rating;
    using Microsoft.AspNetCore.Mvc;

    public class RatingController : BaseController
    {
        private readonly IRatingService ratingService;

        public RatingController(IRatingService ratingService)
        {
            this.ratingService = ratingService;
        }

        public IActionResult Rate()
        {
            var viewModel = new RatingInputModel();
            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult Rate(RatingInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            return this.Redirect("/Appointment/All");
        }
    }
}
