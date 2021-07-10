using System;
using System.Collections.Generic;
using System.Text;

namespace HealthHub.Data.Models.Enums
{
    public enum MedicalConditionStatus
    {
        NotYeTtreated = 1,
        CurrentlyTreated = 2,
        Recovered = 3,
        RefusedTreatment = 4,
        TreatmentNotAvaialble = 5,
    }

    public static class Extension
    {
        public static string ToString(this MedicalConditionStatus status)
        {
            return status switch
            {
                MedicalConditionStatus.NotYeTtreated => "Not yet treated",
                MedicalConditionStatus.CurrentlyTreated => "Currently being treated",
                MedicalConditionStatus.Recovered => "Patient recovered",
                MedicalConditionStatus.RefusedTreatment => "Patient refused treatment",
                MedicalConditionStatus.TreatmentNotAvaialble => "Treatment is not available",
                _ => string.Empty,
            };
        }
    }
}
