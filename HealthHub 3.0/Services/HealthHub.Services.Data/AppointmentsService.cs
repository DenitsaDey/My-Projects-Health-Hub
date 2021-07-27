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
    using HealthHub.Services;
    using HealthHub.Web.ViewModels.Appointment;
    using Microsoft.EntityFrameworkCore;

    public class AppointmentsService : IAppointmentsService
    {
        private readonly IDeletableEntityRepository<Appointment> appointmentsRepository;
        private readonly IDeletableEntityRepository<Service> proceduresRepository;
        private readonly IDateTimeParserService dateTimeParserService;

        public AppointmentsService(
            IDeletableEntityRepository<Appointment> appointmentsRepository,
            IDeletableEntityRepository<Service> proceduresRepository,
            IDateTimeParserService dateTimeParserService)
        {
            this.appointmentsRepository = appointmentsRepository;
            this.proceduresRepository = proceduresRepository;
            this.dateTimeParserService = dateTimeParserService;
        }

        public async Task<AppointmentViewModel> GetByIdAsync(string id)
        {
            var currentAppointment = await this.appointmentsRepository.All()
                .Where(a => a.Id == id)
                .OrderByDescending(a => a.AppointmentTime)
                .Select(a => new AppointmentViewModel
                {
                    Id = a.Id,
                    DoctorId = a.DoctorId,
                    DoctorName = a.Doctor.FirstName + " " + a.Doctor.LastName,
                    Clinic = a.Doctor.Clinic.Name,
                    ClinicId = a.Doctor.Clinic.Id,
                    Address = a.Doctor.Clinic.Address,
                    Location = a.Doctor.Clinic.MapUrl,
                    ServiceId = a.ServiceId,
                    ProcedureBooked = a.ProcedureBooked.Name,
                    AppointmentTime = a.AppointmentTime,
                    AppointmentStatus = a.AppointmentStatus.ToString(),
                    RatingValue = a.HasBeenVoted ? a.Rating.Value : 0,
                })
                .FirstOrDefaultAsync();

            return currentAppointment;
        }

        public IEnumerable<AppointmentViewModel> GetAll()
        {
            var allAppointments = this.appointmentsRepository.All()
                .OrderByDescending(a => a.AppointmentTime)
                .Select(a => new AppointmentViewModel
                {
                    Id = a.Id,
                    DoctorId = a.DoctorId,
                    DoctorName = a.Doctor.FirstName + " " + a.Doctor.LastName,
                    Clinic = a.Doctor.Clinic.Name,
                    ClinicId = a.Doctor.Clinic.Id,
                    Address = a.Doctor.Clinic.Address,
                    Location = a.Doctor.Clinic.MapUrl,
                    ServiceId = a.ServiceId,
                    ProcedureBooked = a.ProcedureBooked.Name,
                    AppointmentTime = a.AppointmentTime,
                    AppointmentStatus = a.AppointmentStatus.ToString(),
                    RatingValue = a.HasBeenVoted ? a.Rating.Value : 0,
                })
                .ToList();

            return allAppointments;
        }

        public IEnumerable<AppointmentViewModel> GetAllByDoctor(string doctorId)
        {
            var allAppointments = this.appointmentsRepository.All()
                .Where(a => a.DoctorId == doctorId)
                .OrderByDescending(a => a.AppointmentTime)
                .Select(a => new AppointmentViewModel
                {
                    Id = a.Id,
                    DoctorId = a.DoctorId,
                    DoctorName = a.Doctor.FirstName + " " + a.Doctor.LastName,
                    Clinic = a.Doctor.Clinic.Name,
                    ClinicId = a.Doctor.Clinic.Id,
                    Address = a.Doctor.Clinic.Address,
                    Location = a.Doctor.Clinic.MapUrl,
                    ServiceId = a.ServiceId,
                    ProcedureBooked = a.ProcedureBooked.Name,
                    AppointmentTime = a.AppointmentTime,
                    AppointmentStatus = a.AppointmentStatus.ToString(),
                    RatingValue = a.HasBeenVoted ? a.Rating.Value : 0,
                })
                .ToList();

            return allAppointments;
        }

        public IEnumerable<AppointmentViewModel> GetUpcomingByPatient(string patientId)
        {
            var allAppointments = this.appointmentsRepository.All()
                .Where(a => a.PatientId == patientId
                && a.AppointmentTime.Date > DateTime.UtcNow.Date)
                .OrderBy(a => a.AppointmentTime)
                .Select(a => new AppointmentViewModel
                {
                    Id = a.Id,
                    DoctorId = a.DoctorId,
                    DoctorName = a.Doctor.FirstName + " " + a.Doctor.LastName,
                    Clinic = a.Doctor.Clinic.Name,
                    ClinicId = a.Doctor.Clinic.Id,
                    Address = a.Doctor.Clinic.Address,
                    Location = a.Doctor.Clinic.MapUrl,
                    ServiceId = a.ServiceId,
                    ProcedureBooked = a.ProcedureBooked.Name,
                    AppointmentTime = a.AppointmentTime,
                    AppointmentStatus = a.AppointmentStatus.ToString(),
                    RatingValue = a.HasBeenVoted ? a.Rating.Value : 0,
                })
                .ToList();

            return allAppointments;
        }

        public async Task<IEnumerable<AppointmentViewModel>> GetPastByPatientAsync(string patientId)
        {
            var allAppointments = this.appointmentsRepository.All()
                .Where(a => a.PatientId == patientId
                && a.AppointmentTime.Date < DateTime.UtcNow.Date
                && a.AppointmentStatus == AppointmentStatus.Completed)
                .OrderByDescending(a => a.AppointmentTime)
                .Select(a => new AppointmentViewModel
                {
                    Id = a.Id,
                    DoctorId = a.DoctorId,
                    DoctorName = a.Doctor.FirstName + " " + a.Doctor.LastName,
                    Clinic = a.Doctor.Clinic.Name,
                    ClinicId = a.Doctor.Clinic.Id,
                    Address = a.Doctor.Clinic.Address,
                    Location = a.Doctor.Clinic.MapUrl,
                    ServiceId = a.ServiceId,
                    ProcedureBooked = a.ProcedureBooked.Name,
                    AppointmentTime = a.AppointmentTime,
                    AppointmentStatus = a.AppointmentStatus.ToString(),
                    RatingValue = a.HasBeenVoted ? a.Rating.Value : 0,
                })
                .ToList();

            return allAppointments;
        }

        public async Task AddAppointmentAsync(AppointmentInputModel input, string patientId)
        {
            var dateTime = this.dateTimeParserService.ConvertStrings(input.AppointmentDate, input.AppointmentTime);

            var newAppointment = new Appointment
            {
                AppointmentTime = dateTime,
                ProcedureBooked = this.proceduresRepository.All().Where(p => p.Id == input.ServiceId).FirstOrDefault(),
                PatientId = patientId,
                DoctorId = input.DoctorId,
                AppointmentStatus = AppointmentStatus.Requested,
                HasBeenVoted = false,
            };

            await this.appointmentsRepository.AddAsync(newAppointment);
            await this.appointmentsRepository.SaveChangesAsync();
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

        public async Task RescheduleAppointmentAsync(string appointmentId, string newDate)
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
