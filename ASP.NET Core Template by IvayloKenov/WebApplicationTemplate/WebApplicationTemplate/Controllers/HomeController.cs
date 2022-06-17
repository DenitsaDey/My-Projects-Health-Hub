using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
namespace WebApplicationTemplate.Controllers
{
    using System.Diagnostics;
    using WebApplicationTemplate.Models;
    using Microsoft.AspNetCore.Mvc;
    using WebApplicationTemplate.Services.Statistics;
    using WebApplicationTemplate.Models.Home;

    public class HomeController : Controller
    {
        private readonly IStatisticsService statisticsService;

        public HomeController(IStatisticsService statisticsService)
        {
            this.statisticsService = statisticsService;
        }

        public IActionResult Index()
        {
            var statistics = statisticsService.Total();

            return this.View(new IndexViewModel
            {
                TotalCars = statistics.Cars,
                TotalUsers = statistics.Users,
                TotalRents = statistics.Rents
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

    }
}
