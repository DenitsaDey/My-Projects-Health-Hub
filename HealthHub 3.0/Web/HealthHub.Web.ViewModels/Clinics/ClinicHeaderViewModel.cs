namespace HealthHub.Web.ViewModels.Clinics
{
    public class ClinicHeaderViewModel : HeaderSearchQueryModel
    {
        public ClinicViewModel Clinic { get; set; }

        public PagingViewModel Paging { get; set; }
    }
}
