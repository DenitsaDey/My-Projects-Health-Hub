namespace HealthHub.Data.Seeding.CustomSeeders
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using HealthHub.Data.Models;

    public class ClinicsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Clinics.Any())
            {
                return;
            }

            var clinics = new Clinic[]
            {
                new Clinic
                {
                    Name = "Sidra Medicine",
                    MapUrl = "https://goo.gl/maps/RNLUGUN4bYZhwT7R9",
                    Address = "Al Gharrafa Str.",
                    AreaId = dbContext.CityAreas.Where(a => a.Name == "New Al Rayyan").FirstOrDefault().Id,
                },
                new Clinic
                {
                    Name = "Hamad General Hospital",
                    MapUrl = "https://goo.gl/maps/7SRjFcJRHbU4omkG7",
                    Address = "Al Rayyan Rd.",
                    AreaId = dbContext.CityAreas.Where(a => a.Name == "Hamad Medical City").FirstOrDefault().Id,
                },
                new Clinic
                {
                    Name = "Doha Clinic Hospital",
                    MapUrl = "https://g.page/dohaclinic?share",
                    Address = "New Al Mirqab Str.",
                    AreaId = dbContext.CityAreas.Where(a => a.Name == "Fareej Al Nasr").FirstOrDefault().Id,
                },
                new Clinic
                {
                    Name = "Al-Ahli Hospital",
                    MapUrl = "https://goo.gl/maps/mMLXv2mD8osjJYW5A",
                    Address = "Ahmed Bin Ali Str.",
                    AreaId = dbContext.CityAreas.Where(a => a.Name == "New Al Rayyan").FirstOrDefault().Id,
                },
                new Clinic
                {
                    Name = "Medicare Clinic",
                    MapUrl = "https://g.page/WestBayMedicare?share",
                    Address = "Omar Al Mukhtar Str.",
                    AreaId = dbContext.CityAreas.Where(a => a.Name == "West Bay").FirstOrDefault().Id,
                },
            };

            foreach (var clinic in clinics)
            {
                await dbContext.Clinics.AddAsync(clinic);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
