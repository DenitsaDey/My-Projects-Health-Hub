namespace HealthHub.Services.Data.Tests.UseInMemoryDatabase
{
    using System.Threading.Tasks;

    using HealthHub.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class SpecialtiesServiceTest : BaseServiceTests
    {
        private ISpecialtiesService Service => this.ServiceProvider.GetRequiredService<ISpecialtiesService>();

        /*  Task<IEnumerable<T>> GetAllSpecialtiesAsync<T>();

            IEnumerable<string> GetAllSpecialtiesNames();

            Task<T> GetByIdAsync<T>(string specialtyId);

            Task AddAsync(string name) - done

            IEnumerable<string> GetAllSpecialtiesIds();
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

        private async Task<Specialty> CreateSpecialtyAsync(string name)
        {
            var specialty = new Specialty
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
