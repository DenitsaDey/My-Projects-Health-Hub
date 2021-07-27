namespace HealthHub.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using HealthHub.Web.ViewModels;

    public interface ICityAreasService
    {
        Task AddAsync(string name);

        Task<IEnumerable<CityAreasViewModel>> GetAllCityAreasAsync();

        Task<CityAreasViewModel> GetByIdAsync(string cityAreaId);
    }
}
