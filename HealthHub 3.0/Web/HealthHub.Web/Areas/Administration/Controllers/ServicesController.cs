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
    public class ServicesController : AdministrationController
    {
        private readonly IDeletableEntityRepository<Service> servicesRepository;

        public ServicesController(IDeletableEntityRepository<Service> servicesRepository)
        {
            this.servicesRepository = servicesRepository;
        }

        // GET: Administration/Services
        public async Task<IActionResult> Index()
        {
            return this.View(await this.servicesRepository.AllWithDeleted().ToListAsync());
        }

        // GET: Administration/Services/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var service = await this.servicesRepository.All()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return this.NotFound();
            }

            return this.View(service);
        }

        // GET: Administration/Services/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Administration/Services/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Service service)
        {
            if (this.ModelState.IsValid)
            {
                await this.servicesRepository.AddAsync(service);
                await this.servicesRepository.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(service);
        }

        // GET: Administration/Services/Edit/5
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var service = this.servicesRepository.All().FirstOrDefault(x => x.Id == id);
            if (service == null)
            {
                return this.NotFound();
            }

            return this.View(service);
        }

        // POST: Administration/Services/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,Description,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Service service)
        {
            if (id != service.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.servicesRepository.Update(service);
                    await this.servicesRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.ServiceExists(service.Id))
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

            return this.View(service);
        }

        // GET: Administration/Services/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var service = await this.servicesRepository.All()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return this.NotFound();
            }

            return this.View(service);
        }

        // POST: Administration/Services/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var service = this.servicesRepository.All().FirstOrDefault(x => x.Id == id);
            this.servicesRepository.Delete(service);
            await this.servicesRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool ServiceExists(string id)
        {
            return this.servicesRepository.All().Any(e => e.Id == id);
        }
    }
}
