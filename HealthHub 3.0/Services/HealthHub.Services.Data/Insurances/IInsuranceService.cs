namespace HealthHub.Services.Data
{
    using System.Collections.Generic;

    using HealthHub.Web.ViewModels;

    public interface IInsuranceService
    {
        IEnumerable<T> GetAllInsuranceCompanies<T>();
    }
}
