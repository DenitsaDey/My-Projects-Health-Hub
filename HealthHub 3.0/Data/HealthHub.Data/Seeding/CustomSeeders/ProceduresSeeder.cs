using HealthHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthHub.Data.Seeding.CustomSeeders
{
    public class ProceduresSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Procedures.Any())
            {
                return;
            }

            var procedures = new Procedure[]
            {
                new Procedure
                {
                    Name = "Initial check-up",
                    Description = "Visiting this doctor for the fist time or visiting this doctor with a new issue.",
                },
                new Procedure
                {
                    Name = "Follow-up",
                    Description = "Visiting this doctor for the same issue within a week of the last appointment.",
                },
                new Procedure
                {
                    Name = "Lab test",
                    Description = "Visiting this doctor for lab referral.",
                },
                new Procedure
                {
                    Name = "Vaccination",
                    Description = "Visiting this doctor for vaccination.",
                },
                new Procedure
                {
                    Name = "Medical Document",
                    Description = "Visiting this doctor for prescription, medical certificate for sick leave, return to work, fit for school etc.",
                },
                new Procedure
                {
                    Name = "Other",
                    Description = "Any other reason that was not mentioned above.",
                },
            };

            foreach (var procedure in procedures)
            {
                await dbContext.Procedures.AddAsync(procedure);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
