namespace HealthHub.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using HealthHub.Data.Common.Repositories;
    using HealthHub.Data.Models;
    using HealthHub.Data.Models.Enums;
    using HealthHub.Web.ViewModels.Doctor;

    public class DoctorsService : IDoctorsService
    {
        private readonly IDeletableEntityRepository<Doctor> doctorsRepository;

        public DoctorsService(IDeletableEntityRepository<Doctor> doctorsRepository)
        {
            this.doctorsRepository = doctorsRepository;
        }

        public IEnumerable<DoctorsSummaryViewModel> GetAll()
        {
            var allDoctors = this.doctorsRepository.All()
                .OrderBy(d => d.ScheduledAppointments.Select(sa => sa.Rating.Value).Average())
                .Select(d => new DoctorsSummaryViewModel
                {
                    Id = d.Id,
                    FirstName = d.FirstName,
                    LastName = d.LastName,
                    ImageUrl = d.ImageUrl,
                    Clinic = d.Clinic.Name,
                    Specialty = d.Specialty.Name,
                })
                .ToList();

            return allDoctors;
        }

        public IEnumerable<DoctorsSummaryViewModel> GetAllSearched(string specialty, string cityArea, string name)
        {
            var allDoctors = this.doctorsRepository.All()
                .Where(d => (d.Specialty.Name == specialty && d.Clinic.Area.Name == cityArea) || d.FirstName.ToLower().Contains(name.ToLower()) || d.LastName.ToLower().Contains(name.ToLower()))
                .OrderBy(d => d.ScheduledAppointments.Select(sa => sa.Rating.Value).Average())
                .Select(d => new DoctorsSummaryViewModel
                {
                    Id = d.Id,
                    FirstName = d.FirstName,
                    LastName = d.LastName,
                    ImageUrl = d.ImageUrl,
                    Clinic = d.Clinic.Name,
                    Specialty = d.Specialty.Name,
                })
                .ToList();

            return allDoctors;
        }

        public DoctorsViewModel GetById(string doctorId)
        {
            var currentDoctor = this.doctorsRepository.All()
                .Where(d => d.Id == doctorId)
                .Select(d => new DoctorsViewModel
                {
                    Id = d.Id,
                    FirstName = d.FirstName,
                    LastName = d.LastName,
                    Age = DateTime.Now.Year - d.DateOfBirth.Year,
                    PhoneNumber = d.PhoneNumber,
                    ImageUrl = d.ImageUrl,
                    Clinic = d.Clinic.Name,
                    Specialty = d.Specialty.Name,
                    YearsOFExperience = d.YearsOFExperience,
                    WorksWithChildren = d.WorksWithChildren ? "Yes" : "No",
                    OnlineConsultation = d.OnlineConsultation ? "Yes" : "No",
                    AverageRating = d.ScheduledAppointments
                                    .Where(sa => sa.AppointmentStatus == AppointmentStatus.Completed && sa.HasBeenVoted == true)
                                    .Select(sa => sa.Rating.Value).Average(),
                    RatingCount = d.ScheduledAppointments
                                    .Where(sa => sa.AppointmentStatus == AppointmentStatus.Completed && sa.HasBeenVoted == true)
                                    .Count(),
                })
                .FirstOrDefault();

            return currentDoctor;
        }
    }
}
