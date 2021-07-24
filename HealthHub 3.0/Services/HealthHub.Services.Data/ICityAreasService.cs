namespace HealthHub.Services.Data
{
    using System.Collections.Generic;

    using HealthHub.Web.ViewModels;

    public interface ICityAreasService
    {
        IEnumerable<CityAreasViewModel> GetAllCityAreas();
    }
}
