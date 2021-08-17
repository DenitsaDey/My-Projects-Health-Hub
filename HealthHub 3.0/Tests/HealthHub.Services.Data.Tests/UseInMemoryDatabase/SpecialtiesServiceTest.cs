namespace HealthHub.Services.Data.Tests.UseInMemoryDatabase
{
    using System.Linq;
    using System.Threading.Tasks;

    using HealthHub.Data.Models;
    using HealthHub.Web.ViewModels;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class SpecialtiesServiceTest : BaseServiceTests
    {
        private ISpecialtiesService Service => this.ServiceProvider.GetRequiredService<ISpecialtiesService>();

        /*  Task<IEnumerable<T>> GetAllSpecialtiesAsync<T>();

            Task<T> GetByIdAsync<T>(string specialtyId); - done

            Task AddAsync(string name) - done
         */
        [Fact]
        public async Task AddAsyncShouldAddCorrectly()
        {
            await this.CreateSpecialtyAsync(new NLipsum.Core.Word().ToString());

            var name = new NLipsum.Core.Word().ToString();

            await this.Service.AddAsync(name);

            var specialtiesCount = await this.DbContext.Specialties.CountAsync();

            Assert.Equal(2, specialtiesCount);
        }

        [Fact]
        public async Task GetByIdAsyncShouldReturnTheCorrectModel()
        {
            var specialty = await this.CreateSpecialtyAsync("My specialty");
            var specialtyId = specialty.Id;
            var model = new SpecialtyViewModel()
            {
                Id = specialtyId,
                Name = specialty.Name,
            };

            var resultModel = await this.Service.GetByIdAsync<SpecialtyViewModel>(specialtyId);

            Assert.Equal(model.Id, resultModel.Id);
            Assert.Equal(model.Name, resultModel.Name);
        }

        [Fact]
        public async Task GetAllSpecialtiesAsyncShouldReturnTheCorrectModelCollection()
        {
            var specialty1 = await this.CreateSpecialtyAsync("My specialty");
            var specialtyId1 = specialty1.Id;
            var model1 = new SpecialtyViewModel()
            {
                Id = specialtyId1,
                Name = specialty1.Name,
            };

            var specialty2 = await this.CreateSpecialtyAsync("Your service");
            var specialtyId2 = specialty2.Id;
            var model2 = new ServicesViewModel()
            {
                Id = specialtyId2,
                Name = specialty2.Name,
            };

            var resultModelCollection = await this.Service.GetAllSpecialtiesAsync<SpecialtyViewModel>();

            // first and last bellow may need to be switched around as the method is async
            Assert.Equal(model1.Id, resultModelCollection.First().Id);
            Assert.Equal(model1.Name, resultModelCollection.First().Name);
            Assert.Equal(model2.Id, resultModelCollection.Last().Id);
            Assert.Equal(model2.Name, resultModelCollection.Last().Name);
        }

        private async Task<Specialty> CreateSpecialtyAsync(string name)
        {
            var specialty = new Specialty()
            {
                Name = name,
            };

            await this.DbContext.Specialties.AddAsync(specialty);
            await this.DbContext.SaveChangesAsync();
            this.DbContext.Entry<Specialty>(specialty).State = EntityState.Detached;
            return specialty;
        }
    }
}
