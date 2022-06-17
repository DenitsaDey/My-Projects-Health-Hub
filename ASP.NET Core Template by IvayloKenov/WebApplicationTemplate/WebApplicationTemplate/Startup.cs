namespace WebApplicationTemplate
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using WebApplicationTemplate.Data;
    using WebApplicationTemplate.Infrastructure;
    using WebApplicationTemplate.Services.Cars;
    using WebApplicationTemplate.Services.Dealers;
    using WebApplicationTemplate.Services.Statistics;

    public class Startup
    {
        public Startup(IConfiguration configuration)
            => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDbContext<ApplicationDbContext>(options => options
                .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            //Niki's template - options are moved to separate identityOptionsProvider.cs located in Data folder
            services
                .AddDefaultIdentity<IdentityUser>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAutoMapper(typeof(Startup));
            
            services.AddControllersWithViews();

            services.AddTransient<IStatisticsService, StatisticsService>();
            services.AddTransient<ICarsService, CarsService>();
            services.AddTransient<IDealersService, DealersService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //automatic migration update everytime the app starts
            /* in Niki's Template:  Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }
            */
            app.PrepareDatabase(); //seeds data manually initially without creaing interface IDataSeeder etc.

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            //chaining 
            app
                .UseHttpsRedirection()
                .UseStaticFiles()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapDefaultAreaRoute();
                    endpoints.MapDefaultControllerRoute();
                    endpoints.MapRazorPages();
                });

            
        }
    }
}
