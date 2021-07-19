namespace HealthHub.Data.Seeding.CustomSeeders
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using HealthHub.Data.Models;

    public class ServicesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Services.Any())
            {
                return;
            }

            var services = new Service[]
            {
                new Service
                {
                    Name = "Initial check-up",
                    Description = "Visiting this doctor for the fist time or visiting this doctor with a new issue.",
                },
                new Service
                {
                    Name = "Follow-up",
                    Description = "Visiting this doctor for the same issue within a week of the last appointment.",
                },
                new Service
                {
                    Name = "Lab test",
                    Description = "Visiting this doctor for lab referral.",
                },
                new Service
                {
                    Name = "Vaccination",
                    Description = "Visiting this doctor for vaccination.",
                },
                new Service
                {
                    Name = "Medical Document",
                    Description = "Visiting this doctor for prescription, medical certificate for sick leave, return to work, fit for school etc.",
                },
                new Service
                {
                    Name = "Other",
                    Description = "Any other reason that was not mentioned above.",
                },
            };

            foreach (var service in services)
            {
                await dbContext.Services.AddAsync(service);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
