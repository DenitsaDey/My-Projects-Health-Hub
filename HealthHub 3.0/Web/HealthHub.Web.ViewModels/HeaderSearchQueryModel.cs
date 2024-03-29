﻿namespace HealthHub.Web.ViewModels
{
    using System.Collections.Generic;

    using HealthHub.Web.ViewModels.Clinics;

    public class HeaderSearchQueryModel
    {
        public string ClinicId { get; set; }

        public IEnumerable<ClinicSimpleViewModel> Clinics { get; set; }

        public string SearchName { get; set; }
    }
}
