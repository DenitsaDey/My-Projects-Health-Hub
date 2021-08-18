namespace HealthHub.Data.Seeding.CustomSeeders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HealthHub.Common;
    using HealthHub.Data.Models;

    public class AppointmentsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Appointments.Any())
            {
                return;
            }

            var appointments = new List<Appointment>();

            // Get Patient Id
            var patientId = dbContext.Users.Where(x => x.Email == GlobalConstants.AccountsSeeding.PatientEmail).FirstOrDefault().Id;

            // Get Doctors Ids
            var doctorsId = dbContext.Doctors.OrderByDescending(x => x.CreatedOn).Select(x => x.Id).Take(6).ToList();

            // Add Upcoming Appointments
            appointments.Add(new Appointment
            {
                AppointmentTime = DateTime.UtcNow.AddDays(3),
                PatientId = patientId,
                DoctorId = doctorsId[0],
                ProcedureBooked = dbContext.Services.Where(x => x.Name == "Follow-up").FirstOrDefault(),
                AppointmentStatus = Models.Enums.AppointmentStatus.Confirmed,
                Message = "appointment test 1",
                HasBeenVoted = false,
            });

            appointments.Add(new Appointment
            {
                AppointmentTime = DateTime.UtcNow.AddDays(5),
                PatientId = patientId,
                DoctorId = doctorsId[1],
                ProcedureBooked = dbContext.Services.Where(x => x.Name == "Lab test").FirstOrDefault(),
                AppointmentStatus = Models.Enums.AppointmentStatus.Requested,
                Message = "appointment test 2",
                HasBeenVoted = false,
            });

            // Add Past Appointments
            appointments.Add(new Appointment
            {
                AppointmentTime = DateTime.UtcNow.AddDays(-2),
                PatientId = patientId,
                DoctorId = doctorsId[2],
                ProcedureBooked = dbContext.Services.Where(x => x.Name == "Initial check-up").FirstOrDefault(),
                AppointmentStatus = Models.Enums.AppointmentStatus.Completed,
                Message = "appointment test 3",
                HasBeenVoted = false,
            });

            appointments.Add(new Appointment
            {
                AppointmentTime = DateTime.UtcNow.AddDays(-3),
                PatientId = patientId,
                DoctorId = doctorsId[3],
                ProcedureBooked = dbContext.Services.Where(x => x.Name == "Initial check-up").FirstOrDefault(),
                AppointmentStatus = Models.Enums.AppointmentStatus.Cancelled,
                Message = "appointment test 4",
                HasBeenVoted = false,
            });

            appointments.Add(new Appointment
            {
                AppointmentTime = DateTime.UtcNow.AddDays(-3),
                PatientId = patientId,
                DoctorId = doctorsId[3],
                ProcedureBooked = dbContext.Services.Where(x => x.Name == "Vaccination").FirstOrDefault(),
                AppointmentStatus = Models.Enums.AppointmentStatus.Confirmed,
                Message = "appointment test 5",
                HasBeenVoted = false,
            });

            appointments.Add(new Appointment
            {
                AppointmentTime = DateTime.UtcNow.AddDays(-4),
                PatientId = patientId,
                DoctorId = doctorsId[3],
                ProcedureBooked = dbContext.Services.Where(x => x.Name == "Initial check-up").FirstOrDefault(),
                AppointmentStatus = Models.Enums.AppointmentStatus.Requested,
                Message = "appointment test 6",
                HasBeenVoted = false,
            });

            appointments.Add(new Appointment
            {
                AppointmentTime = DateTime.UtcNow.AddDays(-4),
                PatientId = patientId,
                DoctorId = doctorsId[5],
                ProcedureBooked = dbContext.Services.Where(x => x.Name == "Initial check-up").FirstOrDefault(),
                AppointmentStatus = Models.Enums.AppointmentStatus.NoShow,
                Message = "appointment test 7",
                HasBeenVoted = false,
            });

            // More Past Appointments for testing the Rating functionality
            appointments.Add(new Appointment
            {
                AppointmentTime = DateTime.UtcNow.AddDays(-5),
                PatientId = patientId,
                DoctorId = doctorsId[3],
                ProcedureBooked = dbContext.Services.Where(x => x.Name == "Vaccination").FirstOrDefault(),
                AppointmentStatus = Models.Enums.AppointmentStatus.Completed,
                Message = "test voting 1",
                HasBeenVoted = false,
            });

            appointments.Add(new Appointment
            {
                AppointmentTime = DateTime.UtcNow.AddDays(-5),
                PatientId = patientId,
                DoctorId = doctorsId[3],
                ProcedureBooked = dbContext.Services.Where(x => x.Name == "Medical document").FirstOrDefault(),
                AppointmentStatus = Models.Enums.AppointmentStatus.Completed,
                Message = "test voting 2",
                HasBeenVoted = false,
            });

            appointments.Add(new Appointment
            {
                AppointmentTime = DateTime.UtcNow.AddDays(-5),
                PatientId = patientId,
                DoctorId = doctorsId[3],
                ProcedureBooked = dbContext.Services.Where(x => x.Name == "Follow-up").FirstOrDefault(),
                AppointmentStatus = Models.Enums.AppointmentStatus.Completed,
                Message = "test voting 3",
                HasBeenVoted = false,
            });

            appointments.Add(new Appointment
            {
                AppointmentTime = DateTime.UtcNow.AddDays(-7),
                PatientId = patientId,
                DoctorId = doctorsId[4],
                ProcedureBooked = dbContext.Services.Where(x => x.Name == "Initial check-up").FirstOrDefault(),
                AppointmentStatus = Models.Enums.AppointmentStatus.Completed,
                Message = "test voting 4",
                HasBeenVoted = false,
            });

            appointments.Add(new Appointment
            {
                AppointmentTime = DateTime.UtcNow.AddDays(-7),
                PatientId = patientId,
                DoctorId = doctorsId[4],
                ProcedureBooked = dbContext.Services.Where(x => x.Name == "Initial check-up").FirstOrDefault(),
                AppointmentStatus = Models.Enums.AppointmentStatus.Completed,
                Message = "test voting 5",
                HasBeenVoted = false,
            });

            appointments.Add(new Appointment
            {
                AppointmentTime = DateTime.UtcNow.AddDays(-9),
                PatientId = patientId,
                DoctorId = doctorsId[5],
                ProcedureBooked = dbContext.Services.Where(x => x.Name == "Initial check-up").FirstOrDefault(),
                AppointmentStatus = Models.Enums.AppointmentStatus.Completed,
                Message = "test voting 6",
                HasBeenVoted = false,
            });

            await dbContext.Appointments.AddRangeAsync(appointments);
            await dbContext.SaveChangesAsync();
        }
    }
}
