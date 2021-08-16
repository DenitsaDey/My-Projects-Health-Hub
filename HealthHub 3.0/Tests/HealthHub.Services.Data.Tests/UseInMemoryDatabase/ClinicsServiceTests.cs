namespace HealthHub.Services.Data.Tests.UseInMemoryDatabase
{
    using HealthHub.Services.Data.Clinics;
    using Microsoft.Extensions.DependencyInjection;

    public class ClinicsServiceTests : BaseServiceTests
    {
        private IClinicsService Service => this.ServiceProvider.GetRequiredService<IClinicsService>();

        /*
         Task AddAsync(ClinicInputModel input);

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

        IEnumerable<string> GetAllClinicsNames();

        ClinicViewModel GetById(string clinicId);

        IEnumerable<string> GetAllClinicIds();

        Task UpdateAsync(string id, ClinicEditInputModel input);

        Task DeleteAsync(string id);

        bool ClinicExists(string id);
         */
    }
}
