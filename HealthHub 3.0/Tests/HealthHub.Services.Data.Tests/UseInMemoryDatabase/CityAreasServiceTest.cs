namespace HealthHub.Services.Data.Tests.UseInMemoryDatabase
{
    using System.Linq;
    using System.Threading.Tasks;

    using HealthHub.Data.Models;
    using HealthHub.Web.ViewModels;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class CityAreasServiceTest : BaseServiceTests
    {
        private ICityAreasService Service => this.ServiceProvider.GetRequiredService<ICityAreasService>();

        /* AddAsync - done
         * GetAllCityAreasAsync - done
         * GetByIdAsync - done
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

        [Fact]
        public async Task GetByIdAsyncShouldReturnTheCorrectModel()
        {
            var cityArea = await this.CreateCityAreaAsync();
            var cityAreaId = cityArea.Id;
            var model = new CityAreasViewModel()
            {
                Id = cityAreaId,
                Name = cityArea.Name,
            };

            var resultModel = await this.Service.GetByIdAsync<CityAreasViewModel>(cityAreaId);

            Assert.Equal(model.Id, resultModel.Id);
            Assert.Equal(model.Name, resultModel.Name);
        }

        [Fact]
        public async Task GetAllCityAreasAsyncShouldReturnTheCorrectModelCollection()
        {
            var cityArea1 = await this.CreateCityAreaAsync();
            var cityAreaId1 = cityArea1.Id;
            var model1 = new CityAreasViewModel()
            {
                Id = cityAreaId1,
                Name = cityArea1.Name,
            };

            var cityArea2 = await this.CreateCityAreaAsync();
            var cityAreaId2 = cityArea2.Id;
            var model2 = new CityAreasViewModel()
            {
                Id = cityAreaId2,
                Name = cityArea2.Name,
            };

            var resultModelCollection = await this.Service.GetAllCityAreasAsync<CityAreasViewModel>();

            // first and last bellow may need to be switched around as the method is async
            Assert.Equal(model1.Id, resultModelCollection.First().Id);
            Assert.Equal(model1.Name, resultModelCollection.First().Name);
            Assert.Equal(model2.Id, resultModelCollection.Last().Id);
            Assert.Equal(model2.Name, resultModelCollection.Last().Name);
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
