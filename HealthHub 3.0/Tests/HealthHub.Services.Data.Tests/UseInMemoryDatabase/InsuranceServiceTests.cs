namespace HealthHub.Services.Data.Tests.UseInMemoryDatabase
{
    using Microsoft.Extensions.DependencyInjection;

    public class InsuranceServiceTests : BaseServiceTests
    {
        private IInsuranceService Service => this.ServiceProvider.GetRequiredService<IInsuranceService>();

        /*  IEnumerable<T> GetAllInsuranceCompanies<T>();

            IEnumerable<T> GetAllByClinicId<T>(string clinicId);
         */
    }
}
