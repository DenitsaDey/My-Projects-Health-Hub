using System;
using System.Collections.Generic;
using System.Text;

namespace HealthHub.Data.Models.Enums
{
    public enum AppointmentStatus
    {
        Requested = 1,
        Confirmed = 2,
        Cancelled = 3,
        NoShow = 4,
        Completed = 5,
    }
}
