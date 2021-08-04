namespace HealthHub.Services.Data
{
    using System.Collections.Generic;

    using HealthHub.Web.ViewModels;

    public interface IServicesService
    {
        IEnumerable<T> GetAllServices<T>();
    }
}
