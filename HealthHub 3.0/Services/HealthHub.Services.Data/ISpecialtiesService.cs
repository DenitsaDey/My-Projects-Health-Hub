namespace HealthHub.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using HealthHub.Web.ViewModels;

    public interface ISpecialtiesService
    {
        Task<IEnumerable<SpecialtyViewModel>> GetAllSpecialtiesAsync();

        IEnumerable<string> GetAllSpecialtiesNames();

        Task<SpecialtyViewModel> GetByIdAsync(string specialtyId);

        Task AddAsync(string name);
    }
}
