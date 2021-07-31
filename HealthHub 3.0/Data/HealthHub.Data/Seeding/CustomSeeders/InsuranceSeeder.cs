namespace HealthHub.Data.Seeding.CustomSeeders
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using HealthHub.Data.Models;

    public class InsuranceSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Insurances.Any())
            {
                return;
            }

            var insuranceCompanies = new Insurance[]
            {
                new Insurance
                {
                    Name = "Allianz",
                },
                new Insurance
                {
                    Name = "NextCare",
                },
                new Insurance
                {
                    Name = "Daman UAE",
                },
                new Insurance
                {
                    Name = "MSH",
                },
                new Insurance
                {
                    Name = "TRICARE",
                },
                new Insurance
                {
                    Name = "Aetna(Good Health)",
                },
                new Insurance
                {
                    Name = "IGI",
                },
                new Insurance
                {
                    Name = "Bupa",
                },
                new Insurance
                {
                    Name = "HENNER - GMC",
                },
                new Insurance
                {
                    Name = "MedNet BAHRAIN",
                },
                new Insurance
                {
                    Name = "MedNet UAE",
                },
                new Insurance
                {
                    Name = "NAS",
                },
                new Insurance
                {
                    Name = "SAICO",
                },
                new Insurance
                {
                    Name = "WapMed",
                },
                new Insurance
                {
                    Name = "AXAPPP",
                },
                new Insurance
                {
                    Name = "AMITY",
                },
                new Insurance
                {
                    Name = "HEALIX",
                },
                new Insurance
                {
                    Name = "EURO CENTER - TURKEY",
                },
                new Insurance
                {
                    Name = "UTA",
                },
                new Insurance
                {
                    Name = "MARM ASSIST.",
                },
                new Insurance
                {
                    Name = "CONNEX",
                },
                new Insurance
                {
                    Name = "AL KHALEEJ TAKAFUL",
                },
                new Insurance
                {
                    Name = "WHEALTH",
                },
                new Insurance
                {
                    Name = "GEMS",
                },
                new Insurance
                {
                    Name = "NowHealth",
                },
                new Insurance
                {
                    Name = "Oman Insurance",
                },
                new Insurance
                {
                    Name = "Metlife Alico",
                },
                new Insurance
                {
                    Name = "GlobeMed",
                },
                new Insurance
                {
                    Name = "AXA",
                },
                new Insurance
                {
                    Name = "QLM",
                },
                new Insurance
                {
                    Name = "BEEMA",
                },
                new Insurance
                {
                    Name = "SAIPEM",
                },
                new Insurance
                {
                    Name = "GULF ASSIST BAHRAIN",
                },
                new Insurance
                {
                    Name = "OCCUMED",
                },new Insurance
                {
                    Name = "HTH",
                },new Insurance
                {
                    Name = "GBG",
                },new Insurance
                {
                    Name = "WILLIAM RUSSELL",
                },new Insurance
                {
                    Name = "CISI",
                },new Insurance
                {
                    Name = "CIGNA",
                },new Insurance
                {
                    Name = "ALKOOT",
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
