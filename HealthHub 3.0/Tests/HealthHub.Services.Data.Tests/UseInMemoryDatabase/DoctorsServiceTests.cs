namespace HealthHub.Services.Data.Tests.UseInMemoryDatabase
{
    using Microsoft.Extensions.DependencyInjection;

    public class DoctorsServiceTests : BaseServiceTests
    {
        private IDoctorsService Service => this.ServiceProvider.GetRequiredService<IDoctorsService>();
        /*
         Task<DoctorsFilterViewModel> GetAllSearchedAsync(
            string specialtyId,
            string cityAreaId,
            string insuranceId,
            bool worksWithChilderen,
            bool onlineConsultations,
            Gender gender,
            SearchSorting sorting,
            string clinicId,
            string searchName,
            int pageNumber,
            int itemsPerPage);

        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetAllWithDeleted<T>();

        Task<string> AddAsync(DoctorInputModel input);

        bool DoctorExists(string doctorId);

        Task DeleteAsync(string id);

        Task UpdateAsync(string id, DoctorEditInputModel input);

        Task<T> GetByIdAsync<T>(string doctorId);

        string GetIdByMostAppointments();

        T GetByAppointment<T>(string appointmentId);

        Task<IEnumerable<T>> GetByClinicAsync<T>(string clinicId);
         */
    }
}
