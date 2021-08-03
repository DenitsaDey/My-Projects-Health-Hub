namespace HealthHub.Web.ViewModels.Appointment
{
    using System.Collections.Generic;

    public class AppointmentListViewModel : HeaderSearchQueryModel
    {
        public IEnumerable<AppointmentViewModel> AppointmentList { get; set; }
    }
}
