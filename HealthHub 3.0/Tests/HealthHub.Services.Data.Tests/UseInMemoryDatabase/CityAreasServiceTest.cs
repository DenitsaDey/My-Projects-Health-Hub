namespace HealthHub.Services.Data.Tests.UseInMemoryDatabase
{
    using System.Threading.Tasks;

    using HealthHub.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class CityAreasServiceTest : BaseServiceTests
    {
        private ICityAreasService Service => this.ServiceProvider.GetRequiredService<ICityAreasService>();

        /* AddAsync - done
         * GetAllCityAreasAsync
         * GetByIdAsync
         */

        [Fact]
        public async Task AddAsyncShouldAddCorrectly()
        {
            await this.CreateCityAreaAsync();

            var name = new NLipsum.Core.Word().ToString();

            await this.Service.AddAsync(name);

            var cityAreasCount = await this.DbContext.CityAreas.CountAsync();

            Assert.Equal(2, cityAreasCount);
        }

        private async Task<CityArea> CreateCityAreaAsync()
        {
            var cityArea = new CityArea
            {
                Name = new NLipsum.Core.Word().ToString(),
            };

            await this.DbContext.CityAreas.AddAsync(cityArea);
            await this.DbContext.SaveChangesAsync();
            this.DbContext.Entry<CityArea>(cityArea).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            return cityArea;
        }
    }
}
