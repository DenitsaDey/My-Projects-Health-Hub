namespace HealthHub.Services.Data.Ratings
{
    using System.Threading.Tasks;

    public interface IRatingsService
    {
        Task SetRatingAsync(string appointmentId, string patientId, int value, string additionalComments);

        double GetDoctorAverageRating(string doctorId);

        double GetClinicAverageRating(string clinicId);
    }
}
