namespace HealthHub.Services.Data.Clinics
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using HealthHub.Web.ViewModels.Clinics;

    public interface IClinicsService
    {
        Task AddAsync(ClinicInputModel input);

        Task<IEnumerable<ClinicViewModel>> GetAllClinicsAsync();

        IEnumerable<string> GetAllClinicsNames();

        Task<ClinicViewModel> GetByIdAsync(string clinicId);
    }
}
