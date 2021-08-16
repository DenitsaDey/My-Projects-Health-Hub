namespace HealthHub.Services.Data.Tests.UseInMemoryDatabase
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using HealthHub.Data.Models;
    using HealthHub.Data.Models.Enums;
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

            var patientId2 = new NLipsum.Core.Word().ToString();
            var doctorId2 = new NLipsum.Core.Word().ToString();
            var serviceId2 = new NLipsum.Core.Word().ToString();
            var message2 = new NLipsum.Core.Word().ToString();
            var appointmentTime2 = DateTime.UtcNow.AddDays(2);
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
        public async Task RescheduleAppointmentStatusAsyncShouldAssignRequiredStatus()
        {
            this.CreateAppointmentAsync();
        }

        [Fact]
        public async Task EditMessageAsyncShouldAssignNewMessageToAppointment()
        {
            var appointment = this.CreateAppointmentAsync();
            var appointmentId = appointment.Result.Id;
            var newMessage = "bananas";

            await this.Service.EditMessageAsync(appointmentId, newMessage);

            // var result = this.DbContext.Appointments.Where(x => x.Id == appointmentId).FirstOrDefault().Message;
            var result = appointment.Result.Message;

            Assert.Equal("bananas", result);
        }

        private async Task<Appointment> CreateAppointmentAsync()
        {
            var appointment = new Appointment
            {
                Id = Guid.NewGuid().ToString(),
                DoctorId = new NLipsum.Core.Word().ToString(),
                PatientId = new NLipsum.Core.Word().ToString(),
                ServiceId = new NLipsum.Core.Word().ToString(),
                Message = new NLipsum.Core.Word().ToString(),
                AppointmentTime = DateTime.UtcNow.AddDays(2),
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
