namespace HealthHub.Services
{
    using System.Threading.Tasks;

    public interface IRatingPopulatingService
    {
        Task ImportRatings();
    }
}
