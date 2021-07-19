namespace HealthHub.Services.Data
{
    using System.Threading.Tasks;

    public interface IRatingService
    {
        Task SetRatungAsync(string appointmentId, int value);

        double GetDoctorAverageRating(string doctorId);

        double GetClinicAverageRating(string clinicId);
    }
}
