namespace HealthHub.Services.Data
{
    using System.Collections.Generic;
    using HealthHub.Web.ViewModels;
    using HealthHub.Web.ViewModels.Home;

    public interface ISpecialtiesService
    {
        IEnumerable<SpecialtyViewModel> GetAllSpecialties();

        IEnumerable<string> GetAllSpecialtiesNames();
    }
}
