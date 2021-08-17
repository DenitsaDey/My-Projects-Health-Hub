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

        //[Fact]
        //public async Task GetAllByClinicIdShouldReturnTheCorrectModelCollection()
        //{
        //    var cityArea = this.CreateCityAreaAsync("area");
        //    var cityAreaId = cityArea.Result.Id;

        //    // 2 insurnace companies
        //    var insurance1 = await this.CreateInsuranceAsync("insurnace 1");
        //    var insurance2 = await this.CreateInsuranceAsync("insurnace 2");

        //    // clinic 1 with 2 isnurance companies
        //    var clinic = await this.CreateClinicAsync(cityAreaId);
        //    var clinicId = clinic.Id;

        //    var insuranceinClinic1 = await this.CreateInsuranceClinicAsync(clinicId, insurance1.Id);
        //    var insuranceinClinic2 = await this.CreateInsuranceClinicAsync(clinicId, insurance2.Id);

        //    var model1 = new InsuranceClinicsViewModel()
        //    {
        //        Id = insuranceinClinic1.Id,
        //        ClinicId = insuranceinClinic1.ClinicId,
        //        ClinicName = insuranceinClinic1.Clinic.Name,
        //        InsuranceId = insuranceinClinic1.InsuranceId,
        //        InsuranceName = insuranceinClinic2.Insurance.Name,
        //    };

        //    var model2 = new InsuranceClinicsViewModel()
        //    {
        //        Id = insuranceinClinic2.Id,
        //        ClinicId = insuranceinClinic2.ClinicId,
        //        ClinicName = insuranceinClinic2.Clinic.Name,
        //        InsuranceId = insuranceinClinic2.InsuranceId,
        //        InsuranceName = insuranceinClinic2.Insurance.Name,
        //    };

        //    var resultModelCollection = this.Service.GetAllByClinicId<InsuranceClinicsViewModel>(clinicId);

        //    Assert.Equal(model1.Id, resultModelCollection.Last().Id);
        //    Assert.Equal(model1.ClinicId, resultModelCollection.Last().ClinicId);
        //    Assert.Equal(model1.InsuranceId, resultModelCollection.Last().InsuranceId);
        //    Assert.Equal(model2.Id, resultModelCollection.First().Id);
        //    Assert.Equal(model2.ClinicId, resultModelCollection.First().ClinicId);
        //    Assert.Equal(model2.InsuranceId, resultModelCollection.First().InsuranceId);
        //}

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
