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

        public static class SeededDataCounts
        {
            public const int BlogPosts = 4;

            public const int Categories = 6;

            public const int Services = 55;

            public const int CityAreas = 10;

            public const int Salons = 18;

            public const int Appointments = 54;
        }
    }
}
