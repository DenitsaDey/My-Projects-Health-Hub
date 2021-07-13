using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthHub.Services.Data
{
    public interface IRatingService
    {
        Task SetRatungAsync(string appointmentId, int value);

        double GetDoctorAverageRating(string doctorId);

        double GetClinicAverageRating(string clinicId);
    }
}
