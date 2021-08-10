namespace HealthHub.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using HealthHub.Web.ViewModels.Doctor;

    public interface IDoctorsService
    {
        Task<DoctorsHeaderViewModel> GetAllSearchedAsync(
            string specialtyId,
            string cityAreaId,
            string clinicId,
            string name,
            int pageNumber,
            //SearchSorting sorting,
            //Gender gender,
            //string insuranceId,
            int itemsPerPage);

        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetAllWithDeleted<T>();

        T GetById<T>(string id);

        Task<T> GetByIdAsync<T>(string doctorId);

        string GetIdByMostAppointments();

        T GetByAppointment<T>(string appointmentId);

        Task<IEnumerable<T>> GetByClinicAsync<T>(string clinicId);
    }
}
