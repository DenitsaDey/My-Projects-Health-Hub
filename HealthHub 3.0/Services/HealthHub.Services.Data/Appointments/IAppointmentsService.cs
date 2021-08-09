namespace HealthHub.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using HealthHub.Web.ViewModels.Appointment;

    public interface IAppointmentsService
    {
        Task<T> GetByIdAsync<T>(string id);

        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetUpcomingByDoctor<T>(string doctorId);

        Task<IEnumerable<T>> GetPastByDoctorAsync<T>(string doctorId);

        IEnumerable<T> GetUpcomingByPatient<T>(string patientId);

        Task<IEnumerable<T>> GetPastByPatientAsync<T>(string patientId);

        Task AddAppointmentAsync(string patientId, string doctorId, string serviceId, string message, DateTime dateTime);

        Task ChangeAppointmentStatusAsync(string appointmentId, string status);

        Task RescheduleAppointmentAsync(string appointmentId);

        Task EditMessageAsync(string appointmentId, string message);
    }
}
