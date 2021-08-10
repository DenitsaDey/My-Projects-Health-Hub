namespace HealthHub.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using HealthHub.Data.Common.Repositories;
    using HealthHub.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class InsurancesController : AdministrationController
    {
        private readonly IDeletableEntityRepository<Insurance> insurancesRepository;

        public InsurancesController(IDeletableEntityRepository<Insurance> insurancesRepository)
        {
            this.insurancesRepository = insurancesRepository;
        }

        // GET: Administration/Insurances
        public async Task<IActionResult> Index()
        {
            return this.View(await this.insurancesRepository.AllWithDeleted().ToListAsync());
        }

        // GET: Administration/Insurances/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var insurance = await this.insurancesRepository.All()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (insurance == null)
            {
                return this.NotFound();
            }

            return this.View(insurance);
        }

        // GET: Administration/Insurances/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Administration/Insurances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Insurance insurance)
        {
            if (this.ModelState.IsValid)
            {
                await this.insurancesRepository.AddAsync(insurance);
                await this.insurancesRepository.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(insurance);
        }

        // GET: Administration/Insurances/Edit/5
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var insurance = this.insurancesRepository.All().FirstOrDefault(x => x.Id == id);
            if (insurance == null)
            {
                return this.NotFound();
            }

            return this.View(insurance);
        }

        // POST: Administration/Insurances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Insurance insurance)
        {
            if (id != insurance.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.insurancesRepository.Update(insurance);
                    await this.insurancesRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.InsuranceExists(insurance.Id))
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

            return this.View(insurance);
        }

        // GET: Administration/Insurances/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var insurance = await this.insurancesRepository.All()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (insurance == null)
            {
                return this.NotFound();
            }

            return this.View(insurance);
        }

        // POST: Administration/Insurances/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var insurance = this.insurancesRepository.All().FirstOrDefault(x => x.Id == id);
            this.insurancesRepository.Delete(insurance);
            await this.insurancesRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool InsuranceExists(string id)
        {
            return this.insurancesRepository.All().Any(e => e.Id == id);
        }
    }
}
