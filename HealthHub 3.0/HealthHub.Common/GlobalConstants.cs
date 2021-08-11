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

            public const string AdminFirstName = "Admin";

            public const string AdminLastName = "AdminLastName";

            public const string DoctorEmail = "doctor@doctor.com";

            public const string DoctorFirstName = "Doctor";

            public const string DoctorLastName = "DoctorLastName";

            public const string PatientEmail = "patient@patient.com";

            public const string PatientFirstName = "Patient";

            public const string PatientLastName = "PatientLastName";
        }

        public static class DateTimeFormats
        {
            public const string DateFormat = "dd-MM-yyyy";

            public const string TimeFormat = "h:mmtt";

            public const string DateTimeFormat = "dd-MM-yyyy h:mmtt";
        }

        public static class DataValidations
        {
            public const int SpecialtyMaxLength = 100;

            public const int InsuranceMaxLength = 60;

            public const int ClinicNameMaxLength = 50;

            public const int ClinicNameMinLength = 3;

            public const int AboutMaxLength = 3500;

            public const int NameMaxLength = 40;

            public const int NameMinLength = 2;

            public const int MessageMaxLength = 700;

            public const int MessageMinLength = 50;

            public const int CityAreaMaxLength = 80;

            public const int AddressMaxLength = 200;

            public const int AddressMinLength = 5;

            public const int YearsofExperienceMaxLength = 75;

            public const int YearsofExperienceMinLength = 0;
        }

        public static class ErrorMessages
        {
            public const string About = "Content must be up to 3500 characters.";

            public const string Name = "Name must be between 2 and 40 characters.";

            public const string ClinicName = "Name must be between 3 and 50 characters.";

            public const string Message = "Additional Notes must be between up to 700 characters.";

            public const string Address = "Address must be between 5 and 200 characters.";

            public const string CityArea = "City Area must be up to 80 characters.";

            public const string Insurance = "Insurance company name must be up to 60 characters.";

            public const string Specialty = "Specialty must be up to 100 characters.";

            public const string YearsofExperience = "Experience must be at least 0 and up to 75 years.";

            public const string Image = "Please select a JPG, JPEG or PNG image smaller than 1MB.";

            public const string DateTime = "Please select a valid DATE and TIME from the datepicker calendar on the left.";

            public const string Rating = "Please choose a valid number of stars from 1 to 5.";
        }
    }
}
