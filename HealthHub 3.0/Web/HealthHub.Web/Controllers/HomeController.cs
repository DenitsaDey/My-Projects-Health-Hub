namespace HealthHub.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using HealthHub.Data;
    using HealthHub.Web.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly ApplicationDbContext db;

        public HomeController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            var viewModel = new CountsViewModel()
            {
                DoctorsCount = this.db.Doctors.Count(),
                ClinicsCount = this.db.Clinics.Count(),
                SpecialtiesCount = this.db.Specialties.Count(),
                Appointments = this.db.Appointments.Count(),
            };
            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
