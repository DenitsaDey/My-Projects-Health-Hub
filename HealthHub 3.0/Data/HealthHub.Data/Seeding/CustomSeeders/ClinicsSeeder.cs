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
                    MapUrl = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d28851.56451135124!2d51.42728353581314!3d25.322825483837864!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x3e45dc0eccdd3dc5%3A0x4f6662f9a36430a2!2sSidra%20Medicine!5e0!3m2!1sen!2sqa!4v1627698622600!5m2!1sen!2sqa",
                    Address = "Al Gharrafa Str.",
                    AreaId = dbContext.CityAreas.Where(a => a.Name == "New Al Rayyan").FirstOrDefault().Id,
                },
                new Clinic
                {
                    Name = "Hamad General Hospital",
                    MapUrl = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d28858.4622616826!2d51.486141335812434!3d25.293861283851303!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x3e45dad73c965c45%3A0xde933cd8262710bb!2sHamad%20General%20Hospital!5e0!3m2!1sen!2sqa!4v1627699069890!5m2!1sen!2sqa",
                    Address = "Al Rayyan Rd.",
                    AreaId = dbContext.CityAreas.Where(a => a.Name == "Hamad Medical City").FirstOrDefault().Id,
                },
                new Clinic
                {
                    Name = "Doha Clinic Hospital",
                    MapUrl = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d28863.154832929868!2d51.48566433581201!3d25.274139083860398!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x3e45dab86cd23845%3A0x747e51c20cd6b12c!2sDoha%20Clinic%20Hospital!5e0!3m2!1sen!2sqa!4v1627698934893!5m2!1sen!2sqa",
                    Address = "New Al Mirqab Str.",
                    AreaId = dbContext.CityAreas.Where(a => a.Name == "Fareej Al Nasr").FirstOrDefault().Id,
                },
                new Clinic
                {
                    Name = "Al-Ahli Hospital",
                    MapUrl = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d28855.116972807784!2d51.480902635812825!3d25.307912283844775!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x3e45db3c1a62c457%3A0xcbad0752c273b5c4!2sAl%20Ahli%20Hospital!5e0!3m2!1sen!2sqa!4v1627698839831!5m2!1sen!2sqa",
                    Address = "Ahmed Bin Ali Str.",
                    AreaId = dbContext.CityAreas.Where(a => a.Name == "New Al Rayyan").FirstOrDefault().Id,
                },
                new Clinic
                {
                    Name = "Medicare Clinic",
                    MapUrl = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d28851.468438262713!2d51.50927243581319!3d25.323228683837673!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x3e45c5e130553aa1%3A0x85905e4d63ae8ca2!2sWest%20Bay%20Medicare!5e0!3m2!1sen!2sqa!4v1627699008199!5m2!1sen!2sqa",
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
