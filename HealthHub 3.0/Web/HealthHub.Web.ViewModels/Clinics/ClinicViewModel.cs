namespace HealthHub.Web.ViewModels.Clinics
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using HealthHub.Data.Models;
    using HealthHub.Data.Models.Enums;
    using HealthHub.Services.Mapping;
    using HealthHub.Web.ViewModels.Doctor;

    public class ClinicViewModel : HeaderSearchQueryModel, IMapFrom<Clinic>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string MapUrl { get; set; }

        public string AreaId { get; set; }

        public string AreaName { get; set; }

        public string Address { get; set; }

        public virtual IEnumerable<DoctorsViewModel> MedicalStaff { get; set; }

        public virtual IEnumerable<InsuranceClinicsViewModel> InsuranceCompanies { get; set; }

        public double AverageRating { get; set; }

        public int RatingsCount { get; set; }

        public bool IsDeleted { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Clinic, ClinicViewModel>()
            .ForMember(x => x.AverageRating, opt =>
                opt.MapFrom(x =>
                (this.MedicalStaff == null ||
                    !this.MedicalStaff.Any()) ? 0 :
                    this.MedicalStaff.Where(ms => ms.AverageRating != 0).Average(ms => ms.AverageRating)))
            .ForMember(x => x.RatingsCount, opt =>
                opt.MapFrom(x =>
                (this.MedicalStaff == null ||
                    !this.MedicalStaff.Any()) ? 0 :
                    this.MedicalStaff.Where(ms => ms.AverageRating != 0).Sum(ms => ms.RatingsCount)));
        }
    }
}
