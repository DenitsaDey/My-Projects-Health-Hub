namespace HealthHub.Web.ViewModels.Appointment
{
    using HealthHub.Data.Models;
    using HealthHub.Services.Mapping;

    public class AppointmentEditInputModel : HeaderSearchQueryModel, IMapFrom<Appointment>
    {
        public string Id { get; set; }

        public string Message { get; set; }
    }
}
