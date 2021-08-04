namespace HealthHub.Web.ViewModels.Clinics
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using HealthHub.Data.Models;
    using HealthHub.Data.Models.Enums;
    using HealthHub.Services.Mapping;
    using HealthHub.Web.ViewModels.Doctor;

    public class ClinicViewModel : IMapFrom<Clinic>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string MapUrl { get; set; }

        public string AreaId { get; set; }

        public string AreaName { get; set; }

        public string Address { get; set; }

        public IEnumerable<DoctorsViewModel> MedicalStaff { get; set; }

        public IEnumerable<InsuranceClinicsViewModel> InsuranceCompanies { get; set; }

        public double AverageRating { get; set; }

        public int RatingCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Clinic, ClinicViewModel>()
                //.ForMember(x => x.MedicalStaff, opt =>
                //opt.MapFrom(x =>
                //x.MedicalStaff.Where(d => d.ClinicId == this.Id).Any() ? 
                //x.MedicalStaff.Where(d => d.ClinicId == this.Id)
                //   .Select(d => new DoctorsViewModel
                //   {
                //       Id = d.Id,
                //       AverageRating = d.ScheduledAppointments
                //                   .Where(sa => sa.AppointmentStatus == AppointmentStatus.Completed && sa.HasBeenVoted == true).Any() ? d.ScheduledAppointments
                //                   .Where(sa => sa.AppointmentStatus == AppointmentStatus.Completed && sa.HasBeenVoted == true).Select(sa => sa.Rating.Value).Average() : 0,
                //       RatingCount = d.ScheduledAppointments
                //                   .Where(sa => sa.AppointmentStatus == AppointmentStatus.Completed && sa.HasBeenVoted == true).Any() ? d.ScheduledAppointments
                //                   .Where(sa => sa.AppointmentStatus == AppointmentStatus.Completed && sa.HasBeenVoted == true).Count() : 0,
                //   })
                //   .ToList() : new List<DoctorsViewModel>()))
                .ForMember(x => x.RatingCount, opt =>
                opt.MapFrom(x =>
                this.MedicalStaff.Select(x => x.RatingCount).Sum()))
                .ForMember(x => x.AverageRating, opt =>
                opt.MapFrom(x =>
                this.MedicalStaff.Select(x => x.AverageRating).Average()));
                //.ForMember(x => x.InsuranceCompanies, opt =>
                // opt.MapFrom(x =>
                // x.InsuranceCompanies.Where(ic => ic.ClinicId == this.Id).Any()?
                // x.InsuranceCompanies.Where(ic => ic.ClinicId == this.Id)
                //    .Select(ic => new InsuranceClinicsViewModel
                //    {
                //        ClinicId = ic.ClinicId,
                //        InsuranceId = ic.InsuranceId,
                //    })
                //    .ToList() : new List<InsuranceClinicsViewModel>()));
        }
    }
}
