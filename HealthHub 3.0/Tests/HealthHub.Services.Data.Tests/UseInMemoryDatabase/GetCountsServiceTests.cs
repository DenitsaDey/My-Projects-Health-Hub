namespace HealthHub.Services.Data.Tests.UseInMemoryDatabase
{
    using Microsoft.Extensions.DependencyInjection;

    public class GetCountsServiceTests : BaseServiceTests
    {
        private IGetCountsService Service => this.ServiceProvider.GetRequiredService<IGetCountsService>();
        /* CountsViewModel GetCounts();
         */
    }
}
