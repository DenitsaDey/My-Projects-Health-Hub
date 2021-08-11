namespace HealthHub.Web.ViewModels.Clinics
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using HealthHub.Data.Models;
    using HealthHub.Services.Mapping;

    public class ClinicEditInputModel : ClinicInputModel, IMapFrom<Clinic>
    {
        public string Id { get; set; }
    }
}
