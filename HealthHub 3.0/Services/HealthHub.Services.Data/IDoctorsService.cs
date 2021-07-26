namespace HealthHub.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using HealthHub.Data.Models.Enums;
    using HealthHub.Web.ViewModels.Doctor;

    public interface IDoctorsService
    {
        DoctorsHeaderViewModel GetAll(
            string specialtyId,
            string cityAreaId,
            string name,
            int pageNumber,
            //SearchSorting sorting,
            //Gender gender,
            //string insuranceId,
            int itemsPerPage = 8);

        IEnumerable<DoctorsSummaryViewModel> GetAllSearched(string specialty, string area, string name);

        Task<DoctorsViewModel> GetByIdAsync(string doctorId);
    }
}
