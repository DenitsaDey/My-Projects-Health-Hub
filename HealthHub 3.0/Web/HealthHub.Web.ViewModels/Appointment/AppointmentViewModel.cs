namespace HealthHub.Web.ViewModels.Appointment
{
    using System;

    using AutoMapper;
    using HealthHub.Data.Models;
    using HealthHub.Data.Models.Enums;
    using HealthHub.Services.Mapping;

    public class AppointmentViewModel : HeaderSearchQueryModel, IMapFrom<Appointment>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string DoctorId { get; set; }

        public string DoctorName { get; set; }

        public string DoctorImageUrl { get; set; }

        public string ClinicId { get; set; }

        public string ClinicName { get; set; }

        public string ClinicMapUrl { get; set; }

        public string ClinicAddress { get; set; }

        public string ServiceId { get; set; }

        public string ProcedureBooked { get; set; }

        public DateTime AppointmentTime { get; set; }

        public AppointmentStatus AppointmentStatus { get; set; }

        public string Message { get; set; }

        public bool? HasBeenVoted { get; set; }

        public int RatingValue { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Appointment, AppointmentViewModel>()
                .ForMember(x => x.RatingValue, opt =>
                opt.MapFrom(x =>
                x.HasBeenVoted ? x.Rating.Value : 0))
                .ForMember(x => x.DoctorName, opt =>
                opt.MapFrom(x =>
                x.Doctor.FirstName + " " + x.Doctor.LastName))
                .ForMember(x => x.ProcedureBooked, opt =>
                opt.MapFrom(x =>
                x.ProcedureBooked.Name))
                .ForMember(x => x.ClinicName, opt =>
                opt.MapFrom(x =>
                x.Doctor.Clinic.Name))
                .ForMember(x => x.ClinicMapUrl, opt =>
                opt.MapFrom(x =>
                x.Doctor.Clinic.MapUrl))
                .ForMember(x => x.ClinicAddress, opt =>
                opt.MapFrom(x =>
                x.Doctor.Clinic.Address));
        }
    }
}
