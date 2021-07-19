namespace HealthHub.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using HealthHub.Web.ViewModels.Appointment;

    public interface IAppointmentsService
    {
        Task AddAppointment(AppointmentInputModel input, string doctorId, string patientId);

        IEnumerable<AppointmentSummaryViewModel> GetAll(string patientId);

        AppointmentViewModel GetById(string appointmentId);

        Task ChangeAppointmentStaus(string appointmentId, string status);

        Task RescheduleAppointment(string appointmentId, string newDate);

        Task EditMessage(string appointmentId, string message);
    }
}
