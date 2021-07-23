namespace HealthHub.Services.Data
{
    using System.Collections.Generic;

    using HealthHub.Web.ViewModels;
    using HealthHub.Web.ViewModels.Doctor;

    public interface IDoctorsService
    {
        HeaderSearchQueryModel GetAll(string specialtyId, string cityAreaId, string name);

        IEnumerable<DoctorsSummaryViewModel> GetAllSearched(string specialty, string area, string name);

        DoctorsViewModel GetById(string doctorId);
    }
}
