namespace HealthHub.Web.ViewModels.Appointment
{
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using HealthHub.Common;
    using HealthHub.Data.Models;
    using HealthHub.Services.Mapping;

    public class AppointmentRatingViewModel : HeaderSearchQueryModel, IMapFrom<Appointment>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string DoctorId { get; set; }

        public string DoctorName { get; set; }

        public string DoctorSpecialty { get; set; }

        public string DoctorClinic { get; set; }

        public string DoctorImg { get; set; }

        public bool? HasBeenVoted { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = GlobalConstants.ErrorMessages.Rating)]
        public int RateValue { get; set; }

        [MaxLength(200)]
        public string AdditionalComments { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Appointment, AppointmentRatingViewModel>()
                .ForMember(x => x.DoctorSpecialty, opt =>
                opt.MapFrom(x =>
                x.Doctor.Specialty.Name))
                .ForMember(x => x.DoctorName, opt =>
                opt.MapFrom(x =>
                x.Doctor.FirstName + " " + x.Doctor.LastName))
                .ForMember(x => x.DoctorClinic, opt =>
                opt.MapFrom(x =>
                x.Doctor.Clinic.Name))
                .ForMember(x => x.DoctorImg, opt =>
                opt.MapFrom(x =>
                x.Doctor.ImageUrl));
        }
    }
}
