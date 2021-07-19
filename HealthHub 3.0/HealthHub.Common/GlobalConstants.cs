namespace HealthHub.Common
{
    public static class GlobalConstants
    {
        public const string SystemName = "HealthHub";

        public const string AdministratorRoleName = "Admin";

        public const string DoctorRoleName = "Doctor";

        public const string PatientRoleName = "Patient";

        public static class AccountsSeeding
        {
            public const string Password = "123456";

            public const string AdminEmail = "admin@admin.com";

            public const string DoctorEmail = "doctor@doctor.com";

            public const string PatientEmail = "patient@patient.com";
        }

        public static class DateTimeFormats
        {
            public const string DateFormat = "dd-MM-yyyy";

            public const string TimeFormat = "h:mmtt";

            public const string DateTimeFormat = "dd-MM-yyyy h:mmtt";
        }
    }
}
