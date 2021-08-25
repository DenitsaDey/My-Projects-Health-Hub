namespace HealthHub.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HealthHub.Data.Common.Repositories;
    using HealthHub.Data.Models;

    public class RatingPopulatingService : IRatingPopulatingService
    {
        private readonly IDeletableEntityRepository<Rating> ratingRepository;
        private readonly IDeletableEntityRepository<Appointment> appointmentRepository;

        public RatingPopulatingService(
            IDeletableEntityRepository<Rating> ratingRepository,
            IDeletableEntityRepository<Appointment> appointmentRepository)
        {
            this.ratingRepository = ratingRepository;
            this.appointmentRepository = appointmentRepository;
        }

        public async Task ImportRatings()
        {
            if (this.ratingRepository.All().Any())
            {
                return;
            }

            var ratings = new List<Rating>()
            {
                new Rating
            {
                Value = 5,
                AppointmentId = this.appointmentRepository.All().Where(a => a.Message == "test voting 1").FirstOrDefault().Id,
                AdditionalComments = "excellent service",
            },
                new Rating
            {
                Value = 4,
                AppointmentId = this.appointmentRepository.All().Where(a => a.Message == "test voting 2").FirstOrDefault().Id,
                AdditionalComments = "very good service",
            },
                new Rating
            {
                Value = 4,
                AppointmentId = this.appointmentRepository.All().Where(a => a.Message == "test voting 3").FirstOrDefault().Id,
                AdditionalComments = "very good service",
            },
                new Rating
            {
                Id = Guid.NewGuid().ToString(),
                Value = 2,
                AppointmentId = this.appointmentRepository.All().Where(a => a.Message == "test voting 5").FirstOrDefault().Id,
                AdditionalComments = "average service",
            },
                new Rating
            {
                Value = 5,
                AppointmentId = this.appointmentRepository.All().Where(a => a.Message == "test voting 6").FirstOrDefault().Id,
                AdditionalComments = "excellent service",
            },
            };

            foreach (var rating in ratings)
            {
                await this.ratingRepository.AddAsync(rating);
                await this.ratingRepository.SaveChangesAsync();

                var currentAppointment = this.appointmentRepository.All()
                .FirstOrDefault(a => a.Id == rating.AppointmentId);
                currentAppointment.HasBeenVoted = true;
                currentAppointment.RatingId = rating.Id;

                await this.appointmentRepository.SaveChangesAsync();
            }
        }
    }
}
