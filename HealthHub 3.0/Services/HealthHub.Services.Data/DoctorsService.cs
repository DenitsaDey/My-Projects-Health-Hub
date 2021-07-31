﻿namespace HealthHub.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using HealthHub.Data.Common.Repositories;
    using HealthHub.Data.Models;
    using HealthHub.Data.Models.Enums;
    using HealthHub.Web.ViewModels;
    using HealthHub.Web.ViewModels.Doctor;
    using Microsoft.EntityFrameworkCore;

    public class DoctorsService : IDoctorsService
    {
        private readonly IDeletableEntityRepository<Doctor> doctorsRepository;
        private readonly IDeletableEntityRepository<Service> servicesRepository;

        public DoctorsService(
            IDeletableEntityRepository<Doctor> doctorsRepository,
            IDeletableEntityRepository<Service> servicesRepository)
        {
            this.doctorsRepository = doctorsRepository;
            this.servicesRepository = servicesRepository;
        }

        public async Task<DoctorsHeaderViewModel> GetAllSearchedAsync(
            string specialtyId,
            string cityAreaId,
            string clinicId,
            string searchName,
            int pageNumber,
            //SearchSorting sorting,
            //Gender gender,
            //string insuranceId,
            int itemsPerPage = 8)
        {
            var doctorsQuery = this.doctorsRepository.AllAsNoTracking()
                .OrderBy(d => d.ScheduledAppointments.Select(sa => sa.Rating.Value).Average())
                .AsQueryable();
            if (!string.IsNullOrEmpty(specialtyId))
            {
                doctorsQuery = doctorsQuery
                    .Where(d => d.Specialty.Id == specialtyId);
            }

            if (!string.IsNullOrEmpty(cityAreaId))
            {
                doctorsQuery = doctorsQuery
                    .Where(d => d.Clinic.Area.Id == cityAreaId);
            }

            if (!string.IsNullOrEmpty(clinicId))
            {
                doctorsQuery = doctorsQuery
                    .Where(d => d.Clinic.Id == clinicId);
            }

            if (!string.IsNullOrWhiteSpace(searchName))
            {
                doctorsQuery = doctorsQuery
                    .Where(d => (d.FirstName + " " + d.LastName).ToLower().Contains(searchName.ToLower()));
            }

            //doctorsQuery = sorting switch
            //{
            //    SearchSorting.DateCreated => doctorsQuery.OrderByDescending(d => d.Id),
            //    SearchSorting.Rating => doctorsQuery.OrderByDescending(d => d.ScheduledAppointments.Select(sa => sa.Rating.Value).Average()),
            //    SearchSorting.AppointmentsCount => doctorsQuery.OrderByDescending(d => d.ScheduledAppointments.Count),
            //    _ => doctorsQuery.OrderByDescending(d => d.Id),
            //};

            //doctorsQuery = gender switch
            //{
            //    Gender.Male => doctorsQuery.Where(d => d.Gender == Gender.Male).OrderByDescending(d => d.Id),
            //    Gender.Female => doctorsQuery.Where(d => d.Gender == Gender.Female).OrderByDescending(d => d.Id),
            //    _ => doctorsQuery.OrderByDescending(d => d.Id),
            //};

            //if (!string.IsNullOrWhiteSpace(insuranceId))
            //{
            //    doctorsQuery = doctorsQuery
            //        .Where(d => d.Clinic.InsuranceCompanies.Any(ic => ic.InsuranceID == insuranceId));
            //}

            var allDoctors = await doctorsQuery
                .Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage)
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
                                    .Where(sa => sa.AppointmentStatus == AppointmentStatus.Completed && sa.HasBeenVoted == true).Any() ? d.ScheduledAppointments
                                    .Where(sa => sa.AppointmentStatus == AppointmentStatus.Completed && sa.HasBeenVoted == true).Select(sa => sa.Rating.Value).Average() : 0,
                    RatingCount = d.ScheduledAppointments
                                    .Where(sa => sa.AppointmentStatus == AppointmentStatus.Completed && sa.HasBeenVoted == true).Any() ? d.ScheduledAppointments
                                    .Where(sa => sa.AppointmentStatus == AppointmentStatus.Completed && sa.HasBeenVoted == true).Count() : 0,
                    About = d.About,
                })
                .ToListAsync();

            var result = new DoctorsHeaderViewModel
            {
                Doctors = allDoctors,
            };

            return result;
        }

        public IEnumerable<DoctorsViewModel> GetAll()
        {
            var allDoctors = this.doctorsRepository.All()
                .OrderBy(d => d.ScheduledAppointments.Select(sa => sa.Rating.Value).Average())
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
                                    .Where(sa => sa.AppointmentStatus == AppointmentStatus.Completed && sa.HasBeenVoted == true).Any() ? d.ScheduledAppointments
                                    .Where(sa => sa.AppointmentStatus == AppointmentStatus.Completed && sa.HasBeenVoted == true).Select(sa => sa.Rating.Value).Average() : 0,
                    RatingCount = d.ScheduledAppointments
                                    .Where(sa => sa.AppointmentStatus == AppointmentStatus.Completed && sa.HasBeenVoted == true).Any() ? d.ScheduledAppointments
                                    .Where(sa => sa.AppointmentStatus == AppointmentStatus.Completed && sa.HasBeenVoted == true).Count() : 0,
                    About = d.About,
                })
                .ToList();

            return allDoctors;
        }

        public async Task<DoctorsViewModel> GetByIdAsync(string doctorId)
        {
            var currentDoctor = await this.doctorsRepository.All()
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
                                    .Where(sa => sa.AppointmentStatus == AppointmentStatus.Completed && sa.HasBeenVoted == true).Any() ? d.ScheduledAppointments
                                    .Where(sa => sa.AppointmentStatus == AppointmentStatus.Completed && sa.HasBeenVoted == true).Select(sa => sa.Rating.Value).Average() : 0,
                    RatingCount = d.ScheduledAppointments
                                    .Where(sa => sa.AppointmentStatus == AppointmentStatus.Completed && sa.HasBeenVoted == true).Any() ? d.ScheduledAppointments
                                    .Where(sa => sa.AppointmentStatus == AppointmentStatus.Completed && sa.HasBeenVoted == true).Count() : 0,
                    About = d.About,
                })
                .FirstOrDefaultAsync();

            return currentDoctor;
        }

        public async Task<IEnumerable<DoctorsViewModel>> GetByClinicAsync(string clinicId)
        {
            var doctorsInClinic = await this.doctorsRepository.All()
                .Where(d => d.ClinicId == clinicId)
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
                                    .Where(sa => sa.AppointmentStatus == AppointmentStatus.Completed && sa.HasBeenVoted == true).Any() ? d.ScheduledAppointments
                                    .Where(sa => sa.AppointmentStatus == AppointmentStatus.Completed && sa.HasBeenVoted == true).Select(sa => sa.Rating.Value).Average() : 0,
                    RatingCount = d.ScheduledAppointments
                                    .Where(sa => sa.AppointmentStatus == AppointmentStatus.Completed && sa.HasBeenVoted == true).Any() ? d.ScheduledAppointments
                                    .Where(sa => sa.AppointmentStatus == AppointmentStatus.Completed && sa.HasBeenVoted == true).Count() : 0,
                    About = d.About,
                })
                .ToListAsync();

            return doctorsInClinic;
        }
    }
}
