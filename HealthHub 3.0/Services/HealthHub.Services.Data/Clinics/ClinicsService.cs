namespace HealthHub.Services.Data.Clinics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HealthHub.Data.Common.Repositories;
    using HealthHub.Data.Models;
    using HealthHub.Data.Models.Enums;
    using HealthHub.Services.Mapping;
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
        private readonly IDeletableEntityRepository<Insurance> insurancesRepository;
        private readonly ICityAreasService cityAreasService;

        public ClinicsService(
        IDeletableEntityRepository<Clinic> clinicsRepository,
        IDeletableEntityRepository<CityArea> cityAreasRepository,
        ICityAreasService cityAreasService,
        IDeletableEntityRepository<Doctor> doctorsRepository,
        IDeletableEntityRepository<InsuranceClinic> insuranceClinicsRepository,
        IDeletableEntityRepository<Insurance> insurancesRepository)
        {
            this.clinicsRepository = clinicsRepository;
            this.cityAreasRepository = cityAreasRepository;
            this.cityAreasService = cityAreasService;
            this.doctorsRepository = doctorsRepository;
            this.insuranceClinicsRepository = insuranceClinicsRepository;
            this.insurancesRepository = insurancesRepository;
        }

        public async Task AddAsync(ClinicInputModel input)
        {
            var clinic = new Clinic
            {
                Id = Guid.NewGuid().ToString(),
                Name = input.Name,
                Address = input.Address,
                AreaId = input.AreaId,
                MapUrl = input.MapUrl,
            };

            foreach (var inputInsuranceInClinic in input.InsuranceCompanies)
            {
                var insuranceInClinic = this.insuranceClinicsRepository.All().FirstOrDefault(x => x.Id == inputInsuranceInClinic.Id);
                if (insuranceInClinic == null)
                {
                    insuranceInClinic = new InsuranceClinic { ClinicId = clinic.Id, InsuranceId = inputInsuranceInClinic.InsuranceId };
                }

                clinic.InsuranceCompanies.Add(insuranceInClinic);

                await this.insuranceClinicsRepository.AddAsync(insuranceInClinic);
                await this.insuranceClinicsRepository.SaveChangesAsync();
            }

            await this.clinicsRepository.AddAsync(clinic);
            await this.clinicsRepository.SaveChangesAsync();
        }

        public IEnumerable<ClinicViewModel> GetAllClinics()
        {
            var allClinics = this.clinicsRepository.All()
                .OrderBy(x => x.Name)
                .Select(c => new ClinicViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    MapUrl = c.MapUrl,
                    Address = c.Address,
                    AreaId = c.AreaId,
                    AreaName = c.Area.Name,
                    MedicalStaff = new List<DoctorsViewModel>(),
                    InsuranceCompanies = new List<InsuranceViewModel>(),
                })
                .ToList();

            //foreach (var clinic in allClinics)
            //{
            //    clinic.MedicalStaff = this.doctorsRepository.All()
            //        .Where(d => d.ClinicId == clinic.Id)
            //        .Select(d => new DoctorsViewModel
            //        {
            //            Id = d.Id,
            //            AverageRating = d.ScheduledAppointments.Where(sa => sa.HasBeenVoted).Any() ?
            //                            d.ScheduledAppointments.Where(sa => sa.HasBeenVoted)
            //                            .Select(sa => sa.Rating.Value).Average() : 0,
            //            RatingsCount = d.ScheduledAppointments.Where(sa => sa.HasBeenVoted).Any() ?
            //                            d.ScheduledAppointments.Where(sa => sa.HasBeenVoted).Count() : 0,
            //        })
            //        .ToList();

            //    clinic.InsuranceCompanies = this.insuranceClinicsRepository.All()
            //        .Where(ic => ic.ClinicId == clinic.Id)
            //        .Select(ic => new InsuranceViewModel
            //        {
            //            Id = ic.Insurance.Id,
            //            Name = ic.Insurance.Name,
            //        })
            //        .ToList();

            //    clinic.AverageRating = clinic.MedicalStaff.Where(ms => ms.AverageRating != 0).Any() ?
            //        clinic.MedicalStaff.Where(ms => ms.AverageRating != 0).Select(ms => ms.AverageRating).Average() : 0;

            //    clinic.RatingsCount = clinic.MedicalStaff.Where(ms => ms.AverageRating != 0).Any() ?
            //        clinic.MedicalStaff.Where(ms => ms.AverageRating != 0).Select(ms => ms.RatingsCount).Sum() : 0;
            //}

            return allClinics;
        }

        // for filtering on the Clinics/Index page
        public async Task<ClinicFilterViewModel> GetAllSearchedAsync(
            string specialtyId,
            string cityAreaId,
            string insuranceId,
            SearchSorting sorting,
            int pageNumber,
            int itemsPerPage)
        {
            var clinicsQuery = this.clinicsRepository.AllAsNoTracking()
                .OrderBy(c => c.Name)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(specialtyId))
            {
                clinicsQuery = clinicsQuery
                    .Where(c => c.MedicalStaff.Any(ms => ms.Specialty.Id == specialtyId));
            }

            if (!string.IsNullOrWhiteSpace(cityAreaId))
            {
                clinicsQuery = clinicsQuery
                    .Where(c => c.Area.Id == cityAreaId);
            }

            if (!string.IsNullOrWhiteSpace(insuranceId))
            {
                clinicsQuery = clinicsQuery
                    .Where(d => d.InsuranceCompanies.Any(x => x.InsuranceId == insuranceId));
            }

            // for list of clinics by filter criteria on page Clinics/Index
            var filteredClinics = await clinicsQuery
                .Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage)
                .Select(c => new ClinicViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    MapUrl = c.MapUrl,
                    Address = c.Address,
                    AreaId = c.AreaId,
                    AreaName = c.Area.Name,
                    MedicalStaff = new List<DoctorsViewModel>(),
                    InsuranceCompanies = new List<InsuranceViewModel>(),
                })
                .ToListAsync();

            //foreach (var clinic in filteredClinics)
            //{
            //    clinic.MedicalStaff = this.doctorsRepository.All()
            //        .Where(d => d.ClinicId == clinic.Id)
            //        .Select(d => new DoctorsViewModel
            //        {
            //            Id = d.Id,
            //            AverageRating = d.ScheduledAppointments.Where(sa => sa.HasBeenVoted).Any() ?
            //                            d.ScheduledAppointments.Where(sa => sa.HasBeenVoted)
            //                            .Select(sa => sa.Rating.Value).Average() : 0,
            //            RatingsCount = d.ScheduledAppointments.Where(sa => sa.HasBeenVoted).Any() ?
            //                            d.ScheduledAppointments.Where(sa => sa.HasBeenVoted).Count() : 0,
            //        })
            //        .ToList();

            //    clinic.InsuranceCompanies = this.insuranceClinicsRepository.All()
            //        .Where(ic => ic.ClinicId == clinic.Id)
            //        .Select(ic => new InsuranceViewModel
            //        {
            //            Id = ic.Insurance.Id,
            //            Name = ic.Insurance.Name,
            //        })
            //        .ToList();

            //    clinic.AverageRating = clinic.MedicalStaff.Where(ms => ms.AverageRating != 0).Any() ?
            //        clinic.MedicalStaff.Where(ms => ms.AverageRating != 0).Select(ms => ms.AverageRating).Average() : 0;

            //    clinic.RatingsCount = clinic.MedicalStaff.Where(ms => ms.AverageRating != 0).Any() ?
            //        clinic.MedicalStaff.Where(ms => ms.AverageRating != 0).Select(ms => ms.RatingsCount).Sum() : 0;
            //}

            // for header bar dropdown list of all clinics
            var allClinics = this.clinicsRepository.All()
                .OrderBy(x => x.Name)
                .Select(c => new ClinicViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToList();

            var result = new ClinicFilterViewModel
            {
                Clinics = allClinics,
                FilteredClinics = filteredClinics,
                ClinicsCount = clinicsQuery.ToList().Count(),
            };

            return result;
        }

        // for Administration Area/ Clinics Controller/ Index
        public IEnumerable<T> GetAllWithDeleted<T>()
        {
            return this.clinicsRepository.AllWithDeleted()
                .Include(c => c.Area)
                .Include(c => c.InsuranceCompanies)
                .To<T>()
                .ToList();
        }

        // for Admin Area / Doctors Controller/ Create
        public IEnumerable<string> GetAllClinicIds()
        {
            return this.clinicsRepository.All()
                .Select(x => x.Id)
                .ToList();
        }

        // for Administration Area/ Clinics Controller/ Edit
        public async Task UpdateAsync(string id, ClinicEditInputModel input)
        {
            var clinic = this.clinicsRepository.All().FirstOrDefault(x => x.Id == id);
            clinic.Name = input.Name;
            clinic.MapUrl = input.MapUrl;
            clinic.AreaId = input.AreaId;
            clinic.Address = input.Address;
            clinic.MedicalStaff = this.doctorsRepository.All().Where(x => x.ClinicId == id).ToList();
            clinic.InsuranceCompanies = this.insuranceClinicsRepository.All().Where(x => x.ClinicId == id).ToList();

            await this.clinicsRepository.SaveChangesAsync();
        }

        // for Administration Area/ Clinics Controller/
        public bool ClinicExists(string id)
        {
            return this.clinicsRepository.All().Any(x => x.Id == id);
        }

        // for Administration Area/ Clinics Controller/ Delete
        public async Task DeleteAsync(string id)
        {
            var clinic = await this.clinicsRepository.All()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            this.clinicsRepository.Delete(clinic);
            await this.clinicsRepository.SaveChangesAsync();
        }

        public IEnumerable<string> GetAllClinicsNames()
        {
            return this.clinicsRepository.All()
                .OrderBy(c => c.Name)
                .Select(c => c.Name)
                .ToList();
        }

        public ClinicViewModel GetById(string clinicId)
        {
            var clinic = this.clinicsRepository.All()
                .Where(c => c.Id == clinicId)
                .Select(c => new ClinicViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Address = c.Address,
                    MapUrl = c.MapUrl,
                    MedicalStaff = this.doctorsRepository.All()
                    .Where(d => d.ClinicId == clinicId)
                    .Select(d => new DoctorsViewModel
                    {
                        Id = d.Id,
                        FullName = d.FullName,
                        SpecialtyName = d.Specialty.Name,
                        AverageRating = d.ScheduledAppointments.Where(sa => sa.HasBeenVoted).Any() ?
                                        d.ScheduledAppointments.Where(sa => sa.HasBeenVoted)
                                        .Select(sa => sa.Rating.Value).Average() : 0,
                        RatingsCount = d.ScheduledAppointments.Where(sa => sa.HasBeenVoted).Any() ?
                                       d.ScheduledAppointments.Where(sa => sa.HasBeenVoted).Count() : 0,
                    })
                    .ToList(),
                    InsuranceCompanies = this.insuranceClinicsRepository.All()
                    .Where(ic => ic.ClinicId == clinicId)
                    .Select(ic => new InsuranceViewModel
                    {
                        Id = ic.Insurance.Id,
                        Name = ic.Insurance.Name,
                    })
                    .ToList(),
                })
                .FirstOrDefault();
            clinic.AverageRating =
                    clinic.MedicalStaff.Where(ms => ms.AverageRating != 0).Any() ?
                    clinic.MedicalStaff.Where(ms => ms.AverageRating != 0).Select(ms => ms.AverageRating).Average() : 0;

            clinic.RatingsCount =
                clinic.MedicalStaff.Where(ms => ms.AverageRating != 0).Any() ?
                clinic.MedicalStaff.Where(ms => ms.AverageRating != 0).Select(ms => ms.RatingsCount).Sum() : 0;

            return clinic;
        }

        public async Task RateCLinicAsync(string clinicId, int rateValue)
        {
            var clinic =
                await this.clinicsRepository
                .All()
                .Where(x => x.Id == clinicId)
                .FirstOrDefaultAsync();

            // might not need old and new in my case (depending on the view)
            var oldRating = clinic.MedicalStaff.Select(ms => ms.ScheduledAppointments
                .Where(sa => sa.HasBeenVoted)).Any() ? 0 :
                clinic.MedicalStaff.Select(ms => ms.ScheduledAppointments
                .Where(sa => sa.HasBeenVoted)
                .Average(sa => sa.Rating.Value)).Average();
            var oldRatersCount = clinic.MedicalStaff.Select(ms => ms.ScheduledAppointments.Where(sa => sa.HasBeenVoted)).Any() ? 0 :
                clinic.MedicalStaff.Select(ms => ms.ScheduledAppointments
                .Where(sa => sa.HasBeenVoted)
                .Average(sa => sa.Rating.Value)).Count();

            var newRatersCount = oldRatersCount + 1;
            var newRating = (oldRating + rateValue) / newRatersCount;

            await this.clinicsRepository.SaveChangesAsync();
        }
    }
}
