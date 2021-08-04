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

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string ImageUrl { get; set; }

        public string ClinicName { get; set; }

        public string SpecialtyName { get; set; }

        public int YearsOFExperience { get; set; }

        public string WorksWithChildren { get; set; }

        public string OnlineConsultation { get; set; }

        public double AverageRating { get; set; }

        public int RatingCount { get; set; }

        public string About { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Doctor, DoctorsViewModel>()
                .ForMember(x => x.AverageRating, opt =>
                opt.MapFrom(x =>
                x.ScheduledAppointments
                                    .Where(sa => sa.AppointmentStatus == AppointmentStatus.Completed && sa.HasBeenVoted == true).Any() ? x.ScheduledAppointments
                                    .Where(sa => sa.AppointmentStatus == AppointmentStatus.Completed && sa.HasBeenVoted == true).Select(sa => sa.Rating.Value).Average() : 0))
                .ForMember(x => x.RatingCount, opt =>
                opt.MapFrom(x =>
                x.ScheduledAppointments
                                    .Where(sa => sa.AppointmentStatus == AppointmentStatus.Completed && sa.HasBeenVoted == true).Any() ? x.ScheduledAppointments
                                    .Where(sa => sa.AppointmentStatus == AppointmentStatus.Completed && sa.HasBeenVoted == true).Count() : 0));
        }
    }
}
