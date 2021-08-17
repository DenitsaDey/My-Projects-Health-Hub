namespace HealthHub.Services.Data.Tests.UseInMemoryDatabase
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using HealthHub.Data.Models;
    using HealthHub.Data.Models.Enums;
    using HealthHub.Services.Data;
    using HealthHub.Web.ViewModels.Appointment;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class AppointmentsServiceTests : BaseServiceTests
    {
        private IAppointmentsService Service => this.ServiceProvider.GetRequiredService<IAppointmentsService>();

        /*
         Task<T> GetByIdAsync<T>(string id);

        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetUpcomingByDoctor<T>(string doctorId);

        Task<IEnumerable<T>> GetPastByDoctorAsync<T>(string doctorId);

        IEnumerable<T> GetUpcomingByPatient<T>(string patientId);

        Task<IEnumerable<T>> GetPastByPatientAsync<T>(string patientId);

        Task<string> AddAppointmentAsync(string patientId, string doctorId, string serviceId, string message, DateTime dateTime); - done

        Task ChangeAppointmentStatusAsync(string appointmentId, string status); - done

        Task RescheduleAppointmentAsync(string appointmentId); - done

        Task EditMessageAsync(string appointmentId, string message); - done
         */
        [Fact]
        public async Task AddAppointmentAsyncShouldAddCorrectly()
        {
            await this.CreateAppointmentAsync();

            var patientId2 = Guid.NewGuid().ToString();
            var doctorId2 = Guid.NewGuid().ToString();
            var serviceId2 = Guid.NewGuid().ToString();
            var message2 = new NLipsum.Core.Word().ToString();
            var appointmentTime2 = DateTime.UtcNow.AddDays(5);
            await this.Service.AddAppointmentAsync(patientId2, doctorId2, serviceId2, message2, appointmentTime2);

            var appointmentsCount = await this.DbContext.Appointments.CountAsync();

            Assert.Equal(2, appointmentsCount);
        }

        [Fact]
        public async Task ChangeAppointmentStatusAsyncShouldAssignRequiredStatus()
        {
            var appointmentId = this.CreateAppointmentAsync().Result.Id;
            await this.Service.ChangeAppointmentStatusAsync(appointmentId, "Confirmed");

            var result = this.DbContext.Appointments.Where(x => x.Id == appointmentId).FirstOrDefault().AppointmentStatus.ToString();

            Assert.Equal("Confirmed", result);
        }

        [Fact]
        public async Task RescheduleAppointmentStatusAsyncShouldDeleteAppointment()
        {
            /* the logic used in the Application controller is that
                by rescheduling an appointment the patient deletes the current appointment
                and is redirected to book a new one with the same doctor details
            */
            var appointmentId = this.CreateAppointmentAsync().Result.Id;

            await this.Service.RescheduleAppointmentAsync(appointmentId);
            var appointmentsCount = this.DbContext.Appointments.Where(x => !x.IsDeleted).ToArray().Count();
            var deletedAppointment = await this.DbContext.Appointments.FirstOrDefaultAsync(x => x.Id == appointmentId);
            Assert.Equal(0, appointmentsCount);
            Assert.Null(deletedAppointment);
        }

        [Fact]
        public async Task EditMessageAsyncShouldAssignNewMessageToAppointment()
        {
            var appointment = this.CreateAppointmentAsync();
            var appointmentId = appointment.Result.Id;
            var newMessage = "bananas";

            await this.Service.EditMessageAsync(appointmentId, newMessage);

            var result = this.DbContext.Appointments.Where(x => x.Id == appointmentId).FirstOrDefault().Message;

            Assert.Equal("bananas", result);
        }

        private async Task<Appointment> CreateAppointmentAsync()
        {
            var appointment = new Appointment
            {
                DoctorId = Guid.NewGuid().ToString(),
                PatientId = Guid.NewGuid().ToString(),
                ServiceId = Guid.NewGuid().ToString(),
                Message = new NLipsum.Core.Word().ToString(),
                DateTime = DateTime.UtcNow.AddDays(2),
                AppointmentStatus = AppointmentStatus.Requested,
                HasBeenVoted = false,
            };

            await this.DbContext.Appointments.AddAsync(appointment);
            await this.DbContext.SaveChangesAsync();
            this.DbContext.Entry<Appointment>(appointment).State = EntityState.Detached;
            return appointment;
        }
    }
}
