using HealthHub.Data.Common.Repositories;
using HealthHub.Data.Models;
using HealthHub.Services.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HealthHub.Services.Data
{
    public class GetCountsService : IGetCountsService
    {
        private readonly IDeletableEntityRepository<Doctor> doctorsRepository;
        private readonly IDeletableEntityRepository<Clinic> clinicsRepository;
        private readonly IDeletableEntityRepository<Specialty> specialtiesRepository;
        private readonly IDeletableEntityRepository<Appointment> appointmentsRepository;

        public GetCountsService(
            IDeletableEntityRepository<Doctor> doctorsRepository,
            IDeletableEntityRepository<Clinic> clinicsRepository,
            IDeletableEntityRepository<Specialty> specialtiesRepository,
            IDeletableEntityRepository<Appointment> appointmentsRepository)
        {
            this.doctorsRepository = doctorsRepository;
            this.clinicsRepository = clinicsRepository;
            this.specialtiesRepository = specialtiesRepository;
            this.appointmentsRepository = appointmentsRepository;
        }

        public CountsDto GetCounts()
        {
            var countsData = new CountsDto
            {
                DoctorsCount = this.doctorsRepository.All().Count(),
                ClinicsCount = this.clinicsRepository.All().Count(),
                SpecialtiesCount = this.specialtiesRepository.All().Count(),
                AppointmentsCount = this.appointmentsRepository.All().Count(),
            };

            return countsData;
        }
    }
}
