namespace HealthHub.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using HealthHub.Data.Models;
    using HealthHub.Services.Data;
    using HealthHub.Services.Data.Clinics;
    using HealthHub.Web.ViewModels;
    using HealthHub.Web.ViewModels.Doctor;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class DoctorsController : AdministrationController
    {
        private readonly IDoctorsService doctorsService;
        private readonly IClinicsService clinicsService;
        private readonly ISpecialtiesService specialtiesService;

        public DoctorsController(
            IDoctorsService doctorsService,
            IClinicsService clinicsService,
            ISpecialtiesService specialtiesService)
        {
            this.doctorsService = doctorsService;
            this.clinicsService = clinicsService;
            this.specialtiesService = specialtiesService;
        }

        // GET: Administration/Doctors
        public IActionResult Index()
        {
            var doctors = this.doctorsService.GetAllWithDeleted<DoctorsViewModel>();
            return this.View(doctors);
        }

        // GET: Administration/Doctors/Details/5
        public async Task<IActionResult> Details(string doctorId)
        {
            if (doctorId == null)
            {
                return this.NotFound();
            }

            var model = await this.doctorsService.GetByIdAsync<DoctorsViewModel>(doctorId);

            if (model == null)
            {
                return new StatusCodeResult(404);
            }

            return this.View(model);
        }

        // GET: Administration/Doctors/Create
        public async Task<IActionResult> Create()
        {
            var clinics = this.clinicsService.GetAllClinics();
            var specialties = await this.specialtiesService.GetAllSpecialtiesAsync<SpecialtyViewModel>();

            this.ViewData["Clinics"] = new SelectList(clinics, "Id", "Name");
            this.ViewData["Specialties"] = new SelectList(specialties, "Id", "Name");
            return this.View();
        }

        // POST: Administration/Doctors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DoctorInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                var clinics = this.clinicsService.GetAllClinics();
                var specialties = await this.specialtiesService.GetAllSpecialtiesAsync<SpecialtyViewModel>();

                this.ViewData["Clinics"] = new SelectList(clinics, "Id", "Name");
                this.ViewData["Specialties"] = new SelectList(specialties, "Id", "Name");

                return this.View(input);
            }

            // Add Doctor
            await this.doctorsService.AddAsync(input);

            return this.RedirectToAction("Index");
        }

        // GET: Administration/Doctors/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var doctor = await this.doctorsService.GetByIdAsync<DoctorEditInputModel>(id);
            if (doctor == null)
            {
                return this.NotFound();
            }

            var clinics = this.clinicsService.GetAllClinics();
            var specialties = await this.specialtiesService.GetAllSpecialtiesAsync<SpecialtyViewModel>();

            this.ViewData["Clinics"] = new SelectList(clinics, "Id", "Name");
            this.ViewData["Specialties"] = new SelectList(specialties, "Id", "Name");

            return this.View(doctor);
        }

        // POST: Administration/Doctors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, DoctorEditInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                var clinics = this.clinicsService.GetAllClinics();
                var specialties = await this.specialtiesService.GetAllSpecialtiesAsync<SpecialtyViewModel>();

                this.ViewData["Clinics"] = new SelectList(clinics, "Id", "Name");
                this.ViewData["Specialties"] = new SelectList(specialties, "Id", "Name");

                return this.View(input);
            }
            else
            {
                try
                {
                    await this.doctorsService.UpdateAsync(id, input);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.DoctorExists(input.Id))
                    {
                        return this.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return this.RedirectToAction(nameof(this.Index));
            }
        }

        // GET: Administration/Doctors/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var doctor = await this.doctorsService.GetByIdAsync<DoctorsViewModel>(id);
            if (doctor == null)
            {
                return this.NotFound();
            }

            return this.View(doctor);
        }

        // POST: Administration/Doctors/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await this.doctorsService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool DoctorExists(string id)
        {
            return this.doctorsService.DoctorExists(id);
        }
    }
}
