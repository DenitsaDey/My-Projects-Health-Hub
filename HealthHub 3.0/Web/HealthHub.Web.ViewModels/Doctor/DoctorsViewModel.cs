namespace HealthHub.Web.ViewModels.Doctor
{
    using System.Collections.Generic;

    public class DoctorsViewModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public string PhoneNumber { get; set; }

        public string ImageUrl { get; set; }

        public string Clinic { get; set; }

        public string Specialty { get; set; }

        public int YearsOFExperience { get; set; }

        public string WorksWithChildren { get; set; }

        public string OnlineConsultation { get; set; }

        public double AverageRating { get; set; }

        public int RatingCount { get; set; }

        public string About { get; set; }
    }
}
