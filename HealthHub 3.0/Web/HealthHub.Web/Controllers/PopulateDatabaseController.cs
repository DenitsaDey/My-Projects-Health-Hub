using HealthHub.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthHub.Web.Controllers
{
    public class PopulateDatabaseController : BaseController
    {
        private readonly ICityAreasScraperService cityAreasScraperService;
        private readonly IInsuranceScraperService insuranceScraperService;

        public PopulateDatabaseController(
            ICityAreasScraperService cityAreasScraperService,
            IInsuranceScraperService insuranceScraperService)
        {
            this.cityAreasScraperService = cityAreasScraperService;
            this.insuranceScraperService = insuranceScraperService;
        }
        
        public IActionResult Index()
        {
            return this.View();
        }

        public async Task<IActionResult> Add()
        {
            await this.cityAreasScraperService.ImportCityAreas();
            await this.insuranceScraperService.ImportInsuranceCompanies();

            return this.View();
        }
    }
}
