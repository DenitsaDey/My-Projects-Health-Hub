namespace HealthHub.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IInsuranceScraperService
    {
        Task ImportInsuranceCompanies();
    }
}
