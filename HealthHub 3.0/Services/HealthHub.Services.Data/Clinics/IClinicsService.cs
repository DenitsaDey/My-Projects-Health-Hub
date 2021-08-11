namespace HealthHub.Services.Data.Clinics
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using HealthHub.Web.ViewModels.Clinics;

    public interface IClinicsService
    {
        Task AddAsync(ClinicInputModel input);

        IEnumerable<ClinicViewModel> GetAllClinics();

        IEnumerable<T> GetAllWithDeleted<T>();

        IEnumerable<string> GetAllClinicsNames();

        ClinicViewModel GetById(string clinicId);

        IEnumerable<string> GetAllClinicIds();

        Task UpdateAsync(string id, ClinicEditInputModel input);

        Task DeleteAsync(string id);

        bool ClinicExists(string id);
    }
}
