using HealthHub.Data.Common.Models;
using System.Collections.Generic;

namespace HealthHub.Data.Models
{
    public class ClinicProcedure : BaseDeletableModel<int>
    {
        public string ClinicId { get; set; }

        public virtual Clinic Clinic { get; set; }

        public string ProcedureId { get; set; }

        public virtual Procedure Procedure { get; set; }

        public virtual ICollection<Appointment> ScheduledAppointments { get; set; } = new HashSet<Appointment>();
    }
}