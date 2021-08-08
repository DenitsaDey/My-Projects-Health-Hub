namespace HealthHub.Web.ViewModels.Doctor
{
    using System.Linq;

    using AutoMapper;
    using HealthHub.Data.Models;
    using HealthHub.Data.Models.Enums;
    using HealthHub.Services.Mapping;

    public class DoctorsViewModel : HeaderSearchQueryModel, IMapFrom<Doctor>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public string ImageUrl { get; set; }

        public string ClinicName { get; set; }

        public string SpecialtyName { get; set; }

        public int YearsOFExperience { get; set; }

        public string WorksWithChildren { get; set; }

        public string OnlineConsultation { get; set; }

        public double AverageRating { get; set; }

        public int RatingsCount { get; set; }

        public string About { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Doctor, DoctorsViewModel>()
            .ForMember(x => x.AverageRating, opt =>
                opt.MapFrom(x =>
                (x.ScheduledAppointments == null ||
                    !x.ScheduledAppointments.Any() ||
                    !x.ScheduledAppointments.Where(sa => sa.HasBeenVoted).Any()) ? 0 :
                    x.ScheduledAppointments.Where(sa => sa.HasBeenVoted).Average(sa => sa.Rating.Value)))
            .ForMember(x => x.RatingsCount, opt =>
                opt.MapFrom(x =>
                (x.ScheduledAppointments == null ||
                    !x.ScheduledAppointments.Any() ||
                    !x.ScheduledAppointments.Where(sa => sa.HasBeenVoted).Any()) ? 0 :
                    x.ScheduledAppointments.Where(sa => sa.HasBeenVoted).Count()));
        }
    }
}
