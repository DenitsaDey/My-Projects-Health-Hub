using HealthHub.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyTested.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthHub.Services.Data.Tests.MyTestedAspNet
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) 
            : base(configuration)
        {
        }

        public void ConfigureTestServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

        }
    }
}
