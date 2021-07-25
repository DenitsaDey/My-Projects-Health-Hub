using HealthHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthHub.Data.Seeding.CustomSeeders
{
    public class InsuranceSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.CityAreas.Any())
            {
                return;
            }

            var insuranceCompanies = new Insurance[]
            {
                new Insurance
                {
                    Name = "Allianz",
                },
            };

            foreach (var insurance in insuranceCompanies)
            {
                await dbContext.Insurances.AddAsync(insurance);
                await dbContext.SaveChangesAsync();

            }
        }
    }
}
