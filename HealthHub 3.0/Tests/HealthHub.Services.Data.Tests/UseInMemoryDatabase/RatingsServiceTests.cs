namespace HealthHub.Services.Data.Tests.UseInMemoryDatabase
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using HealthHub.Data.Models;
    using HealthHub.Data.Models.Enums;
    using HealthHub.Services.Data.Ratings;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class RatingsServiceTests : BaseServiceTests
    {
        private IRatingsService Service => this.ServiceProvider.GetRequiredService<IRatingsService>();

        /*  Task SetRatingAsync(string appointmentId, int value, string additionalComments); - done

            double GetDoctorAverageRating(string doctorId); - done

            double GetClinicAverageRating(string clinicId); - done
         */

        [Fact]
        public async Task SetRatingAsyncShouldAddCorrectly()
        {
            var appointmentId = new NLipsum.Core.Word().ToString();
            int value = 5;
            var additionalComments = new NLipsum.Core.Sentence().ToString();
            await this.CreateRatingAsync(appointmentId, value, additionalComments);

            var secondAppointmentId = new NLipsum.Core.Word().ToString();
            int secondValue = 4;
            var secondAdditionalComments = string.Empty;

            await this.Service.SetRatingAsync(secondAppointmentId, secondValue, secondAdditionalComments);

            var ratingsCount = await this.DbContext.Ratings.CountAsync();

            Assert.Equal(2, ratingsCount);
        }

        [Fact]
        public async Task SetRatingAsyncShouldChangeAppointmentAsHasBeenVoted()
        {
            var secondAppointmentId = new NLipsum.Core.Word().ToString();
            int secondValue = 3;
            var secondAdditionalComments = string.Empty;

            await this.Service.SetRatingAsync(secondAppointmentId, secondValue, secondAdditionalComments);

            var appointmentHasBeenVoted = this.DbContext.Appointments.Where(x => x.Id == secondAppointmentId).FirstOrDefault().HasBeenVoted;

            Assert.True(appointmentHasBeenVoted);
        }

        [Fact]
        public async Task SetRatingAsyncShouldSetAppointmentValue()
        {
            var secondAppointmentId = new NLipsum.Core.Word().ToString();
            int secondValue = 5;
            var secondAdditionalComments = string.Empty;

            await this.Service.SetRatingAsync(secondAppointmentId, secondValue, secondAdditionalComments);

            var appointmentRatingValue = this.DbContext.Appointments.Where(x => x.Id == secondAppointmentId).FirstOrDefault().Rating.Value;

            Assert.Equal(5, appointmentRatingValue);
        }

        [Fact]
        public async Task SetRatingAsyncShouldNotSetRatingOnAppointmentThatHasBeenRated()
        {
            var appointmentId = new NLipsum.Core.Word().ToString();
            int value = 1;
            var additionalComments = new NLipsum.Core.Sentence().ToString();
            await this.CreateRatingAsync(appointmentId, value, additionalComments);

            var secondAppointmentId = new NLipsum.Core.Word().ToString();
            int secondValue = 4;
            var secondAdditionalComments = string.Empty;

            await this.Service.SetRatingAsync(secondAdditionalComments, secondValue, secondAdditionalComments);

            var appointmentRatingValue = this.DbContext.Appointments.Where(x => x.Id == secondAppointmentId).FirstOrDefault().Rating.Value;

            Assert.Equal(1, appointmentRatingValue);
        }

        [Fact]
        public async Task GetDoctorAverageRatingShouldCalculateAverageCorrectly()
        {
            var doctorId = new NLipsum.Core.Word().ToString();

            var appointmentId = new NLipsum.Core.Word().ToString();
            await this.CreateAppointmentAsync(appointmentId, doctorId);
            await this.Service.SetRatingAsync(appointmentId, 5, string.Empty);

            var secondAppointmentId = new NLipsum.Core.Word().ToString();
            await this.CreateAppointmentAsync(secondAppointmentId, doctorId);
            await this.Service.SetRatingAsync(secondAppointmentId, 3, string.Empty);

            var actualResult = this.DbContext.Ratings
                                .Where(r => r.Appointment.DoctorId == doctorId && r.Appointment.HasBeenVoted).Average(r => r.Value);

            Assert.Equal(4, actualResult);
        }

        [Fact]
        public async Task GetClinicAverageRatingShouldCalculateAverageCorrectly()
        {
            // clinic with 2 doctors
            var clinicId = new NLipsum.Core.Word().ToString();

            // 2 appointments for first doctor
            var doctorId = new NLipsum.Core.Word().ToString();
            await this.CreateDoctorAsync(clinicId, doctorId);

            var appointmentId = new NLipsum.Core.Word().ToString();
            await this.CreateAppointmentAsync(appointmentId, doctorId);
            await this.Service.SetRatingAsync(appointmentId, 5, string.Empty);

            var secondAppointmentId = new NLipsum.Core.Word().ToString();
            await this.CreateAppointmentAsync(secondAppointmentId, doctorId);
            await this.Service.SetRatingAsync(secondAppointmentId, 3, string.Empty);

            // 3 appointments for second doctor
            var doctorId2 = new NLipsum.Core.Word().ToString();
            await this.CreateDoctorAsync(clinicId, doctorId2);

            var appointmentId3 = new NLipsum.Core.Word().ToString();
            await this.CreateAppointmentAsync(appointmentId3, doctorId2);
            await this.Service.SetRatingAsync(appointmentId3, 2, string.Empty);

            var appointmentId4 = new NLipsum.Core.Word().ToString();
            await this.CreateAppointmentAsync(appointmentId4, doctorId2);
            await this.Service.SetRatingAsync(appointmentId4, 2, string.Empty);

            var appointmentId5 = new NLipsum.Core.Word().ToString();
            await this.CreateAppointmentAsync(appointmentId5, doctorId2);
            await this.Service.SetRatingAsync(appointmentId5, 3, string.Empty);

            var actualResult = this.DbContext.Ratings
                                .Where(r => r.Appointment.Doctor.ClinicId == clinicId
                                && r.Appointment.HasBeenVoted).Average(r => r.Value);
            Assert.Equal(3, actualResult);
        }

        private async Task<Rating> CreateRatingAsync(string appointmentId, int value, string additionalComments)
        {
            var rating = new Rating
            {
                AppointmentId = appointmentId,
                Value = value,
                AdditionalComments = additionalComments,
            };

            await this.DbContext.Ratings.AddAsync(rating);
            await this.DbContext.SaveChangesAsync();
            this.DbContext.Entry<Rating>(rating).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            return rating;
        }

        private async Task<Appointment> CreateAppointmentAsync(string appointmentId, string doctorId)
        {
            var appointment = new Appointment
            {
                Id = appointmentId,
                DoctorId = doctorId,
                PatientId = new NLipsum.Core.Word().ToString(),
                ServiceId = new NLipsum.Core.Word().ToString(),
                AppointmentTime = DateTime.UtcNow.AddDays(-2),
                AppointmentStatus = AppointmentStatus.Completed,
                HasBeenVoted = false,
            };

            await this.DbContext.Appointments.AddAsync(appointment);
            await this.DbContext.SaveChangesAsync();
            this.DbContext.Entry<Appointment>(appointment).State = EntityState.Detached;
            return appointment;
        }

        private async Task<Doctor> CreateDoctorAsync(string clinicId, string doctorId)
        {
            var doctor = new Doctor
            {
                Id = doctorId,
                FirstName = new NLipsum.Core.Word().ToString(),
                LastName = new NLipsum.Core.Word().ToString(),
                ImageUrl = new NLipsum.Core.Sentence().ToString(),
                ClinicId = clinicId,
                SpecialtyId = new NLipsum.Core.Word().ToString(),
                YearsOFExperience = 5,
            };

            await this.DbContext.Doctors.AddAsync(doctor);
            await this.DbContext.SaveChangesAsync();
            this.DbContext.Entry<Doctor>(doctor).State = EntityState.Detached;
            return doctor;
        }
    }
}
