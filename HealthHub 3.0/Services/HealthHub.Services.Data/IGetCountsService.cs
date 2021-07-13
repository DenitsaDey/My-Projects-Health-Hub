using HealthHub.Services.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthHub.Services.Data
{
    public interface IGetCountsService
    {
        CountsDto GetCounts();
    }
}
