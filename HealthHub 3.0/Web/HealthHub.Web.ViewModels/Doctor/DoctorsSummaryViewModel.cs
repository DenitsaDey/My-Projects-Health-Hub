using System;
using System.Collections.Generic;
using System.Text;

namespace HealthHub.Web.ViewModels.Doctor
{
    public class DoctorsSummaryViewModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ImageUrl { get; set; }

        public string Clinic { get; set; }

        public string Specialty { get; set; }
    }
}
