namespace HealthHub.Web.Areas.Administration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HealthHub.Data;
    using HealthHub.Data.Common.Repositories;
    using HealthHub.Data.Models;
    using HealthHub.Services.Data;
    using HealthHub.Services.Data.Clinics;
    using HealthHub.Web.Areas.Administration.Controllers;
    using HealthHub.Web.ViewModels;
    using HealthHub.Web.ViewModels.Clinics;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class ClinicsController : AdministrationController
    {
        private readonly IClinicsService clinicsService;
        private readonly ICityAreasService cityAreasService;
        private readonly IInsuranceService insurancesService;

        public ClinicsController(
            IClinicsService clinicsService,
            ICityAreasService cityAreasService,
            IInsuranceService insurancesService)
        {
            this.clinicsService = clinicsService;
            this.cityAreasService = cityAreasService;
            this.insurancesService = insurancesService;
        }

        // GET: Administration/Clinics
        public IActionResult Index()
        {
            var clinics = this.clinicsService.GetAllWithDeleted();
            var insuranceCompanies = clinics.Select(c => c.InsuranceCompanies.Select(ic => ic.InsuranceName)).ToList();
            this.ViewData["InsuranceCompanies"] = new SelectList(insuranceCompanies, "Name");

            return this.View(clinics);
        }

        // GET: Administration/Clinics/Details/5
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var clinic = this.clinicsService.GetById(id);

            if (clinic == null)
            {
                return this.NotFound();
            }

            return this.View(clinic);
        }

        // GET: Administration/Clinics/Create
        public async Task<IActionResult> Create()
        {
            var areas = await this.cityAreasService.GetAllCityAreasAsync<CityAreasViewModel>();
            var insuranceCompanies = this.insurancesService.GetAllInsuranceCompanies<InsuranceViewModel>();

            this.ViewData["Areas"] = new SelectList(areas, "Id", "Name");
            this.ViewData["InsuranceCompanies"] = new SelectList(insuranceCompanies, "Id", "Name");
            return this.View();
        }

        // POST: Administration/Clinics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClinicInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                var areas = await this.cityAreasService.GetAllCityAreasAsync<CityAreasViewModel>();
                var insuranceCompanies = this.insurancesService.GetAllInsuranceCompanies<InsuranceViewModel>();

                this.ViewData["Areas"] = new SelectList(areas, "Id", "Name");
                this.ViewData["InsuranceCompanies"] = new SelectList(insuranceCompanies, "Id", "Name");
                return this.View(input);
            }

            // Add Clinic
            await this.clinicsService.AddAsync(input);

            return this.RedirectToAction("Index");
        }

        // GET: Administration/Clinics/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var clinic = this.clinicsService.GetById(id);
            if (clinic == null)
            {
                return this.NotFound();
            }

            var areas = await this.cityAreasService.GetAllCityAreasAsync<CityAreasViewModel>();

            this.ViewData["Areas"] = new SelectList(areas, "Id", "Name");

            return this.View(clinic);
        }

        // POST: Administration/Clinics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ClinicEditInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                var areas = await this.cityAreasService.GetAllCityAreasAsync<CityAreasViewModel>();
                var insuranceCompanies = this.insurancesService.GetAllInsuranceCompanies<InsuranceViewModel>();

                this.ViewData["Areas"] = new SelectList(areas, "Id", "Name");
                this.ViewData["InsuranceCompanies"] = new SelectList(insuranceCompanies, "Id", "Name");
                return this.View();
            }
            else
            {
                try
                {
                    await this.clinicsService.UpdateAsync(id, input);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.ClinicExists(input.Id))
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

        // GET: Administration/Clinics/Delete/5
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var clinic = this.clinicsService.GetById(id);
            if (clinic == null)
            {
                return this.NotFound();
            }

            return this.View(clinic);
        }

        // POST: Administration/Clinics/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await this.clinicsService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool ClinicExists(string id)
        {
            return this.clinicsService.ClinicExists(id);
        }
    }
}
