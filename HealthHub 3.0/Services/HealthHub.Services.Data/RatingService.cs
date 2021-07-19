namespace HealthHub.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using HealthHub.Data.Common.Repositories;
    using HealthHub.Data.Models;

    public class RatingService : IRatingService
    {
        private readonly IDeletableEntityRepository<Doctor> doctorRepository;
        private readonly IDeletableEntityRepository<Clinic> clinicRepository;
        private readonly IDeletableEntityRepository<Rating> ratingRepository;
        private readonly IDeletableEntityRepository<Appointment> appointmentRepository;

        public RatingService(
            IDeletableEntityRepository<Doctor> doctorRepository,
            IDeletableEntityRepository<Clinic> clinicRepository,
            IDeletableEntityRepository<Rating> ratingRepository,
            IDeletableEntityRepository<Appointment> appointmentRepository)
        {
            this.doctorRepository = doctorRepository;
            this.ratingRepository = ratingRepository;
            this.appointmentRepository = appointmentRepository;
            this.clinicRepository = clinicRepository;
        }

        public double GetDoctorAverageRating(string doctorId)
        {
            return this.doctorRepository.All()
                .Where(d => d.Id == doctorId && d.ScheduledAppointments.Where(a => a.HasBeenVoted).Any())
                .Average(d => d.ScheduledAppointments.Average(sa => sa.Rating.Value));
        }

        public double GetClinicAverageRating(string clinicId)
        {
            return this.clinicRepository.All()
                .Where(c => c.Id == clinicId)
                .Average(c => c.MedicalStaff
                            .Average(ms => ms.ScheduledAppointments
                                                        .Average(sa => sa.Rating.Value)));
        }

        public async Task SetRatungAsync(string appointmentId, int value, string additionalComments)
        {
            var ratingToBeSet = this.ratingRepository.All()
                .FirstOrDefault(r => r.AppointmentId == appointmentId && !r.Appointment.HasBeenVoted);

            if (ratingToBeSet == null)
            {
                ratingToBeSet = new Rating
                {
                    AppointmentId = appointmentId,
                };

                await this.ratingRepository.AddAsync(ratingToBeSet);
            }

            ratingToBeSet.Value = value;

            if (additionalComments != string.Empty)
            {
                ratingToBeSet.AdditionalComments = additionalComments;
            }

            this.appointmentRepository.All()
                .FirstOrDefault(a => a.Id == appointmentId)
                .HasBeenVoted = true;

            await this.ratingRepository.SaveChangesAsync();
            await this.appointmentRepository.SaveChangesAsync();
        }
    }
}
