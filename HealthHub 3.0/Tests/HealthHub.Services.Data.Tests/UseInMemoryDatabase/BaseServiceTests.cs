namespace HealthHub.Services.Data.Tests.UseInMemoryDatabase
{
    using System;
    using System.Reflection;
    using HealthHub.Data;
    using HealthHub.Data.Common.Repositories;
    using HealthHub.Data.Repositories;
    using HealthHub.Services.Data.Clinics;
    using HealthHub.Services.Data.Ratings;
    using HealthHub.Services.Mapping;
    using HealthHub.Web.ViewModels;
    using HealthHub.Web.ViewModels.Appointment;
    using HealthHub.Web.ViewModels.Clinics;
    using HealthHub.Web.ViewModels.Doctor;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public abstract class BaseServiceTests : IDisposable
    {
        protected BaseServiceTests()
        {
            var services = this.SetServices();

            this.ServiceProvider = services.BuildServiceProvider();
            this.DbContext = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        }

        protected IServiceProvider ServiceProvider { get; set; }

        protected ApplicationDbContext DbContext { get; set; }

        public void Dispose()
        {
            this.DbContext.Database.EnsureDeleted();
            this.SetServices();
        }

        private ServiceCollection SetServices()
        {
            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(
                options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            // Application services
            services.AddTransient<IAppointmentsService, AppointmentsService>();
            services.AddTransient<ICityAreasService, CityAreasService>();
            services.AddTransient<IClinicsService, ClinicsService>();
            services.AddTransient<IDoctorsService, DoctorsService>();
            services.AddTransient<IGetCountsService, GetCountsService>();
            services.AddTransient<IServicesService, ServicesService>();
            services.AddTransient<ISpecialtiesService, SpecialtiesService>();
            services.AddTransient<IInsuranceService, InsuranceService>();
            services.AddTransient<IRatingsService, RatingsService>();

            // AutoMapper
            AutoMapperConfig.RegisterMappings(typeof(SpecialtyViewModel).GetTypeInfo().Assembly);
            AutoMapperConfig.RegisterMappings(typeof(ServicesViewModel).GetTypeInfo().Assembly);
            AutoMapperConfig.RegisterMappings(typeof(InsuranceViewModel).GetTypeInfo().Assembly);
            AutoMapperConfig.RegisterMappings(typeof(InsuranceClinicsViewModel).GetTypeInfo().Assembly);
            AutoMapperConfig.RegisterMappings(typeof(ClinicViewModel).GetTypeInfo().Assembly);
            AutoMapperConfig.RegisterMappings(typeof(AppointmentViewModel).GetTypeInfo().Assembly);
            AutoMapperConfig.RegisterMappings(typeof(CityAreasViewModel).GetTypeInfo().Assembly);
            
            return services;
        }
    }
}
