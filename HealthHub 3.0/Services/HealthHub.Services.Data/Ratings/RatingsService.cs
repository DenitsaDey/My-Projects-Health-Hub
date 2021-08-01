namespace HealthHub.Services.Data.Ratings
{
    using System.Linq;
    using System.Threading.Tasks;

    using HealthHub.Data.Common.Repositories;
    using HealthHub.Data.Models;

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

        public async Task SetRatingAsync(string appointmentId, string patientId, int value, string additionalComments)
        {
            var ratingToBeSet = this.ratingRepository.All()
                .FirstOrDefault(r => r.AppointmentId == appointmentId
                && r.PatientId == patientId
                && !r.Appointment.HasBeenVoted);

            if (ratingToBeSet == null)
            {
                ratingToBeSet = new Rating
                {
                    AppointmentId = appointmentId,
                    PatientId = patientId,
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

        public double GetDoctorAverageRating(string doctorId)
        {
            return this.ratingRepository.All()
                .Where(r => r.Appointment.DoctorId == doctorId && r.Appointment.HasBeenVoted)
                .Average(r => r.Value);

            //return this.doctorRepository.All()
            //    .Where(d => d.Id == doctorId && d.ScheduledAppointments.Where(a => a.HasBeenVoted).Any())
            //    .Average(d => d.ScheduledAppointments.Average(sa => sa.Rating.Value));
        }

        public double GetClinicAverageRating(string clinicId)
        {
            return this.ratingRepository.All()
                .Where(r => r.Appointment.Doctor.ClinicId == clinicId && r.Appointment.HasBeenVoted)
                .Average(r => r.Value);

            //return this.clinicRepository.All()
            //    .Where(c => c.Id == clinicId)
            //    .Average(c => c.MedicalStaff
            //                .Average(ms => ms.ScheduledAppointments
            //                                            .Average(sa => sa.Rating.Value)));
        }
    }
}
