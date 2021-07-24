namespace HealthHub.Services.Data
{
    using System.Collections.Generic;

    using HealthHub.Web.ViewModels;

    public interface ISpecialtiesService
    {
        IEnumerable<SpecialtyViewModel> GetAllSpecialties();

        IEnumerable<string> GetAllSpecialtiesNames();
    }
}
