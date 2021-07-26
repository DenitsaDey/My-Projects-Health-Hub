namespace HealthHub.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using HealthHub.Web.ViewModels.Appointment;

    public interface IAppointmentsService
    {
        Task<AppointmentViewModel> GetByIdAsync(string id);

        IEnumerable<AppointmentViewModel> GetAll();

        public IEnumerable<AppointmentViewModel> GetAllByDoctor(string doctorId);

        IEnumerable<AppointmentViewModel> GetUpcomingByPatient(string patientId);

        Task<IEnumerable<AppointmentViewModel>> GetPastByPatientAsync(string patientId);

        Task AddAppointmentAsync(AppointmentInputModel input, string patientId);

        Task ChangeAppointmentStatusAsync(string appointmentId, string status);

        Task RescheduleAppointmentAsync(string appointmentId, string newDate);

        Task EditMessageAsync(string appointmentId, string message);
    }
}
