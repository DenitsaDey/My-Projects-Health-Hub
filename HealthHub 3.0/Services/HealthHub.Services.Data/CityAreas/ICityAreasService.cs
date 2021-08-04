namespace HealthHub.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICityAreasService
    {
        Task<string> AddAsync(string name);

        Task<IEnumerable<T>> GetAllCityAreasAsync<T>();

        Task<T> GetByIdAsync<T>(string cityAreaId);
    }
}
