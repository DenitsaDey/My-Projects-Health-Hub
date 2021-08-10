namespace HealthHub.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using HealthHub.Data;
    using HealthHub.Data.Models;
    using HealthHub.Services.Data;
    using HealthHub.Services.Data.Clinics;
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
            var applicationDbContext = this.doctorsService.GetAllWithDeleted<Doctor>();
            return this.View(applicationDbContext);
        }

        // GET: Administration/Doctors/Details/5
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var doctor = this.doctorsService.GetById<Doctor>(id);
            if (doctor == null)
            {
                return this.NotFound();
            }

            return this.View(doctor);
        }

        // GET: Administration/Doctors/Create
        public IActionResult Create()
        {
            this.ViewData["ClinicId"] = this.clinicsService.GetAllClinicIds();
            this.ViewData["SpecialtyId"] = this.specialtiesService.GetAllSpecialtiesIds();
            return this.View();
        }

        // POST: Administration/Doctors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Gender,PhoneNumber,ImageUrl,ClinicId,SpecialtyId,YearsOFExperience,WorksWithChildren,OnlineConsultation,About,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Doctor doctor)
        {
            if (this.ModelState.IsValid)
            {
                this.doctorsService.Add(doctor);
                await _context.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }
            this.ViewData["ClinicId"] = new SelectList(_context.Clinics, "Id", "Id", doctor.ClinicId);
            this.ViewData["SpecialtyId"] = new SelectList(_context.Specialties, "Id", "Id", doctor.SpecialtyId);
            return this.View(doctor);
        }

        // GET: Administration/Doctors/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return this.NotFound();
            }
            this.ViewData["ClinicId"] = new SelectList(_context.Clinics, "Id", "Id", doctor.ClinicId);
            this.ViewData["SpecialtyId"] = new SelectList(_context.Specialties, "Id", "Id", doctor.SpecialtyId);
            return View(doctor);
        }

        // POST: Administration/Doctors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("FirstName,LastName,Gender,PhoneNumber,ImageUrl,ClinicId,SpecialtyId,YearsOFExperience,WorksWithChildren,OnlineConsultation,About,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Doctor doctor)
        {
            if (id != doctor.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    _context.Update(doctor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.DoctorExists(doctor.Id))
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
            this.ViewData["ClinicId"] = new SelectList(_context.Clinics, "Id", "Id", doctor.ClinicId);
            this.ViewData["SpecialtyId"] = new SelectList(_context.Specialties, "Id", "Id", doctor.SpecialtyId);
            return this.View(doctor);
        }

        // GET: Administration/Doctors/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var doctor = await _context.Doctors
                .Include(d => d.Clinic)
                .Include(d => d.Specialty)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (doctor == null)
            {
                return this.NotFound();
            }

            return View(doctor);
        }

        // POST: Administration/Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool DoctorExists(string id)
        {
            return _context.Doctors.Any(e => e.Id == id);
        }
    }
}
