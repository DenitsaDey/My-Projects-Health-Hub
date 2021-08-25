namespace HealthHub.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HealthHub.Data.Common.Repositories;
    using HealthHub.Data.Models;
    using HealthHub.Data.Models.Enums;
    using HealthHub.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class AppointmentsService : IAppointmentsService
    {
        private readonly IDeletableEntityRepository<Appointment> appointmentsRepository;
        private readonly IDeletableEntityRepository<Rating> ratingsRepository;

        public AppointmentsService(
            IDeletableEntityRepository<Appointment> appointmentsRepository,
            IDeletableEntityRepository<Rating> ratingsRepository)
        {
            this.appointmentsRepository = appointmentsRepository;
            this.ratingsRepository = ratingsRepository;
        }

        public async Task<T> GetByIdAsync<T>(string id)
        {
            var currentAppointment = await this.appointmentsRepository.All()
                .Where(a => a.Id == id)
                .OrderByDescending(a => a.AppointmentTime)
                .To<T>()
                .FirstOrDefaultAsync();

            return currentAppointment;
        }

        public IEnumerable<T> GetAll<T>()
        {
            var allAppointments = this.appointmentsRepository.All()
                .OrderByDescending(a => a.AppointmentTime)
                .To<T>()
                .ToList();

            return allAppointments;
        }

        public IEnumerable<T> GetUpcomingByDoctor<T>(string doctorId)
        {
            var allAppointments = this.appointmentsRepository.All()
                .Where(a => a.DoctorId == doctorId
                && a.AppointmentTime.Date >= DateTime.UtcNow.Date)
                .OrderBy(a => a.AppointmentTime)
                .To<T>()
                .ToList();

            return allAppointments;
        }

        public async Task<IEnumerable<T>> GetPastByDoctorAsync<T>(string doctorId)
        {
            var allAppointments = await this.appointmentsRepository.All()
                .Where(a => a.DoctorId == doctorId
                && a.AppointmentTime.Date < DateTime.UtcNow.Date)
                .OrderByDescending(a => a.AppointmentTime)
                .To<T>()
                .ToListAsync();

            return allAppointments;
        }

        public IEnumerable<T> GetUpcomingByPatient<T>(string patientId)
        {
            var allAppointments = this.appointmentsRepository.All()
                .Where(a => a.PatientId == patientId
                && a.AppointmentTime.Date >= DateTime.UtcNow.Date)
                .OrderBy(a => a.AppointmentTime)
                .To<T>()
                .ToList();

            return allAppointments;
        }

        public async Task<IEnumerable<T>> GetPastByPatientAsync<T>(string patientId)
        {
            var allAppointments = await this.appointmentsRepository.All()
                .Where(a => a.PatientId == patientId
                && a.AppointmentTime.Date < DateTime.UtcNow.Date)
                .OrderByDescending(a => a.AppointmentTime)
                .To<T>()
                .ToListAsync();

            return allAppointments;
        }

        public async Task<string> AddAppointmentAsync(string patientId, string doctorId, string serviceId, string message, DateTime dateTime)
        {
            var newAppointment = new Appointment
            {
                AppointmentTime = dateTime,
                ServiceId = serviceId,
                PatientId = patientId,
                DoctorId = doctorId,
                Message = message,
                AppointmentStatus = AppointmentStatus.Requested,
                HasBeenVoted = false,
            };

            await this.appointmentsRepository.AddAsync(newAppointment);
            await this.appointmentsRepository.SaveChangesAsync();

            // for the purpose of sending a confirmation email referring to the specific appointment that has been requetsed (Appointment/Book)
            return newAppointment.Id;
        }

        public async Task ChangeAppointmentStatusAsync(string appointmentId, string status)
        {
            var appointment = this.appointmentsRepository.All()
                .Where(a => a.Id == appointmentId)
                .FirstOrDefault();
            appointment.AppointmentStatus = Enum.Parse<AppointmentStatus>(status);

            await this.appointmentsRepository.SaveChangesAsync();
        }

        public async Task EditMessageAsync(string appointmentId, string message)
        {
            this.appointmentsRepository.All()
                .Where(a => a.Id == appointmentId)
                .FirstOrDefault().Message = message;

            await this.appointmentsRepository.SaveChangesAsync();
        }

        // rescheduling appointment by deleting completely the appointment and redirecting to the same doctor to book a new one
        public async Task RescheduleAppointmentAsync(string appointmentId)
        {
            var appointment = this.appointmentsRepository.All()
                 .Where(a => a.Id == appointmentId)
                 .FirstOrDefault();

            this.appointmentsRepository.Delete(appointment);

            await this.appointmentsRepository.SaveChangesAsync();
        }
    }
}
