namespace HealthHub.Services.Data
{
    using HealthHub.Services.Data.Models;
    using HealthHub.Web.ViewModels;

    public interface IGetCountsService
    {
        CountsViewModel GetCounts();
    }
}
