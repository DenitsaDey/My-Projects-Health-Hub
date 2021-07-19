namespace HealthHub.Services.Data
{
    using System.Linq;

    using HealthHub.Data.Common.Repositories;
    using HealthHub.Data.Models;
    using HealthHub.Services.Data.Models;
    using HealthHub.Web.ViewModels;

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

        public CountsViewModel GetCounts()
        {
            var countsData = new CountsViewModel
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
