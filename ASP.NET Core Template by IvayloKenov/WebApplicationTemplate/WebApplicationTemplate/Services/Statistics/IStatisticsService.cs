namespace WebApplicationTemplate.Services.Statistics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using WebApplicationTemplate.Models.Statistics;

    public interface IStatisticsService
    {
        StatisticsViewModel Total();
    }
}
