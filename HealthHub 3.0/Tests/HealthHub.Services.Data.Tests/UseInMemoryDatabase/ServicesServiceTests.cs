namespace HealthHub.Services.Data.Tests.UseInMemoryDatabase
{
    using System.Linq;
    using System.Threading.Tasks;

    using HealthHub.Data.Models;
    using HealthHub.Web.ViewModels;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class ServicesServiceTests : BaseServiceTests
    {
        private IServicesService Service => this.ServiceProvider.GetRequiredService<IServicesService>();

        /* IEnumerable<T> GetAllServices<T>(); - done
         */
        [Fact]
        public async Task GetAllServicesShouldReturnTheCorrectModelCollection()
        {
            var service1 = await this.CreateServiceAsync("My service");
            var serviceId1 = service1.Id;
            var model1 = new ServicesViewModel()
            {
                Id = serviceId1,
                Name = service1.Name,
            };

            var service2 = await this.CreateServiceAsync("Your service");
            var serviceId2 = service2.Id;
            var model2 = new ServicesViewModel()
            {
                Id = serviceId2,
                Name = service2.Name,
            };

            var resultModelCollection = this.Service.GetAllServices<ServicesViewModel>();

            Assert.Equal(model1.Id, resultModelCollection.First().Id);
            Assert.Equal(model1.Name, resultModelCollection.First().Name);
            Assert.Equal(model2.Id, resultModelCollection.Last().Id);
            Assert.Equal(model2.Name, resultModelCollection.Last().Name);
        }

        private async Task<Service> CreateServiceAsync(string name)
        {
            var service = new Service()
            {
                Name = name,
                Description = new NLipsum.Core.Sentence().ToString(),
            };

            await this.DbContext.Services.AddAsync(service);
            await this.DbContext.SaveChangesAsync();
            this.DbContext.Entry<Service>(service).State = EntityState.Detached;
            return service;
        }
    }
}
