﻿namespace HealthHub.Data.Seeding.CustomSeeders
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using HealthHub.Common;
    using HealthHub.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class AccountsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            // Create Admin
            await CreateUser(
                userManager,
                roleManager,
                GlobalConstants.AccountsSeeding.AdminEmail,
                GlobalConstants.AccountsSeeding.AdminFirstName,
                GlobalConstants.AccountsSeeding.AdminLastName,
                GlobalConstants.AdministratorRoleName);

            // Create Doctor
            await CreateUser(
                userManager,
                roleManager,
                GlobalConstants.AccountsSeeding.DoctorEmail,
                GlobalConstants.AccountsSeeding.DoctorFirstName,
                GlobalConstants.AccountsSeeding.DoctorLastName,
                GlobalConstants.DoctorRoleName);

            // Create Patient
            await CreateUser(
                userManager,
                roleManager,
                GlobalConstants.AccountsSeeding.PatientEmail,
                GlobalConstants.AccountsSeeding.PatientFirstName,
                GlobalConstants.AccountsSeeding.PatientLastName,
                GlobalConstants.PatientRoleName);
        }

        private static async Task CreateUser(
            UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, string email, string firstName, string lastName, string roleName = null)
        {
            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
            };

            var password = GlobalConstants.AccountsSeeding.Password;

            if (roleName != null)
            {
                var role = await roleManager.FindByNameAsync(roleName);

                if (!userManager.Users.Any(x => x.Roles.Any(x => x.RoleId == role.Id)))
                {
                    var result = await userManager.CreateAsync(user, password);

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, roleName);
                    }
                }
            }
            else
            {
                if (!userManager.Users.Any(x => x.Roles.Count() == 0))
                {
                    var result = await userManager.CreateAsync(user, password);
                }
            }
        }
    }
}
