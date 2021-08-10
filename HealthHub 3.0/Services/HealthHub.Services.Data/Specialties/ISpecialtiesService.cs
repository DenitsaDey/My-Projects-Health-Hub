namespace HealthHub.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using HealthHub.Web.ViewModels;

    public interface ISpecialtiesService
    {
        Task<IEnumerable<T>> GetAllSpecialtiesAsync<T>();

        IEnumerable<string> GetAllSpecialtiesNames();

        Task<T> GetByIdAsync<T>(string specialtyId);

        Task AddAsync(string name);

        IEnumerable<string> GetAllSpecialtiesIds();
    }
}
