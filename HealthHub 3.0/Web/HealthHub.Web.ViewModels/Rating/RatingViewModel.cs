using System;
using System.Collections.Generic;
using System.Text;

namespace HealthHub.Web.ViewModels.Rating
{
    public class RatingViewModel
    {
        public string AppointmentId { get; set; }

        public string Doctor { get; set; }

        public string Service { get; set; }

        public string VisitedOn { get; set; }

        public string AdditionalComments { get; set; }
    }
}
