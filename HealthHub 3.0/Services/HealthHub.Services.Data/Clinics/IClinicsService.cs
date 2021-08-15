namespace HealthHub.Services.Data.Clinics
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using HealthHub.Data.Models.Enums;
    using HealthHub.Web.ViewModels.Clinics;

    public interface IClinicsService
    {
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
    }
}
