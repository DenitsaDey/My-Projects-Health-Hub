namespace HealthHub.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;

    using HealthHub.Data.Common.Repositories;
    using HealthHub.Data.Models;
    using HealthHub.Data.Models.Enums;
    using HealthHub.Web.ViewModels.Appointment;

    public class AppointmentsService : IAppointmentsService
    {
        private readonly IDeletableEntityRepository<Appointment> appointmentsRepository;
        private readonly IDeletableEntityRepository<Service> proceduresRepository;

        public AppointmentsService(
            IDeletableEntityRepository<Appointment> appointmentsRepository,
            IDeletableEntityRepository<Service> proceduresRepository)
        {
            this.appointmentsRepository = appointmentsRepository;
            this.proceduresRepository = proceduresRepository;
        }

        public async Task AddAppointment(AppointmentInputModel input, string doctorId, string patientId)
        {
            var newAppointment = new Appointment
            {
                AppointmentTime = DateTime.ParseExact(
                    input.AppointmentTime,
                    "ddMMyyyy HH:mm",
                    CultureInfo.InvariantCulture),
                ProcedureBooked = this.proceduresRepository.All().Where(p => p.Id == input.ServiceId).FirstOrDefault(),
                PatientId = patientId,
                DoctorId = doctorId,
                AppointmentStatus = AppointmentStatus.Requested,
                HasBeenVoted = false,
            };

            await this.appointmentsRepository.AddAsync(newAppointment);
            await this.appointmentsRepository.SaveChangesAsync();
        }

        public async Task CancelAppointment(string appointmentId)
        {
            var appointment = this.appointmentsRepository.All()
                .Where(a => a.Id == appointmentId)
                .FirstOrDefault();
            appointment.AppointmentStatus = AppointmentStatus.Cancelled;

            await this.appointmentsRepository.SaveChangesAsync();
        }

        public async Task EditMessage(string appointmentId, string message)
        {
            this.appointmentsRepository.All()
                .Where(a => a.Id == appointmentId)
                .FirstOrDefault().Message = message;

            await this.appointmentsRepository.SaveChangesAsync();
        }

        public IEnumerable<AppointmentSummaryViewModel> GetAll(string patientId)
        {
            var allAppointments = this.appointmentsRepository.All()
                .Where(a => a.PatientId == patientId)
                .OrderBy(a => a.AppointmentTime)
                .Select(a => new AppointmentSummaryViewModel
                {
                    Id = a.Id,
                    DoctorId = a.DoctorId,
                    AppointmentTime = a.AppointmentTime,
                    AppointmentStatus = a.AppointmentStatus.ToString(),
                    RatingValue = a.HasBeenVoted ? a.Rating.Value : 0,
                })
                .ToList();

            return allAppointments;
        }

        public async Task RescheduleAppointment(string appointmentId, string newDate)
        {
            var appointment = this.appointmentsRepository.All()
                 .Where(a => a.Id == appointmentId)
                 .FirstOrDefault();

            appointment.AppointmentTime = DateTime.ParseExact(newDate, "ddMMyyyy HH:mm", CultureInfo.InvariantCulture);

            appointment.AppointmentStatus = AppointmentStatus.Requested;

            await this.appointmentsRepository.SaveChangesAsync();
        }
    }
}
