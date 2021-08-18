namespace HealthHub.Web.Controllers
{
    using System.Threading.Tasks;

    using HealthHub.Common;
    using HealthHub.Services.Data;
    using HealthHub.Services.Data.Clinics;
    using HealthHub.Services.Data.Ratings;
    using HealthHub.Web.ViewModels.Appointment;
    using HealthHub.Web.ViewModels.Clinics;
    using HealthHub.Web.ViewModels.Doctor;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    [Authorize(Roles = GlobalConstants.PatientRoleName)]
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
            if (appointmentId == null)
            {
                return this.NotFound();
            }

            var viewModel = await this.appointmentsService.GetByIdAsync<AppointmentRatingViewModel>(appointmentId);

            if (viewModel == null)
            {
                return this.RedirectToAction("Error404", "Home");
            }

            viewModel.Clinics = this.clinicsService.GetAll<ClinicSimpleViewModel>();
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Rate(AppointmentRatingViewModel model, string appointmentId)
        {
            var viewModel = await this.appointmentsService.GetByIdAsync<AppointmentRatingViewModel>(appointmentId);
            viewModel.Clinics = this.clinicsService.GetAll<ClinicSimpleViewModel>();

            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("RatePastAppointment", viewModel);
            }

            await this.ratingsService.SetRatingAsync(appointmentId, model.RateValue, model.AdditionalComments);

            return this.RedirectToAction("Details", "Doctors", new { doctorId = this.doctorsService.GetByAppointment<DoctorsViewModel>(appointmentId).Id });
        }
    }
}
