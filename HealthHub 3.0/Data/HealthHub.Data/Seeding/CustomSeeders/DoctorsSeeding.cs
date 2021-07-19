using HealthHub.Data.Models;
using HealthHub.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthHub.Data.Seeding.CustomSeeders
{
    public class DoctorsSeeding : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Doctors.Any())
            {
                return;
            }

            var doctors = new Doctor[]
            {
                new Doctor
                {
                    FirstName = "Caitlin",
                    LastName = "Huckel",
                    DateOfBirth = new DateTime(1966, 04, 30),
                    Gender = Gender.Female,
                    PhoneNumber = "+974000001",
                    ImageUrl = "https://www.sidra.org/sites/default/files/2020-10/Caitlin-Huckell.jpg",
                    Clinic = dbContext.Clinics.Where(c=>c.Name == "Sidra Medicine").FirstOrDefault(),
                    Specialty = dbContext.Specialties.Where(s=>s.Name == "Gynaecology & Obstetrics").FirstOrDefault(),
                    YearsOFExperience = 23,
                    WorksWithChildren = false,
                    OnlineConsultation = false,
                    About = "Dr. Huckell is a Fellow of the Royal College of Physicians and Surgeons of Canada with over 17 years experience as a private consultant in Obstetrics and Gynaecology. She has extensive experience in Medical Education and Leadership. She is a Keynote Speaker at CME Conferences for Contraception, Mirena Use and Management of Polycystic Ovarian Syndrome.In addition she was a National Program Invited Presenter and HPV Campaign Educator. She has been actively involved in Teaching Fellows, Medical Residents, and Medical Students and has been recognized with multiple Teaching Awards.She was a Physician and Nurse Educator in the Obstetrical Risk Reduction Program.",
                },
                
            };

            foreach (var doctor in doctors)
            {
                await dbContext.Doctors.AddAsync(doctor);
                await dbContext.SaveChangesAsync();
            }
        }
    }
    }
