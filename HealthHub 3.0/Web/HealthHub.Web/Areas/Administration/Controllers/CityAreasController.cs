namespace HealthHub.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using HealthHub.Data.Common.Repositories;
    using HealthHub.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class CityAreasController : AdministrationController
    {
        private readonly IDeletableEntityRepository<CityArea> cityAreasRepository;

        public CityAreasController(IDeletableEntityRepository<CityArea> cityAreasRepository)
        {
            this.cityAreasRepository = cityAreasRepository;
        }

        // GET: Administration/CityAreas
        public async Task<IActionResult> Index()
        {
            return this.View(await this.cityAreasRepository.AllWithDeleted().ToListAsync());
        }

        // GET: Administration/CityAreas/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var cityArea = await this.cityAreasRepository.All()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cityArea == null)
            {
                return this.NotFound();
            }

            return this.View(cityArea);
        }

        // GET: Administration/CityAreas/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Administration/CityAreas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] CityArea cityArea)
        {
            if (this.ModelState.IsValid)
            {
                await this.cityAreasRepository.AddAsync(cityArea);
                await this.cityAreasRepository.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(cityArea);
        }

        // GET: Administration/CityAreas/Edit/5
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var cityArea = this.cityAreasRepository.All().FirstOrDefault(x => x.Id == id);
            if (cityArea == null)
            {
                return this.NotFound();
            }

            return this.View(cityArea);
        }

        // POST: Administration/CityAreas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] CityArea cityArea)
        {
            if (id != cityArea.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.cityAreasRepository.Update(cityArea);
                    await this.cityAreasRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.CityAreaExists(cityArea.Id))
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

            return this.View(cityArea);
        }

        // GET: Administration/CityAreas/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var cityArea = await this.cityAreasRepository.All()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cityArea == null)
            {
                return this.NotFound();
            }

            return this.View(cityArea);
        }

        // POST: Administration/CityAreas/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var cityArea = this.cityAreasRepository.All().FirstOrDefault(x => x.Id == id);
            this.cityAreasRepository.Delete(cityArea);
            await this.cityAreasRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool CityAreaExists(string id)
        {
            return this.cityAreasRepository.All().Any(e => e.Id == id);
        }
    }
}
