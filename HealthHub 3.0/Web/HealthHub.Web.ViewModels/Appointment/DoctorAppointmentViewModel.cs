namespace HealthHub.Web.ViewModels.Appointment
{
    using System;

    using AutoMapper;
    using HealthHub.Data.Models;
    using HealthHub.Data.Models.Enums;
    using HealthHub.Services.Mapping;

    public class DoctorAppointmentViewModel : IMapFrom<Appointment>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string PatientId { get; set; }

        public string PatientName { get; set; }

        public string ServiceId { get; set; }

        public string ProcedureBooked { get; set; }

        public DateTime AppointmentTime { get; set; }

        public AppointmentStatus AppointmentStatus { get; set; }

        public string Message { get; set; }

        public bool? HasBeenVoted { get; set; }

        public int RatingValue { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Appointment, DoctorAppointmentViewModel>()
                .ForMember(x => x.RatingValue, opt =>
                opt.MapFrom(x =>
                x.HasBeenVoted ? x.Rating.Value : 0))
                .ForMember(x => x.PatientName, opt =>
                opt.MapFrom(x =>
                x.Patient.FirstName + " " + x.Patient.LastName))
                .ForMember(x => x.ProcedureBooked, opt =>
                opt.MapFrom(x =>
                x.ProcedureBooked.Name));
        }
    }
}
