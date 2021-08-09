namespace HealthHub.Web.Areas.Doctor.Controllers
{
    using HealthHub.Common;
    using HealthHub.Web.Controllers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.DoctorRoleName)]
    [Area("Doctor")]
    public class DoctorBaseController : BaseController
    {
    }
}
