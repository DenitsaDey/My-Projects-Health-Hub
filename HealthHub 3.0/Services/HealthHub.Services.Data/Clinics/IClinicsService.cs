namespace HealthHub.Services.Data.Clinics
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using HealthHub.Web.ViewModels.Clinics;

    public interface IClinicsService
    {
        Task AddAsync(ClinicInputModel input);

        IEnumerable<ClinicViewModel> GetAllClinics();

        IEnumerable<string> GetAllClinicsNames();

        ClinicViewModel GetById(string clinicId);

    }
}
