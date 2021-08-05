namespace HealthHub.Web.Areas.Administration.Controllers
{
    using HealthHub.Common;
    using HealthHub.Services.Data.Clinics;
    using HealthHub.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
        public AdministrationController(IClinicsService clinicsService)
        {
        }

    }
}
