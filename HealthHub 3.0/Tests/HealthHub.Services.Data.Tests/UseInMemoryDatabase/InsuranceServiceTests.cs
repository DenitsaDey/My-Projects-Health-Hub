namespace HealthHub.Services.Data.Tests.UseInMemoryDatabase
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using HealthHub.Data.Models;
    using HealthHub.Web.ViewModels;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class InsuranceServiceTests : BaseServiceTests
    {
        private IInsuranceService Service => this.ServiceProvider.GetRequiredService<IInsuranceService>();

        /*  IEnumerable<T> GetAllInsuranceCompanies<T>(); - done

            IEnumerable<T> GetAllByClinicId<T>(string clinicId);
         */

        [Fact]
        public async Task GetAllInsuranceCompaniesShouldReturnTheCorrectModelCollection()
        {
            var insurance1 = await this.CreateInsuranceAsync("insurnace 1");
            var insuranceId1 = insurance1.Id;
            var model1 = new InsuranceViewModel()
            {
                Id = insuranceId1,
                Name = insurance1.Name,
            };

            var insurance2 = await this.CreateInsuranceAsync("insurance 2");
            var insuranceId2 = insurance2.Id;
            var model2 = new InsuranceViewModel()
            {
                Id = insuranceId2,
                Name = insurance2.Name,
            };

            var resultModelCollection = this.Service.GetAllInsuranceCompanies<InsuranceViewModel>();

            Assert.Equal(model1.Id, resultModelCollection.Last().Id);
            Assert.Equal(model1.Name, resultModelCollection.Last().Name);
            Assert.Equal(model2.Id, resultModelCollection.First().Id);
            Assert.Equal(model2.Name, resultModelCollection.First().Name);
        }

        private async Task<Insurance> CreateInsuranceAsync(string name)
        {
            var insurance = new Insurance()
            {
                Name = name,
            };

            await this.DbContext.Insurances.AddAsync(insurance);
            await this.DbContext.SaveChangesAsync();
            this.DbContext.Entry<Insurance>(insurance).State = EntityState.Detached;
            return insurance;
        }

        private async Task<InsuranceClinic> CreateInsuranceClinicAsync(string clinicId, string insuranceId)
        {
            var insuranceInClinic = new InsuranceClinic()
            {
                ClinicId = clinicId,
                InsuranceId = insuranceId,
            };

            await this.DbContext.InsuranceClinics.AddAsync(insuranceInClinic);
            await this.DbContext.SaveChangesAsync();
            this.DbContext.Entry<InsuranceClinic>(insuranceInClinic).State = EntityState.Detached;
            return insuranceInClinic;
        }

        private async Task<Clinic> CreateClinicAsync(string areaId)
        {
            var clinic = new Clinic()
            {
                Name = "clinic",
                Address = new NLipsum.Core.Sentence().ToString(),
                MapUrl = new NLipsum.Core.Sentence().ToString(),
                AreaId = areaId,
            };

            await this.DbContext.Clinics.AddAsync(clinic);
            await this.DbContext.SaveChangesAsync();
            this.DbContext.Entry<Clinic>(clinic).State = EntityState.Detached;
            return clinic;
        }

        private async Task<CityArea> CreateCityAreaAsync(string name)
        {
            var cityArea = new CityArea()
            {
                Name = name,
            };

            await this.DbContext.CityAreas.AddAsync(cityArea);
            await this.DbContext.SaveChangesAsync();
            this.DbContext.Entry<CityArea>(cityArea).State = EntityState.Detached;
            return cityArea;
        }
    }
}
