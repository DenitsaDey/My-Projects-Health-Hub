namespace HealthHub.Services.Data
{
    using System.Collections.Generic;

    using HealthHub.Web.ViewModels.Home;

    public interface ICityAreasService
    {
        IEnumerable<IndexCityAreaViewModel> GetAllCityAreas();
    }
}
