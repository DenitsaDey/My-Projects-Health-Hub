namespace WebApplicationTemplate.Data
{
    public class DataConstants
    {
        public class Car
        {
            public const int CarBrandMinLength = 2;
            public const int CarBrandMaxLength = 20;
            public const int CarModelMinLength = 3;
            public const int CarModelMaxLength = 30;
            public const int CarDescriptionMinLength = 10;
            public const int CarDescriptionMaxLength = 200;
            public const int CarYearMinValue = 2000;
            public const int CarYearMaxValue = 2100;
        }
        
        public class Category
        {
            public const int CategoryNameMinLength = 2;
            public const int CategoryNameMaxLength = 15;
        }

        public class Dealer
        {
            public const int DealerNameMinLength = 2;
            public const int DealerNameMaxLength = 25;
            public const int PhoneNoMinLength = 8;
            public const int PhoneNoMaxLength = 10;
        }
    }
}
