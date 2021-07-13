using System;
using System.Collections.Generic;
using System.Text;

namespace HealthHub.Services.Data.Models
{
    public class CountsDto
    {
        public int DoctorsCount { get; set; }

        public int ClinicsCount { get; set; }

        public int SpecialtiesCount { get; set; }

        public int AppointmentsCount { get; set; }
    }
}
