namespace HealthHub.Data.Common
{
    public class DataConstants
    {
        public const int IdMaxLength = 40;
        public const int DefaultMaxLength = 20;

        public const int UserMinUsername = 4;
        public const int UserMinPassword = 5;
        public const string UserEmailRegularExpression = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
        public const string UserTypePatient = "Patient";
        public const string UserTypeMedicalProfessional = "Medical Professional";

        public const int MessageMaxLength = 200;
        public const int SpecialtyMaxLength = 100;
        public const int InsuranceMaxLength = 60;
        public const int CityAreaMaxLength = 80;
        public const int ClinicNameMaxLength = 50;
        public const int AddressMaxLength = 200;
    }
}
