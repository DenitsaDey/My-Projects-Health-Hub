using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthHub.Services
{
    public interface IInsuranceScraperService
    {
        Task ImportInsuranceCompanies();
    }
}
