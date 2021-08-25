namespace HealthHub.Services.Data.Tests.UseInMemoryDatabase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HealthHub.Data.Models;
    using HealthHub.Services.Data.Clinics;
    using HealthHub.Web.ViewModels;
    using HealthHub.Web.ViewModels.Clinics;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class ClinicsServiceTests : BaseServiceTests
    {
        private IClinicsService Service => this.ServiceProvider.GetRequiredService<IClinicsService>();

        /*
         Task AddAsync(ClinicInputModel input); - done

        IEnumerable<ClinicSimpleViewModel> GetAll();

        IEnumerable<ClinicViewModel> GetAllClinics();

        Task<ClinicFilterViewModel> GetAllSearchedAsync(
            string specialtyId,
            string cityAreaId,
            string insuranceId,
            SearchSorting sorting,
            int pageNumber,
            int itemsPerPage);

        IEnumerable<ClinicViewModel> GetAllWithDeleted();

        ClinicViewModel GetById(string clinicId);

        Task UpdateAsync(string id, ClinicEditInputModel input); - done

        Task DeleteAsync(string id); - done

        bool ClinicExists(string id); - done
         */

        [Fact]
        public async Task AddAsyncShouldAddCorrectly()
        {
            await this.CreateClinicAsync();

            var input = new ClinicInputModel
            {
                Name = new NLipsum.Core.Word().ToString(),
                Address = new NLipsum.Core.Word().ToString(),
                AreaId = Guid.NewGuid().ToString(),
                MapUrl = new NLipsum.Core.Sentence().ToString(),
                InsuranceCompanies = new List<InsuranceViewModel>(),
            };

            await this.Service.AddAsync(input);

            var clinicsCount = await this.DbContext.Clinics.CountAsync();

            Assert.Equal(2, clinicsCount);
        }

        [Fact]
        public async Task UpdateAsyncShouldUpdateCorrectly()
        {
            var clinicId = this.CreateClinicAsync().Result.Id;

            var input = new ClinicEditInputModel
            {
                Name = "Updated Name",
                Address = "Updated Address",
                AreaId = Guid.NewGuid().ToString(),
                MapUrl = new NLipsum.Core.Sentence().ToString(),
                InsuranceCompanies = new List<InsuranceViewModel>(),
            };

            await this.Service.UpdateAsync(clinicId, input);

            var resultName = this.DbContext.Clinics.FirstOrDefault(c => c.Id == clinicId).Name;
            var resultAddress = this.DbContext.Clinics.FirstOrDefault(c => c.Id == clinicId).Address;

            Assert.Equal("Updated Name", resultName);
            Assert.Equal("Updated Address", resultAddress);
        }

        [Fact]
        public async Task DeleteAsyncShouldDeleteCorrectly()
        {
            var clinic = await this.CreateClinicAsync();

            await this.Service.DeleteAsync(clinic.Id);

            var clinicsCount = this.DbContext.Clinics.Where(x => !x.IsDeleted).ToArray().Count();
            var deletedClinic = await this.DbContext.Clinics.FirstOrDefaultAsync(x => x.Id == clinic.Id);
            Assert.Equal(0, clinicsCount);
            Assert.Null(deletedClinic);
        }

        [Fact]
        public void ClinicExistsShouldReturnCorrectBool()
        {
            var clinicId = this.CreateClinicAsync().Result.Id;

            var result1 = this.Service.ClinicExists(clinicId);
            var result2 = this.Service.ClinicExists(Guid.NewGuid().ToString());

            Assert.True(result1);
            Assert.False(result2);
        }

        private async Task<Clinic> CreateClinicAsync()
        {
            var clinic = new Clinic
            {
                Name = new NLipsum.Core.Word().ToString(),
                Address = new NLipsum.Core.Word().ToString(),
                AreaId = Guid.NewGuid().ToString(),
                MapUrl = new NLipsum.Core.Sentence().ToString(),
            };

            await this.DbContext.Clinics.AddAsync(clinic);
            await this.DbContext.SaveChangesAsync();
            this.DbContext.Entry<Clinic>(clinic).State = EntityState.Detached;
            return clinic;
        }
    }
}
