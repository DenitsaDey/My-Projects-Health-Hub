using HealthHub.Common;
using HealthHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthHub.Data.Seeding.CustomSeeders
{
    public class AppointmentsSeeder //: ISeeder
    {
        //public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        //{
        //    if (dbContext.Appointments.Any())
        //    {
        //        return;
        //    }

        //    var appointments = new List<Appointment>();

        //    // Get Patient Id
        //    var patientId = dbContext.Users.Where(x => x.Email == GlobalConstants.AccountsSeeding.PatientEmail).FirstOrDefault().Id;

        //    // Get Doctor Id
        //    var doctorId = dbContext.Users.Where(x => x.Email == GlobalConstants.AccountsSeeding.DoctorEmail).FirstOrDefault().Id;

        //    // Get ClinicProcedure Ids
        //    var countOfRegisteredClinicProcedures = dbContext.ClinicsProcedures.Count();
        //    var clinicProceduresIds = await dbContext.ClinicsProcedures.Select(x => x.Id).Take(countOfRegisteredClinicProcedures).ToListAsync();

        //    foreach (var cpId in clinicProceduresIds)
        //    {
        //        // Get a Service from each Salon
        //        var serviceId = dbContext.SalonServices.Where(x => x.SalonId == salonId).FirstOrDefault().ServiceId;

        //        // Add Upcoming Appointments
        //        appointments.Add(new Appointment
        //        {
        //            Id = Guid.NewGuid().ToString(),
        //            DateTime = DateTime.UtcNow.AddDays(5),
        //            UserId = userId,
        //            SalonId = salonId,
        //            ServiceId = serviceId,
        //        });

        //        // Add Past Appointments
        //        appointments.Add(new Appointment
        //        {
        //            Id = Guid.NewGuid().ToString(),
        //            DateTime = DateTime.UtcNow.AddDays(-5),
        //            UserId = userId,
        //            SalonId = salonId,
        //            ServiceId = serviceId,
        //            Confirmed = true,
        //        });

        //        // More Past Appointments for testing the RatePastAppointment option
        //        appointments.Add(new Appointment
        //        {
        //            Id = Guid.NewGuid().ToString(),
        //            DateTime = DateTime.UtcNow.AddDays(-10),
        //            UserId = userId,
        //            SalonId = salonId,
        //            ServiceId = serviceId,
        //            Confirmed = true,
        //        });
        //    }

        //    await dbContext.AddRangeAsync(appointments);
        //}
    }
}
