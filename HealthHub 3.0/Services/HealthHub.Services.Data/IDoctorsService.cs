using HealthHub.Web.ViewModels.Doctor;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthHub.Services.Data
{
    public interface IDoctorsService
    {
        IEnumerable<DoctorsSummaryViewModel> GetAll();

        IEnumerable<DoctorsSummaryViewModel> GetAllSearched(string specialty, string area, string name);

        DoctorsViewModel GetById(string doctorId);
    }
}
