namespace HealthHub.Web.ViewModels
{
    using HealthHub.Data.Models;
    using HealthHub.Services.Mapping;

    public class InsuranceClinicsViewModel : IMapFrom<InsuranceClinic>
    {
        public int Id { get; set; }

        public string ClinicId { get; set; }

        public string ClinicName { get; set; }

        public string InsuranceId { get; set; }

        public string InsuranceName { get; set; }
    }
}
