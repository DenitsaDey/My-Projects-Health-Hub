﻿namespace HealthHub.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using HealthHub.Services;
    using HealthHub.Services.Data.Clinics;
    using HealthHub.Web.Controllers;
    using Microsoft.AspNetCore.Mvc;

    public class GatherCityAreasController : BaseController
    {
        private readonly ICityAreasScraperService cityAreasScraperService;

        public GatherCityAreasController(IClinicsService clinicsService, ICityAreasScraperService cityAreasScraperService)
        {
            this.cityAreasScraperService = cityAreasScraperService;
        }

        public IActionResult Index()
        {
            return this.Redirect("/");
        }

        public async Task<IActionResult> Add()
        {
            await this.cityAreasScraperService.ImportCityAreas();

            return this.Redirect("/");
        }
    }
}
