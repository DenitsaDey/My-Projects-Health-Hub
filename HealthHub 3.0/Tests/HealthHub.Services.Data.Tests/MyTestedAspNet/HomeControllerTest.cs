﻿namespace HealthHub.Services.Data.Tests.MyTestedAspNet
{
    using HealthHub.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

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
