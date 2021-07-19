using System;
using System.Collections.Generic;
using System.Text;

namespace HealthHub.Web.ViewModels.Appointment
{
    public class AppointmentListViewModel
    {
        public IEnumerable<AppointmentSummaryViewModel> AppointmentList { get; set; }
    }
}
