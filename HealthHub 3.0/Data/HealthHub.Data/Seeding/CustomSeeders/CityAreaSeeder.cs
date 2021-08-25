namespace HealthHub.Data.Seeding.CustomSeeders
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using HealthHub.Data.Models;

    public class CityAreaSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.CityAreas.Any())
            {
                return;
            }

            var cityAreas = new CityArea[]
            {
                new CityArea
                {
                    Name = "New Al Rayyan",
                },
                new CityArea
                {
                    Name = "Hamad Medical City",
                },
                new CityArea
                {
                    Name = "Fareej Al Nasr",
                },
                new CityArea
                {
                    Name = "Fareej Bin Omran",
                },
                new CityArea
                {
                    Name = "West Bay",
                },
            };

            foreach (var area in cityAreas)
            {
                await dbContext.CityAreas.AddAsync(area);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
