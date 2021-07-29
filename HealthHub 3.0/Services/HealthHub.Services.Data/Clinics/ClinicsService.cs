namespace HealthHub.Services.Data.Clinics
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HealthHub.Data.Common.Repositories;
    using HealthHub.Data.Models;
    using HealthHub.Data.Models.Enums;
    using HealthHub.Web.ViewModels;
    using HealthHub.Web.ViewModels.Clinics;
    using HealthHub.Web.ViewModels.Doctor;
    using Microsoft.EntityFrameworkCore;

    public class ClinicsService : IClinicsService
    {
        private readonly IDeletableEntityRepository<Clinic> clinicsRepository;
        private readonly IDeletableEntityRepository<CityArea> cityAreasRepository;
        private readonly IDeletableEntityRepository<Doctor> doctorsRepository;
        private readonly IDeletableEntityRepository<InsuranceClinic> insuranceClinicsRepository;
        private readonly ICityAreasService cityAreasService;

        public ClinicsService(
        IDeletableEntityRepository<Clinic> clinicsRepository,
        IDeletableEntityRepository<CityArea> cityAreasRepository,
        ICityAreasService cityAreasService,
        IDeletableEntityRepository<Doctor> doctorsRepository,
        IDeletableEntityRepository<InsuranceClinic> insuranceClinicsRepository)
        {
            this.clinicsRepository = clinicsRepository;
            this.cityAreasRepository = cityAreasRepository;
            this.cityAreasService = cityAreasService;
            this.doctorsRepository = doctorsRepository;
            this.insuranceClinicsRepository = insuranceClinicsRepository;
        }

        public async Task AddAsync(ClinicInputModel input)
        {
            var clinic = new Clinic
            {
                Id = input.Id,
                Name = input.Name,
                Address = input.Address,
                AreaId = this.cityAreasRepository.All().FirstOrDefault(ca => ca.Id == input.AreaId).Id != null ?
                this.cityAreasRepository.All().FirstOrDefault(ca => ca.Id == input.AreaId).Id :
                this.cityAreasService.AddAsync(input.AreaName).ToString(),
                MapUrl = input.MapUrl,
            };
            await this.clinicsRepository.AddAsync(clinic);
            await this.clinicsRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<ClinicViewModel>> GetAllClinicsAsync()
        {
            var allClinics = await this.clinicsRepository.All()
                .Select(c => new ClinicViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    MapUrl = c.MapUrl,
                    Address = c.Address,
                })
                .OrderBy(x => x.Name)
                .ToListAsync();

            foreach (var clinic in allClinics)
            {
                clinic.MedicalStaff = this.doctorsRepository.All()
                    .Where(d => d.ClinicId == clinic.Id)
                    .Select(d => new DoctorsViewModel
                    {
                        Id = d.Id,
                        AverageRating = d.ScheduledAppointments
                                    .Where(sa => sa.AppointmentStatus == AppointmentStatus.Completed && sa.HasBeenVoted == true).Any() ? d.ScheduledAppointments
                                    .Where(sa => sa.AppointmentStatus == AppointmentStatus.Completed && sa.HasBeenVoted == true).Select(sa => sa.Rating.Value).Average() : 0,
                        RatingCount = d.ScheduledAppointments
                                    .Where(sa => sa.AppointmentStatus == AppointmentStatus.Completed && sa.HasBeenVoted == true).Any() ? d.ScheduledAppointments
                                    .Where(sa => sa.AppointmentStatus == AppointmentStatus.Completed && sa.HasBeenVoted == true).Count() : 0,
                    })
                    .ToList();

                clinic.RatingCount = clinic.MedicalStaff.Select(x => x.RatingCount).Sum();
            }

            return allClinics;
        }

        public IEnumerable<string> GetAllClinicsNames()
        {
            return this.clinicsRepository.All()
                .OrderBy(c => c.Name)
                .Select(c => c.Name)
                .ToList();
        }

        public async Task<ClinicViewModel> GetByIdAsync(string clinicId)
        {
            var clinic = await this.clinicsRepository.All()
                .Where(s => s.Id == clinicId)
                .Select(s => new ClinicViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Address = s.Address,
                    MapUrl = s.MapUrl,
                    MedicalStaff = this.doctorsRepository.All()
                    .Where(d => d.ClinicId == clinicId)
                    .Select(d => new DoctorsViewModel
                    {
                        Id = d.Id,
                        FirstName = d.FirstName,
                        LastName = d.LastName,
                        Specialty = d.Specialty.Name,
                    })
                    .ToList(),
                    InsuranceCompanies = this.insuranceClinicsRepository.All()
                    .Where(ic => ic.ClinicId == clinicId)
                    .Select(ic => new InsuranceViewModel
                    {
                        Id = ic.InsuranceID,
                        Name = ic.Insurance.Name,
                    })
                    .ToList(),
                })
                .FirstOrDefaultAsync();

            return clinic;
        }
    }
}
