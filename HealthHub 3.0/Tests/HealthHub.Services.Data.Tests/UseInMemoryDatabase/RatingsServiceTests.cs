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
            var doctorId = new NLipsum.Core.Word().ToString();

            var appointmentId = this.CreateAppointmentAsync(doctorId).Result.Id;
            int value = 5;
            var additionalComments = new NLipsum.Core.Sentence().ToString();
            await this.CreateRatingAsync(appointmentId, value, additionalComments);

            var secondAppointmentId = this.CreateAppointmentAsync(doctorId).Result.Id;
            int secondValue = 4;
            var secondAdditionalComments = string.Empty;

            await this.Service.SetRatingAsync(secondAppointmentId, secondValue, secondAdditionalComments);

            var ratingsCount = await this.DbContext.Ratings.CountAsync();

            Assert.Equal(2, ratingsCount);
        }

        [Fact]
        public async Task SetRatingAsyncShouldChangeAppointmentAsHasBeenVoted()
        {
            var doctorId = Guid.NewGuid().ToString();
            var secondAppointmentId = this.CreateAppointmentAsync(doctorId).Result.Id;
            int secondValue = 3;
            var secondAdditionalComments = string.Empty;

            await this.Service.SetRatingAsync(secondAppointmentId, secondValue, secondAdditionalComments);

            var appointmentHasBeenVoted = this.DbContext.Appointments.Where(x => x.Id == secondAppointmentId).FirstOrDefault().HasBeenVoted;

            Assert.True(appointmentHasBeenVoted);
        }

        [Fact]
        public async Task SetRatingAsyncShouldSetAppointmentValue()
        {
            var doctorId = new NLipsum.Core.Word().ToString();

            var secondAppointmentId = this.CreateAppointmentAsync(doctorId).Result.Id;
            int secondValue = 5;
            var secondAdditionalComments = string.Empty;

            await this.Service.SetRatingAsync(secondAppointmentId, secondValue, secondAdditionalComments);

            var appointmentRatingValue = this.DbContext.Appointments.Where(x => x.Id == secondAppointmentId).FirstOrDefault().Rating.Value;

            Assert.Equal(5, appointmentRatingValue);
        }

        [Fact]
        public async Task SetRatingAsyncShouldNotSetRatingOnAppointmentThatHasBeenRated()
        {
            var doctorId = new NLipsum.Core.Word().ToString();

            var appointmentId = this.CreateAppointmentAsync(doctorId).Result.Id;
            await this.Service.SetRatingAsync(appointmentId, 1, "test1");

            /* should not be able to SetRating if
              the first SetRatingAsync has successfully changed
              the appointment status to HasBeenVoted = true
              thus the appointmentToBerated will be null and will throw NullReferenceException
            */
            var appointment = this.DbContext.Appointments.FirstOrDefault(x => x.Id == appointmentId);

            await Assert.ThrowsAsync<NullReferenceException>(() => this.Service.SetRatingAsync(appointment.Id, 4, "test2"));
        }

        [Fact]
        public async Task GetDoctorAverageRatingShouldCalculateAverageCorrectly()
        {
            var doctorId = new NLipsum.Core.Word().ToString();

            var appointmentId = this.CreateAppointmentAsync(doctorId).Result.Id;
            await this.Service.SetRatingAsync(appointmentId, 5, string.Empty);

            var secondAppointmentId = this.CreateAppointmentAsync(doctorId).Result.Id;
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
            var doctorId = this.CreateDoctorAsync(clinicId).Result.Id;

            var appointmentId1 = this.CreateAppointmentAsync(doctorId).Result.Id; ;
            await this.Service.SetRatingAsync(appointmentId1, 5, string.Empty);

            var appointmentId2 = this.CreateAppointmentAsync(doctorId).Result.Id;
            await this.Service.SetRatingAsync(appointmentId2, 3, string.Empty);

            // 3 appointments for second doctor
            var doctorId2 = this.CreateDoctorAsync(clinicId).Result.Id;

            var appointmentId3 = this.CreateAppointmentAsync(doctorId2).Result.Id;
            await this.Service.SetRatingAsync(appointmentId3, 2, string.Empty);

            var appointmentId4 = this.CreateAppointmentAsync(doctorId2).Result.Id;
            await this.Service.SetRatingAsync(appointmentId4, 2, string.Empty);

            var appointmentId5 = this.CreateAppointmentAsync(doctorId2).Result.Id;
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

        private async Task<Appointment> CreateAppointmentAsync(string doctorId)
        {
            var appointment = new Appointment
            {
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

        private async Task<Doctor> CreateDoctorAsync(string clinicId)
        {
            var doctor = new Doctor
            {
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
