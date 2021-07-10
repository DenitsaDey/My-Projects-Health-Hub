using HealthHub.Services;
using HealthHub.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthHub.Web.Areas.Administration.Controllers
{
    public class GatherInsuranceCompaniesController : BaseController
    {
        private readonly IInsuranceScraperService insuranceScraperService;

        public GatherInsuranceCompaniesController(IInsuranceScraperService insuranceScraperService)
         => this.insuranceScraperService = insuranceScraperService;

        public IActionResult Index()
        {
            return this.Redirect("/");
        }

        public async Task<IActionResult> Add()
        {
            await this.insuranceScraperService.ImportInsuranceCompanies();

            return this.Redirect("/");
        }
    }
}
