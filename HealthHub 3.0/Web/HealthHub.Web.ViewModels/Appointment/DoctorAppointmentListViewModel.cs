namespace HealthHub.Web.ViewModels.Appointment
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class DoctorAppointmentListViewModel
    {
        public IEnumerable<DoctorAppointmentViewModel> AppointmentList { get; set; }
    }
}
