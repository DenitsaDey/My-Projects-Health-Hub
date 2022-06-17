namespace WebApplicationTemplate.Services.Statistics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using WebApplicationTemplate.Data;
    using WebApplicationTemplate.Models.Statistics;

    public class StatisticsService : IStatisticsService
    {
        private readonly ApplicationDbContext data;

        public StatisticsService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public StatisticsViewModel Total()
        {
            var totalCars = this.data.Cars.Count();
            var totalUsers = this.data.Users.Count();

            return new StatisticsViewModel
            {
                Cars = totalCars,
                Users = totalUsers,
                Rents = 0
            };
        }
    }
}
