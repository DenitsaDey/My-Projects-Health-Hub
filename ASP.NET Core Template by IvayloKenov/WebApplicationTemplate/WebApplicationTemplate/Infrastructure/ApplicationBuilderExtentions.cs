namespace WebApplicationTemplate.Infrastructure
{
    using Microsoft.AspNetCore.Builder;
    using WebApplicationTemplate.Data;
    using Microsoft.Extensions.DependencyInjection; //needed for the GetService method
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using WebApplicationTemplate.Data.Models;

    public static class ApplicationBuilderExtentions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            //we need to create our scope in order to access the DbContext
            using var scopedServices = app.ApplicationServices.CreateScope();
            var data = scopedServices.ServiceProvider.GetService<ApplicationDbContext>();
            data.Database.Migrate();

            SeedCategories(data);

            return app;
        }

        //seed data manually initially without creaing interface IDataSeeder etc.
        private static void SeedCategories(ApplicationDbContext data)
        {
            if (data.Categories.Any())
            {
                return;
            }

            data.Categories.AddRange(new[]
            {
                new Category { Name = "Mini"},
                new Category { Name = "Economy"},
                new Category { Name = "Luxury"},
            });

            data.SaveChanges();
        }
    }
}
