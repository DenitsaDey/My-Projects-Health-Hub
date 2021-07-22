using HealthHub.Web.ViewModels;
using HealthHub.Web.ViewModels.Doctor;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthHub.Services.Data
{
    public interface IDoctorsService
    {
        HeaderSearchQueryModel GetAll();

        IEnumerable<DoctorsSummaryViewModel> GetAllSearched(string specialty, string area, string name);

        DoctorsViewModel GetById(string doctorId);
    }
}
