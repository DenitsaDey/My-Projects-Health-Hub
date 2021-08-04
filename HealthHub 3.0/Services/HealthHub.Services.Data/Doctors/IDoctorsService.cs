namespace HealthHub.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using HealthHub.Data.Models.Enums;
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
            int itemsPerPage = 8);

        IEnumerable<DoctorsViewModel> GetAll();

        Task<DoctorsViewModel> GetByIdAsync(string doctorId);

        DoctorsViewModel GetByAppointment(string appointmentId);

        Task<IEnumerable<DoctorsViewModel>> GetByClinicAsync(string clinicId);
    }
}
