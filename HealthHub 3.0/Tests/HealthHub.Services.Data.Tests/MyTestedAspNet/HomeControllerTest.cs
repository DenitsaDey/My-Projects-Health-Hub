using HealthHub.Data.Models;
using HealthHub.Web.Controllers;
using HealthHub.Web.ViewModels.Home;
using MyTested.AspNetCore.Mvc;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HealthHub.Services.Data.Tests.MyTestedAspNet
{
    public class HomeControllerTest
    {
        [Fact]
        public void PrivacyShouldReturnView()
            => MyController<HomeController>
            .Instance()
            .Calling(c => c.Privacy())
            .ShouldReturn()
            .View();
    }
}
