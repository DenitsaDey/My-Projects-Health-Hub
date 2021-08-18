namespace HealthHub.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HealthHub.Data.Common.Repositories;
    using HealthHub.Data.Models;
    using HealthHub.Data.Models.Enums;
    using HealthHub.Services.Mapping;
    using HealthHub.Web.ViewModels.Doctor;
    using Microsoft.EntityFrameworkCore;

    public class DoctorsService : IDoctorsService
    {
        private readonly IDeletableEntityRepository<Doctor> doctorsRepository;
        private readonly IDeletableEntityRepository<Service> servicesRepository;
        private readonly IDeletableEntityRepository<Specialty> specialtyRepository;
        private readonly IDeletableEntityRepository<Appointment> appointmentsRepository;

        public DoctorsService(
            IDeletableEntityRepository<Doctor> doctorsRepository,
            IDeletableEntityRepository<Service> servicesRepository,
            IDeletableEntityRepository<Specialty> specialtyRepository,
            IDeletableEntityRepository<Appointment> appointmentsRepository)
        {
            this.doctorsRepository = doctorsRepository;
            this.servicesRepository = servicesRepository;
            this.specialtyRepository = specialtyRepository;
            this.appointmentsRepository = appointmentsRepository;
        }

        public async Task<DoctorsFilterViewModel> GetAllSearchedAsync(
            string searchName,
            string clinicId,
            string specialtyId,
            string cityAreaId,
            string insuranceId,
            bool worksWithChilderen,
            bool onlineConsultations,
            Gender gender,
            SearchSorting sorting,
            int pageNumber,
            int itemsPerPage)
        {
            var doctorsQuery = this.doctorsRepository.AllAsNoTracking()
                .OrderByDescending(d => d.ScheduledAppointments.Select(sa => sa.Rating.Value).Average())
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchName))
            {
                doctorsQuery = doctorsQuery
                    .Where(d => (d.FirstName + " " + d.LastName).ToLower().Contains(searchName.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(clinicId))
            {
                doctorsQuery = doctorsQuery
                    .Where(d => d.Clinic.Id == clinicId);
            }

            if (!string.IsNullOrWhiteSpace(specialtyId))
            {
                doctorsQuery = doctorsQuery
                    .Where(d => d.Specialty.Id == specialtyId);
            }

            if (!string.IsNullOrWhiteSpace(cityAreaId))
            {
                doctorsQuery = doctorsQuery
                    .Where(d => d.Clinic.Area.Id == cityAreaId);
            }

            if (!string.IsNullOrWhiteSpace(insuranceId))
            {
                doctorsQuery = doctorsQuery
                    .Where(d => d.Clinic.InsuranceCompanies.Any(x => x.InsuranceId == insuranceId));
            }

            if (worksWithChilderen)
            {
                doctorsQuery = doctorsQuery.Where(d => d.WorksWithChildren);
            }

            if (onlineConsultations)
            {
                doctorsQuery = doctorsQuery.Where(d => d.OnlineConsultation);
            }

            doctorsQuery = sorting switch
            {
                SearchSorting.DateCreated => doctorsQuery.OrderByDescending(d => d.CreatedOn),
                SearchSorting.Rating => doctorsQuery.OrderByDescending(d => d.ScheduledAppointments.Select(sa => sa.Rating.Value).Average()),
                SearchSorting.AppointmentsCount => doctorsQuery.OrderByDescending(d => d.ScheduledAppointments.Count),
                _ => doctorsQuery,
            };

            doctorsQuery = gender switch
            {
                Gender.Male => doctorsQuery.Where(d => d.Gender == Gender.Male),
                Gender.Female => doctorsQuery.Where(d => d.Gender == Gender.Female),
                _ => doctorsQuery,
            };

            var allDoctors = await doctorsQuery
                .Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage)
                .Select(d => new DoctorsViewModel
                {
                    Id = d.Id,
                    FullName = d.FullName,
                    ImageUrl = d.ImageUrl,
                    ClinicName = d.Clinic.Name,
                    SpecialtyName = d.Specialty.Name,
                })
                .ToListAsync();

            var result = new DoctorsFilterViewModel
            {
                Doctors = allDoctors,
                DoctorsCount = doctorsQuery.ToList().Count(),
            };

            return result;
        }

        public IEnumerable<T> GetAll<T>()
        {
            var allDoctors = this.doctorsRepository.All()
                .OrderByDescending(d => d.ScheduledAppointments.Select(sa => sa.Rating.Value).Average())
                .ThenByDescending(d => d.CreatedOn)
                .To<T>()
                .ToList();

            return allDoctors;
        }

        // for Administration Area/ Doctors Controller/ Index
        public IEnumerable<T> GetDeleted<T>()
        {
            return this.doctorsRepository.AllWithDeleted()
                .Include(d => d.Clinic)
                .Include(d => d.Specialty)
                .Where(d => d.IsDeleted)
                .To<T>()
                .ToList();
        }

        // for Administration Area/ Doctors Controller/ Create
        public async Task<string> AddAsync(DoctorInputModel input)
        {
            if (!this.specialtyRepository.All().Any(x => x.Id == input.SpecialtyId))
            {
                var specialty = new Specialty { Name = input.SpecialtyId };
                await this.specialtyRepository.AddAsync(specialty);
                await this.specialtyRepository.SaveChangesAsync();
                input.SpecialtyId = specialty.Id;
            }

            var doctor = new Doctor
            {
                FirstName = input.FirstName,
                LastName = input.LastName,
                Gender = input.Gender,
                PhoneNumber = input.PhoneNumber,
                ImageUrl = input.ImageUrl,
                ClinicId = input.ClinicId,
                SpecialtyId = input.SpecialtyId,
                YearsOFExperience = input.YearsOFExperience,
                WorksWithChildren = input.WorksWithChildren,
                OnlineConsultation = input.OnlineConsultation,
                About = input.About,
            };

            await this.doctorsRepository.AddAsync(doctor);
            await this.doctorsRepository.SaveChangesAsync();

            return doctor.Id;
        }

        // for Administration Area/ Doctors Controller/
        public bool DoctorExists(string id)
        {
            return this.doctorsRepository.All().Any(x => x.Id == id);
        }

        // for Administration Area/ Doctors Controller/ Delete
        public async Task DeleteAsync(string id)
        {
            var doctor = await this.doctorsRepository.All()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            this.doctorsRepository.Delete(doctor);
            await this.doctorsRepository.SaveChangesAsync();
        }

        // for Administration Area/ Doctors Controller/ Edit
        public async Task UpdateAsync(string id, DoctorEditInputModel input)
        {
            var doctor = this.doctorsRepository.All().FirstOrDefault(x => x.Id == id);
            doctor.FirstName = input.FirstName;
            doctor.LastName = input.LastName;
            doctor.Gender = input.Gender;
            doctor.PhoneNumber = input.PhoneNumber;
            doctor.ImageUrl = input.ImageUrl;
            doctor.ClinicId = input.ClinicId;
            doctor.SpecialtyId = input.SpecialtyId;
            doctor.YearsOFExperience = input.YearsOFExperience;
            doctor.WorksWithChildren = input.WorksWithChildren;
            doctor.OnlineConsultation = input.OnlineConsultation;
            doctor.About = input.About;
            await this.doctorsRepository.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync<T>(string doctorId)
        {
            var currentDoctor = await this.doctorsRepository.AllWithDeleted() // all with deleted in order to be able to still show details for deleted doctors in Admin/Doctors/Index in Deleted Doctors table
                .Where(d => d.Id == doctorId)
                .To<T>()
                .FirstOrDefaultAsync();

            return currentDoctor;
        }

        // for demo purposes in Doctor Area/ Appointments Controller/ Index
        public string GetIdByMostAppointments()
        {
            //return this.doctorsRepository.All()
            //    .OrderByDescending(d => d.ScheduledAppointments.Count)
            //    .FirstOrDefault()
            //    .Id;
            var groupedAppointments = this.appointmentsRepository.All()
                .GroupBy(a => a.DoctorId)
                .Select(x => new
                {
                    DoctorId = x.Key,
                    Count = x.Select(a => a.Id).Distinct().Count(),
                });

            var doctorId = groupedAppointments
                .OrderByDescending(x => x.Count)
                .FirstOrDefault()
                .DoctorId;

            return doctorId;
        }

        public T GetByAppointment<T>(string appointmentId)
        {
            var currentDoctor = this.doctorsRepository.All()
                .Where(d => d.ScheduledAppointments.Any(a => a.Id == appointmentId))
                .To<T>()
                .FirstOrDefault();

            return currentDoctor;
        }

        public async Task<IEnumerable<T>> GetByClinicAsync<T>(string clinicId)
        {
            var doctorsInClinic = await this.doctorsRepository.All().Where(d => d.ClinicId == clinicId)
                .To<T>()
                .ToListAsync();

            return doctorsInClinic;
        }

        public async Task RateDoctorAsync(string doctorId, int rateValue)
        {
            var doctor =
                await this.doctorsRepository
                .All()
                .Where(x => x.Id == doctorId)
                .FirstOrDefaultAsync();

            // might not need old and new in my case (depending on the view)
            var oldRating = doctor.ScheduledAppointments
                .Where(sa => (bool)sa.HasBeenVoted).Any() ? 0 :
                doctor.ScheduledAppointments
                .Where(sa => (bool)sa.HasBeenVoted)
                .Average(sa => sa.Rating.Value);
            var oldRatersCount = doctor.ScheduledAppointments.Where(sa => (bool)sa.HasBeenVoted).Any() ? 0 :
                doctor.ScheduledAppointments.Where(sa => (bool)sa.HasBeenVoted).Count();

            var newRatersCount = oldRatersCount + 1;
            var newRating = (oldRating + rateValue) / newRatersCount;

            await this.doctorsRepository.SaveChangesAsync();
        }
    }
}
