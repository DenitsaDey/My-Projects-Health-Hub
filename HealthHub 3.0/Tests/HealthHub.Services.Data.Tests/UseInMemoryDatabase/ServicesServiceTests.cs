namespace HealthHub.Services.Data.Tests.UseInMemoryDatabase
{
    using Microsoft.Extensions.DependencyInjection;

    public class ServicesServiceTests : BaseServiceTests
    {
        private IServicesService Service => this.ServiceProvider.GetRequiredService<IServicesService>();

        /* IEnumerable<T> GetAllServices<T>();
         */
    }
}
