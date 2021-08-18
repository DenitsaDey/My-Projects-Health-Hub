namespace HealthHub.Services.Data.Ratings
{
    using System.Linq;
    using System.Threading.Tasks;

    using HealthHub.Data.Common.Repositories;
    using HealthHub.Data.Models;
    using HealthHub.Data.Models.Enums;

    public class RatingsService : IRatingsService
    {
        private readonly IDeletableEntityRepository<Rating> ratingRepository;
        private readonly IDeletableEntityRepository<Appointment> appointmentRepository;
        private readonly IDeletableEntityRepository<Doctor> doctorRepository;
        private readonly IDeletableEntityRepository<Clinic> clinicRepository;

        public RatingsService(
            IDeletableEntityRepository<Rating> ratingRepository,
            IDeletableEntityRepository<Appointment> appointmentRepository,
            IDeletableEntityRepository<Doctor> doctorRepository,
            IDeletableEntityRepository<Clinic> clinicRepository)
        {
            this.ratingRepository = ratingRepository;
            this.appointmentRepository = appointmentRepository;
            this.doctorRepository = doctorRepository;
            this.clinicRepository = clinicRepository;
        }

        public async Task SetRatingAsync(string appointmentId, int value, string additionalComments)
        {
            // in case of existig rating from RatingSeeder
            var ratingToBeSet = this.ratingRepository.All()
                .FirstOrDefault(r => r.AppointmentId == appointmentId
                && r.Appointment.AppointmentStatus == AppointmentStatus.Completed
                && !r.Appointment.HasBeenVoted);

            var appointmentToBeRated = this.appointmentRepository.All()
                .FirstOrDefault(x => x.Id == appointmentId
                && x.AppointmentStatus == AppointmentStatus.Completed
                && !x.HasBeenVoted);

            // in normal app use rating will be only created at this point
            if (ratingToBeSet == null && appointmentToBeRated != null)
            {
                ratingToBeSet = new Rating
                {
                    AppointmentId = appointmentId,
                    Appointment = appointmentToBeRated,
                    Value = value,
                    AdditionalComments = additionalComments,
                };

                await this.ratingRepository.AddAsync(ratingToBeSet);
                await this.ratingRepository.SaveChangesAsync();
            }

            var ratingId = this.ratingRepository.All()
                .FirstOrDefault(r => r.AppointmentId == appointmentId
                && r.Appointment.AppointmentStatus == AppointmentStatus.Completed
                && !r.Appointment.HasBeenVoted).Id;

            appointmentToBeRated.HasBeenVoted = true;
            appointmentToBeRated.RatingId = ratingId;
            appointmentToBeRated.Rating = ratingToBeSet;

            await this.appointmentRepository.SaveChangesAsync();
        }

        public double GetDoctorAverageRating(string doctorId)
        {
            return this.ratingRepository.All()
                .Where(r => r.Appointment.DoctorId == doctorId && r.Appointment.HasBeenVoted)
                .Count() == 0 ? 0 :
                this.ratingRepository.All()
                .Where(r => r.Appointment.DoctorId == doctorId && r.Appointment.HasBeenVoted).Average(r => r.Value);
        }

        public double GetClinicAverageRating(string clinicId)
        {
            return this.ratingRepository.All()
                .Where(r => r.Appointment.Doctor.ClinicId == clinicId && r.Appointment.HasBeenVoted)
                .Count() == 0 ? 0 :
                this.ratingRepository.All()
                .Where(r => r.Appointment.Doctor.ClinicId == clinicId && r.Appointment.HasBeenVoted).Average(r => r.Value);
        }
    }
}
