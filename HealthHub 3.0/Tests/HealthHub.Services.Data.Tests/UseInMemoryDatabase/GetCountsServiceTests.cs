namespace HealthHub.Services.Data.Tests.UseInMemoryDatabase
{
    using HealthHub.Data.Models;
    using HealthHub.Data.Models.Enums;
    using HealthHub.Web.ViewModels;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Threading.Tasks;
    using Xunit;

    public class GetCountsServiceTests : BaseServiceTests
    {
        private IGetCountsService Service => this.ServiceProvider.GetRequiredService<IGetCountsService>();
        /* CountsViewModel GetCounts();
         */

        [Fact]
        public async Task GetCountsShouldReturnCorrectCounts()
        {
            // clinic with 2 doctors
            var clinicId = this.CreateClinicAsync().Result.Id;

            // 2 appointments for first doctor
            var doctorId = this.CreateDoctorAsync(clinicId).Result.Id;

            await this.CreateAppointmentAsync(doctorId);
            await this.CreateAppointmentAsync(doctorId);

            // 3 appointments for second doctor
            var doctorId2 = this.CreateDoctorAsync(clinicId).Result.Id;

            await this.CreateAppointmentAsync(doctorId2);
            await this.CreateAppointmentAsync(doctorId2);
            await this.CreateAppointmentAsync(doctorId2);

            // 2 specialties
            await this.CreateSpecialtyAsync("SpecialtyOne");
            await this.CreateSpecialtyAsync("SpecialtyTwo");

            var model = new CountsViewModel()
            {
                DoctorsCount = this.DbContext.Doctors.CountAsync().Result,
                ClinicsCount = this.DbContext.Clinics.CountAsync().Result,
                AppointmentsCount = this.DbContext.Appointments.CountAsync().Result,
                SpecialtiesCount = this.DbContext.Specialties.CountAsync().Result,
            };

            var resultModel = this.Service.GetCounts();

            Assert.Equal(model.DoctorsCount, resultModel.DoctorsCount);
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

        private async Task<Clinic> CreateClinicAsync()
        {
            var clinic = new Clinic
            {
                Name = new NLipsum.Core.Word().ToString(),
                Address = new NLipsum.Core.Word().ToString(),
                AreaId = Guid.NewGuid().ToString(),
                MapUrl = new NLipsum.Core.Sentence().ToString(),
            };

            await this.DbContext.Clinics.AddAsync(clinic);
            await this.DbContext.SaveChangesAsync();
            this.DbContext.Entry<Clinic>(clinic).State = EntityState.Detached;
            return clinic;
        }

        private async Task<Specialty> CreateSpecialtyAsync(string name)
        {
            var specialty = new Specialty
            {
                Name = name,
            };

            await this.DbContext.Specialties.AddAsync(specialty);
            await this.DbContext.SaveChangesAsync();
            this.DbContext.Entry<Specialty>(specialty).State = EntityState.Detached;
            return specialty;
        }
    }
}
