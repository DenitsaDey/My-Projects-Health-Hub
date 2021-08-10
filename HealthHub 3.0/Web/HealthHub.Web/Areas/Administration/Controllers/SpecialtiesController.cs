namespace HealthHub.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using HealthHub.Data;
    using HealthHub.Data.Common.Repositories;
    using HealthHub.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class SpecialtiesController : AdministrationController
    {
        private readonly IDeletableEntityRepository<Specialty> specialtiesRepository;

        public SpecialtiesController(IDeletableEntityRepository<Specialty> specialtiesRepository)
        {
            this.specialtiesRepository = specialtiesRepository;
        }

        // GET: Administration/Specialties
        public async Task<IActionResult> Index()
        {
            return this.View(await this.specialtiesRepository.AllWithDeleted().ToListAsync());
        }

        // GET: Administration/Specialties/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var specialty = await this.specialtiesRepository.All()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (specialty == null)
            {
                return this.NotFound();
            }

            return this.View(specialty);
        }

        // GET: Administration/Specialties/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Administration/Specialties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Specialty specialty)
        {
            if (this.ModelState.IsValid)
            {
                await this.specialtiesRepository.AddAsync(specialty);
                await this.specialtiesRepository.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(specialty);
        }

        // GET: Administration/Specialties/Edit/5
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var specialty = this.specialtiesRepository.All().FirstOrDefault(x => x.Id == id);
            if (specialty == null)
            {
                return this.NotFound();
            }

            return this.View(specialty);
        }

        // POST: Administration/Specialties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Specialty specialty)
        {
            if (id != specialty.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.specialtiesRepository.Update(specialty);
                    await this.specialtiesRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.SpecialtyExists(specialty.Id))
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

            return this.View(specialty);
        }

        // GET: Administration/Specialties/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var specialty = await this.specialtiesRepository.All()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (specialty == null)
            {
                return this.NotFound();
            }

            return this.View(specialty);
        }

        // POST: Administration/Specialties/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var specialty = this.specialtiesRepository.All().FirstOrDefault(x => x.Id == id);
            this.specialtiesRepository.Delete(specialty);
            await this.specialtiesRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool SpecialtyExists(string id)
        {
            return this.specialtiesRepository.All().Any(e => e.Id == id);
        }
    }
}
