namespace HealthHub.Data.Seeding.CustomSeeders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HealthHub.Common;
    using HealthHub.Data.Models;

    public class RatingsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Appointments.Any())
            {
                return;
            }

            var ratings = new List<Rating>();

            // Get Patient Id
            var patientId = dbContext.Users.Where(x => x.Email == GlobalConstants.AccountsSeeding.PatientEmail).FirstOrDefault().Id;

            ratings.Add(new Rating
            {
                Id = Guid.NewGuid().ToString(),
                Value = 5,
                AppointmentId = dbContext.Appointments.Where(a => a.Message == "test voting 1").FirstOrDefault().Id,
                AdditionalComments = "excellent service",
            });

            ratings.Add(new Rating
            {
                Id = Guid.NewGuid().ToString(),
                Value = 4,
                AppointmentId = dbContext.Appointments.Where(a => a.Message == "test voting 2").FirstOrDefault().Id,
                AdditionalComments = "vwry good service",
            });

            ratings.Add(new Rating
            {
                Id = Guid.NewGuid().ToString(),
                Value = 4,
                AppointmentId = dbContext.Appointments.Where(a => a.Message == "test voting 3").FirstOrDefault().Id,
                AdditionalComments = "very good service",
            });

            ratings.Add(new Rating
            {
                Id = Guid.NewGuid().ToString(),
                Value = 5,
                AppointmentId = dbContext.Appointments.Where(a => a.Message == "test voting 4").FirstOrDefault().Id,
                AdditionalComments = "excellent service",
            });

            ratings.Add(new Rating
            {
                Id = Guid.NewGuid().ToString(),
                Value = 2,
                AppointmentId = dbContext.Appointments.Where(a => a.Message == "test voting 5").FirstOrDefault().Id,
                AdditionalComments = "average service",
            });

            ratings.Add(new Rating
            {
                Id = Guid.NewGuid().ToString(),
                Value = 5,
                AppointmentId = dbContext.Appointments.Where(a => a.Message == "test voting 6").FirstOrDefault().Id,
                AdditionalComments = "excellent service",
            });

            await dbContext.Ratings.AddRangeAsync(ratings);
            await dbContext.SaveChangesAsync();
        }
    }
}
