namespace HealthHub.Services.Data.Tests.UseInMemoryDatabase
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using HealthHub.Data.Models;
    using HealthHub.Data.Models.Enums;
    using HealthHub.Web.ViewModels.Doctor;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class DoctorsServiceTests : BaseServiceTests
    {
        private IDoctorsService Service => this.ServiceProvider.GetRequiredService<IDoctorsService>();

        /*
         Task<DoctorsFilterViewModel> GetAllSearchedAsync(
            string specialtyId,
            string cityAreaId,
            string insuranceId,
            bool worksWithChilderen,
            bool onlineConsultations,
            Gender gender,
            SearchSorting sorting,
            string clinicId,
            string searchName,
            int pageNumber,
            int itemsPerPage);

        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetAllWithDeleted<T>();

        Task<string> AddAsync(DoctorInputModel input); - done

        bool DoctorExists(string doctorId); - done

        Task DeleteAsync(string id); - done

        Task UpdateAsync(string id, DoctorEditInputModel input); - done

        Task<T> GetByIdAsync<T>(string doctorId);

        string GetIdByMostAppointments(); - done

        T GetByAppointment<T>(string appointmentId);

        Task<IEnumerable<T>> GetByClinicAsync<T>(string clinicId);
         */
        [Fact]
        public async Task AddAsyncShouldAddCorrectly()
        {
            await this.CreateDoctorAsync();

            var input = new DoctorInputModel
            {
                FirstName = new NLipsum.Core.Word().ToString(),
                LastName = new NLipsum.Core.Word().ToString(),
                ImageUrl = new NLipsum.Core.Sentence().ToString(),
                ClinicId = Guid.NewGuid().ToString(),
                SpecialtyId = Guid.NewGuid().ToString(),
                YearsOFExperience = 1,
            };

            await this.Service.AddAsync(input);

            var doctorsCount = await this.DbContext.Doctors.CountAsync();

            Assert.Equal(2, doctorsCount);
        }

        [Fact]
        public async Task UpdateAsyncShouldUpdateCorrectly()
        {
            var doctorId = this.CreateDoctorAsync().Result.Id;

            var input = new DoctorEditInputModel
            {
                FirstName = "Updated FirstName",
                LastName = "Updated LastName",
                ImageUrl = new NLipsum.Core.Sentence().ToString(),
                ClinicId = Guid.NewGuid().ToString(),
                SpecialtyId = Guid.NewGuid().ToString(),
                YearsOFExperience = 15,
            };

            await this.Service.UpdateAsync(doctorId, input);

            var resultFirstName = this.DbContext.Doctors.FirstOrDefault(d => d.Id == doctorId).FirstName;
            var resultLastName = this.DbContext.Doctors.FirstOrDefault(d => d.Id == doctorId).LastName;
            var resultExperience = this.DbContext.Doctors.FirstOrDefault(d => d.Id == doctorId).YearsOFExperience;

            Assert.Equal("Updated FirstName", resultFirstName);
            Assert.Equal("Updated LastName", resultLastName);
            Assert.Equal(15, resultExperience);
        }

        [Fact]
        public async Task DeleteAsyncShouldDeleteCorrectly()
        {
            var doctor = await this.CreateDoctorAsync();

            await this.Service.DeleteAsync(doctor.Id);

            var doctorsCount = this.DbContext.Doctors.Where(x => !x.IsDeleted).ToArray().Count();
            var deletedDoctor = await this.DbContext.Doctors.FirstOrDefaultAsync(x => x.Id == doctor.Id);
            Assert.Equal(0, doctorsCount);
            Assert.Null(deletedDoctor);
        }

        [Fact]
        public void DoctorExistsShouldReturnCorrectBool()
        {
            var doctorId = this.CreateDoctorAsync().Result.Id;

            var result1 = this.Service.DoctorExists(doctorId);
            var result2 = this.Service.DoctorExists(Guid.NewGuid().ToString());

            Assert.True(result1);
            Assert.False(result2);
        }

        [Fact]
        public void GetIdByMostAppointmentsShouldReturnCorrectDoctorId()
        {
            var doctorId1 = this.CreateDoctorAsync().Result.Id;
            var doctorId2 = this.CreateDoctorAsync().Result.Id;

            var appointment1 = this.CreateAppointmentAsync(doctorId1);
            var appointment2 = this.CreateAppointmentAsync(doctorId1);
            var appointment3 = this.CreateAppointmentAsync(doctorId1);

            var appointment4 = this.CreateAppointmentAsync(doctorId2);

            var result = this.Service.GetIdByMostAppointments();

            Assert.Equal(doctorId1, result);
        }

        private async Task<Doctor> CreateDoctorAsync()
        {
            var doctor = new Doctor
            {
                FirstName = new NLipsum.Core.Sentence().ToString(),
                LastName = new NLipsum.Core.Sentence().ToString(),
                ImageUrl = new NLipsum.Core.Sentence().ToString(),
                ClinicId = Guid.NewGuid().ToString(),
                SpecialtyId = Guid.NewGuid().ToString(),
                YearsOFExperience = 1,
            };

            await this.DbContext.Doctors.AddAsync(doctor);
            await this.DbContext.SaveChangesAsync();
            this.DbContext.Entry<Doctor>(doctor).State = EntityState.Detached;
            return doctor;
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
    }
}
