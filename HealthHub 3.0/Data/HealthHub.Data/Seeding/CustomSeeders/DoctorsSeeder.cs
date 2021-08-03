namespace HealthHub.Data.Seeding.CustomSeeders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HealthHub.Data.Models;
    using HealthHub.Data.Models.Enums;

    public class DoctorsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Doctors.Any())
            {
                return;
            }

            var doctors = new List<Doctor>();

            await AddNewDoctor(
                dbContext,
                doctors,
                "Caitlin",
                "Huckel",
                Gender.Female,
                "+974000001",
                "https://www.sidra.org/sites/default/files/2020-10/Caitlin-Huckell.jpg",
                "Sidra Medicine",
                "Gynaecology And Obstetrics",
                23,
                false,
                false,
                "Dr. Huckell is a Fellow of the Royal College of Physicians and Surgeons of Canada with over 17 years experience as a private consultant in Obstetrics and Gynaecology. She has extensive experience in Medical Education and Leadership. She is a Keynote Speaker at CME Conferences for Contraception, Mirena Use and Management of Polycystic Ovarian Syndrome.In addition she was a National Program Invited Presenter and HPV Campaign Educator. She has been actively involved in Teaching Fellows, Medical Residents, and Medical Students and has been recognized with multiple Teaching Awards.She was a Physician and Nurse Educator in the Obstetrical Risk Reduction Program.");

            await AddNewDoctor(
                dbContext,
                doctors,
                "Shadra",
                "Udassi",
                Gender.Female,
                "+974000002",
                "https://www.sidra.org/sites/default/files/2020-10/ShardaUdassi.jpg",
                "Sidra Medicine",
                "Pediatrics",
                24,
                true,
                false,
                "Dr. Sharda Udassi is Senior Attending at Sidra and Associate Professor of Clinical Pediatrics at Weill Cornell Medicine. She has done American Board of Medical Quality (ABMQ) Certification in Medical Quality, in addition to completion of International Society for Quality in Healthcare fellowship, Florida Medical Association leadership academy and University of Florida leadership training.");

            await AddNewDoctor(
                dbContext,
                doctors,
                "Ahmed",
                "Shamekh",
                Gender.Male,
                "+974000003",
                "https://www.sidra.org/sites/default/files/2020-10/CCMG.AhmedShamekh.jpg",
                "Sidra Medicine",
                "Emergency Medicine",
                32,
                true,
                false,
                "Dr. Ahmed Shamekh is a Senior Attending Physician who was a Consultant Pediatrician at Kings College Hospital NHS Trust for over 12 years before joining Sidra Medicine in December 2017. Dr. Shamekh was the Lead Pediatrician for the Emergency Department at Princess Royal University Hospital. He has an interest in both Diabetes and Medical Education and has been the College Tutor leading the trainee education and training experience.");

            await AddNewDoctor(
                    dbContext,
                    doctors,
                    "Jamal",
                    "Grayez",
                    Gender.Male,
                    "+974000004",
                    "https://www.sidra.org/sites/default/files/2020-10/Jamal-Grayez.jpg",
                    "Sidra Medicine",
                    "Internal Medicine",
                    12,
                    true,
                    false,
                    "Dr Grayez is an experienced consultant in Pulmonology and acute care medicine, before joining Sidra Medicine he worked as a consultant Physician in Internal medicine and Pulmonology in Oxford, Birmingham in the united kingdom. He has extended his knowledge and expertise to include pleural, sleep and difficult asthma, lung infections, lung cancer, pulmonary embolism.");

            await AddNewDoctor(
                dbContext,
                doctors,
                "Patrick",
                "Tang",
                Gender.Male,
                "+974000005",
                "https://www.sidra.org/sites/default/files/2020-10/Patrick-Tang.jpg",
                "Sidra Medicine",
                "Microbiology And Virology",
                18,
                true,
                false,
                "Patrick Tang (MD, PhD) is the Division Chief of Pathology Sciences at Sidra Medicine. In this role, Dr. Tang is responsible for the development and implementation of new molecular and genomics-based tests for microbiology and infectious diseases, and other diagnostic disciplines in the clinical laboratory at Sidra.");

            await AddNewDoctor(
                dbContext,
                doctors,
                "Mohamed",
                "Al Harami",
                Gender.Male,
                string.Empty,
                "https://www.cdc.gov/diabetes/images/library/spotlights/Male-doctor-smiling-portrait-close-up-Med-Res-72991363.jpg",
                "Hamad General Hospital",
                "General Surgery",
                25,
                true,
                false,
                "Dr. Al Harami has graduated MBBCh at Cairo Univercity in 1969 and is a Fellow of the Royal College of Surgeons in Ireland 1978.");

            await AddNewDoctor(
                dbContext,
                doctors,
                "Mohammed",
                "Shaarani",
                Gender.Male,
                string.Empty,
                "https://i2-prod.mirror.co.uk/interactives/article12645227.ece/ALTERNATES/s1200c/doctor.jpg",
                "Hamad General Hospital",
                "Orthopedics",
                28,
                true,
                true,
                "Dr. Shaarani has graduated MBBCh at Ain Shams Univercity in 1980 and is a Fellow of the Royal College of Surgeons of Edinburgh, 1990");

            await AddNewDoctor(
                dbContext,
                doctors,
                "Munir",
                "Osman",
                Gender.Male,
                "+974000008",
                "https://i2-prod.mirror.co.uk/incoming/article4843769.ece/ALTERNATES/s615/Doctor.jpg",
                "Hamad General Hospital",
                "Emergency Medicine",
                29,
                true,
                false,
                "Dr. Osman has graduated MBCHB at Alexandria Univercity in 1964 and has a Diploma in General Surgery, 1968, Alexandria University");

            await AddNewDoctor(
                dbContext,
                doctors,
                "Awad",
                "Rashid",
                Gender.Male,
                "+974000009",
                "https://images2.minutemediacdn.com/image/upload/c_fill,g_auto,h_1248,w_2220/f_auto,q_auto,w_1100/v1555923840/shape/mentalfloss/164609725.jpg",
                "Hamad General Hospital",
                "Nephrology",
                31,
                false,
                true,
                "Dr. Rashid has graduated MBBCh at Cairo Univercity in 1966, MRCp at the Royal College of Physicialns of Edinburgh, 1979");

            await AddNewDoctor(
                dbContext,
                doctors,
                "Mohamed",
                "Al Jaber",
                Gender.Male,
                "+974000010",
                "https://familydoctor.org/wp-content/uploads/2018/02/41808433_l.jpg",
                "Hamad General Hospital",
                "Children Rehab",
                35,
                true,
                true,
                "Dr. Al Jabere has graduated 33MBBCh at Ain Shams Univercity in 1979 and has a Diploma in Child Health from the Royal College of Surgeons in Ireland, 1984.");

            await AddNewDoctor(
                dbContext,
                doctors,
                "Houda",
                "Ferjani",
                Gender.Female,
                "+974000011",
                "https://dohaclinichospital.com/assets/admin/images/doctors/MY_1586153701.jpg",
                "Doha Clinic Hospital",
                "Emergency Medicine",
                27,
                true,
                false,
                "Dr. Ferjani has received her Master’s degree in Emergency Medicine from the Medical Faculty of Tunis in 2012. Holds a certificate of Disaster Medicine from Universite Libre De Bruxelles in Belgium, 2017.");

            await AddNewDoctor(
                dbContext,
                doctors,
                "Maha",
                "El Ghazy",
                Gender.Female,
                "+974000012",
                "https://dohaclinichospital.com/assets/admin/images/doctors/MY_1612678838.jpg",
                "Doha Clinic Hospital",
                "Dermatology",
                19,
                false,
                true,
                "Dr. Maha Mohamed El Ghazy is a dermatology associate specialist, with special interest in treatment of hair and nail disorders. Graduated from school of medicine, Kasr Al-Ainy (Cairo university), Egypt in 2001. Diploma degree in dermatology, Suez Canal university, Egypt in October 2007. Master degree in dermatology, Suez Canal university, Egypt in October 2016.");

            await AddNewDoctor(
                dbContext,
                doctors,
                "Maha",
                "Yehia",
                Gender.Female,
                "+974000013",
                "https://dohaclinichospital.com/assets/admin/images/doctors/MY_1602569626.jpg",
                "Doha Clinic Hospital",
                "Gynaecology And Obstetrics",
                36,
                false,
                false,
                "Prior to joining Doha Clinic Hospital she worked within the Tertiary Hospital in Fraser Health in British Columbia, Canada as a Senior Consultant in Obstetrics and Gynaecology having completed her Medical Degree and Royal College Fellowship in Vancouver, Canada.");

            await AddNewDoctor(
                dbContext,
                doctors,
                "Marwa",
                "Kandeel",
                Gender.Female,
                "+974000014",
                "https://dohaclinichospital.com/assets/admin/images/doctors/MY_1602571405.jpg",
                "Doha Clinic Hospital",
                "Internal Medicine",
                13,
                true,
                false,
                "Dr Kandeel has over 10 years’ experience as a Senior clinical advisor for the Parliamentary and health service Ombudsman in the UK, London. Advising on health complaint a cross the whole of the UK and providing recommendations to hospitals to improve the quality of the health care.");

            await AddNewDoctor(
                dbContext,
                doctors,
                "Mohamad",
                "Barhoma",
                Gender.Male,
                "+974000015",
                "https://dohaclinichospital.com/assets/admin/images/doctors/MY_1602570292.jpg",
                "Doha Clinic Hospital",
                "Ophthalmology",
                14,
                true,
                false,
                "Doctor Barhoma Attained his Doctorate of Ophthalmology in 2011 from School of Medicine – Cairo University (Kasr Al-Aini), Master degree in Ophthalmology, 2006, Graduated from faculty of medicine, Cairo University, Egypt, 2001.");

            await AddNewDoctor(
                dbContext,
                doctors,
                "Wissem",
                "Melki",
                Gender.Male,
                "+974000016",
                "https://www.ahlihospital.com/medical/gastroenterology/profiles/Wissem.jpg",
                "Al-Ahli Hospital",
                "Gastroenterology",
                27,
                true,
                true,
                "Dr. Melki has worked as a Consultant Gastroenterology and Hepatology ; SAAD specialist Hospital , Saudi Arabia, and as Assistant Professor of Gastroenterology and hepatology , Monastir Medical College.");

            await AddNewDoctor(
                dbContext,
                doctors,
                "Samira",
                "Chine",
                Gender.Female,
                string.Empty,
                "https://t4.ftcdn.net/jpg/03/17/85/49/360_F_317854905_2idSdvi2ds3yejmk8mhvxYr1OpdVTrSM.jpg",
                "Al-Ahli Hospital",
                "Cardiology",
                15,
                false,
                true,
                "Dr. Chine has 17 years of experience in cardiac intensive care management. Coronary disease, cardiac failure, valvular disease, high blood pressure, arrhythmias, Echocardiography – Stress test – Holter monitor, Prevention of cardiac disease and management of risk factors.");

            await AddNewDoctor(
                dbContext,
                doctors,
                "Bashir",
                "Elmalik",
                Gender.Male,
                "+974000018",
                "https://samedaydoctor.org/app/uploads/2019/05/doctor-about-us.jpeg",
                "Al-Ahli Hospital",
                "Neurology",
                30,
                false,
                true,
                "Dr. Elmalik has Fellowship Of The Royal College Of Surgeons (Edinburgh), Intercollegiate Fellowship In Surgical Neurology (UK), Fellowship Of The American College Of Surgeons (USA)");

            await AddNewDoctor(
                dbContext,
                doctors,
                "Yousef",
                "Matani",
                Gender.Male,
                "+974000019",
                "https://www.ahlihospital.com/medical/urology/profiles/matani.jpg",
                "Al-Ahli Hospital",
                "Urology",
                29,
                false,
                false,
                "Dr. Matani has worked in Kuwait, Jordan, Saudi Arabia, UK and Germany in various fields of surgery and urology. Regarding the genito-urinary system, particular emphasis on stones, infections, tumors, lower urinary tract problems, fertility and sexual dysfunction. Special interest in endoscopic urology procedures and percutaneous renal access.");

            await AddNewDoctor(
                dbContext,
                doctors,
                "Mohammed",
                "Al-Rishi",
                Gender.Male,
                "+974000020",
                "https://www.ahlihospital.com/medical/endocrinology/profiles/dr.rishi.jpg",
                "Al-Ahli Hospital",
                "Endocrinology",
                26,
                false,
                false,
                "Dr Mohamed has very good experience in all aspects of Endocrinology and Diabetes Care, with special emphasis on Diabetes in pregnancy, and Poly Cystic Ovary Syndrome, in addition to Intensive insulin management through structured education and insulin pump. Thyroid disease, Adrenal disease, pituitary and wide experience in this field.");

            await AddNewDoctor(
                dbContext,
                doctors,
                "Maya",
                "Jalloul",
                Gender.Female,
                "+974000021",
                "https://www.westbaymedicare.com/wp-content/uploads/2018/05/maya-1.jpg",
                "Medicare Clinic",
                "Family Medicine",
                16,
                true,
                true,
                "Dr. Maya achieved a post-graduate diploma in Diabetes, Leicester University, UK. She has been practicing in Qatar as a Family Medicine Consultant for over 10 years, at Qatar Petroleum Healthcare. Her focus on a patient-centered approach contributed to the overall improvement of the department.");

            await AddNewDoctor(
                dbContext,
                doctors,
                "Rana",
                "Almaaz",
                Gender.Female,
                "+974000022",
                "https://www.westbaymedicare.com/wp-content/uploads/2018/05/rama-1.jpg",
                "Medicare Clinic",
                "Cardiology",
                10,
                false,
                true,
                "Dr.Rama has been practicing since 2011, treating diagnosing and managing a wide spectrum of internal and cardiac illnesses (hypertension , arrhythymia, valvular, cardiomyopathies , congenital, ischemic heart diseases and heart attacks in addition to the cardiac emergency cases such as cardiac arrest and CPR), utilizing the most important cardiac investigation tools like electrocardiogram , echocardiography , stress test , holter, ,monitoring gamma camera, catheterization and revasculization ,coronary CT and others.");

            await AddNewDoctor(
                dbContext,
                doctors,
                "Eleni",
                "Papakanaki",
                Gender.Female,
                "+974000023",
                "https://www.westbaymedicare.com/wp-content/uploads/2018/05/eleni-1.jpg",
                "Medicare Clinic",
                "Pediatrics",
                9,
                true,
                true,
                "Dr. Eleni has completed her training in Paediatrics in Chelsea and Westminster Hospital, NHS Foundation Trust, where she has worked as a Clinical Fellow in General Paediatrics and Paediatric Surgery. Since May 2018 she is a Specialist in Paediatrics and she is also a valued member of the West Bay MediCare Team.");

            await AddNewDoctor(
                dbContext,
                doctors,
                "Anke",
                "Ertan",
                Gender.Female,
                "+974000024",
                "https://www.westbaymedicare.com/wp-content/uploads/2018/05/anke-1.jpg",
                "Medicare Clinic",
                "Gynaecology And Obstetrics",
                24,
                false,
                true,
                "Before joining West Bay Medicare, Dr. Ertan spent four years in an Independent Private Practice for OB & GYN, Holistic Medicine, and Coping Strategies for Stress, broadening her fields of expertise. She has a wide range of interests in Medicine, having completed training courses in Stress, and Genetic Disorders, as well as gaining a Diploma of Business Administration in Public Health. ");

            await AddNewDoctor(
                dbContext,
                doctors,
                "Ali",
                "Mardassi",
                Gender.Male,
                "+974000025",
                "https://www.westbaymedicare.com/wp-content/uploads/2018/05/Ali-1.jpg",
                "Medicare Clinic",
                "ENT",
                11,
                true,
                true,
                "Over the past 10 years, Dr. Ali has strengthened his professional and clinical experience and gained a variety of medical and surgical expertise within many hospitals in Tunisia, France and Qatar and had an additional expertise in the management of ENT disorders in pilots and aircrew after getting trained in the Val De Grace Institute of Paris (France) where he obtained the International Certificate of Aeronautical Medicine in June 2016.");

            await dbContext.Doctors.AddRangeAsync(doctors);
            await dbContext.SaveChangesAsync();
        }

        private static async Task AddNewDoctor(
            ApplicationDbContext dbContext,
            List<Doctor> doctors,
            string firstName,
            string lastName,
            Gender gender,
            string phoneNumber,
            string imageUrl,
            string clinicName,
            string specialtyName,
            int yearsOfExperience,
            bool worksWithChildren,
            bool onlineConsultation,
            string about)
        {
            if (!dbContext.Specialties.Any(s => s.Name == specialtyName))
            {
                await dbContext.Specialties.AddAsync(new Specialty { Name = specialtyName });
                await dbContext.SaveChangesAsync();
            }

            var doctor = new Doctor
            {
                FirstName = firstName,
                LastName = lastName,
                Gender = gender,
                PhoneNumber = phoneNumber,
                ImageUrl = imageUrl,
                ClinicId = dbContext.Clinics.Where(c => c.Name == clinicName).FirstOrDefault().Id,
                SpecialtyId = dbContext.Specialties.Where(s => s.Name == specialtyName).FirstOrDefault().Id,
                YearsOFExperience = yearsOfExperience,
                WorksWithChildren = worksWithChildren,
                OnlineConsultation = onlineConsultation,
                About = about,
            };

            doctors.Add(doctor);
        }
    }
}
