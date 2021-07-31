namespace HealthHub.Data.Seeding.CustomSeeders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HealthHub.Data.Models;

    public class InsuranceClinicsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.InsuranceClinics.Any())
            {
                return;
            }

            var insuranceClinics = new List<InsuranceClinic>();

            var clinicsIds = dbContext.Clinics.Select(x => x.Id).Take(5).ToList();

            var insuranceIds = dbContext.Insurances.Select(x => x.Id).Take(28).ToList();

            for (int i = 0; i <= 7; i++)
            {
                insuranceClinics.Add(new InsuranceClinic
                {
                    ClinicId = clinicsIds[0],
                    InsuranceId = insuranceIds[i],
                });
            }

            for (int i = 5; i <= 12; i++)
            {
                insuranceClinics.Add(new InsuranceClinic
                {
                    ClinicId = clinicsIds[1],
                    InsuranceId = insuranceIds[i],
                });
            }

            for (int i = 10; i <= 17; i++)
            {
                insuranceClinics.Add(new InsuranceClinic
                {
                    ClinicId = clinicsIds[2],
                    InsuranceId = insuranceIds[i],
                });
            }

            for (int i = 15; i <= 22; i++)
            {
                insuranceClinics.Add(new InsuranceClinic
                {
                    ClinicId = clinicsIds[3],
                    InsuranceId = insuranceIds[i],
                });
            }

            for (int i = 10; i <= 26; i++)
            {
                insuranceClinics.Add(new InsuranceClinic
                {
                    ClinicId = clinicsIds[4],
                    InsuranceId = insuranceIds[i],
                });
            }

            await dbContext.InsuranceClinics.AddRangeAsync(insuranceClinics);
            await dbContext.SaveChangesAsync();
        }
    }
}
