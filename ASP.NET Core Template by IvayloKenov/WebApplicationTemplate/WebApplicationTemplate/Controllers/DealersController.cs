namespace WebApplicationTemplate.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using WebApplicationTemplate.Data;
    using WebApplicationTemplate.Data.Models;
    using WebApplicationTemplate.Infrastructure;
    using WebApplicationTemplate.Models.Dealers;

    public class DealersController : Controller
    {
        private readonly ApplicationDbContext data;

        public DealersController(ApplicationDbContext data)
        {
            this.data = data;
        }

        [Authorize] //we need authorised user, whithout user we should not be able to create a dealer
        public IActionResult Become() { 
            return this.View(); 
        }

        [HttpPost]
        [Authorize]
        public IActionResult Become(BecomeDealerInputModel input)
        {
            var userId = this.User.GetId();

            var userIsAlreadyDealer = this.data
                .Dealers
                .Any(d => d.UserId == userId);

            if (userIsAlreadyDealer)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid) 
            {
                return this.View(input);
            }

            var dealer = new Dealer
            {
                UserId = userId,
                Name = input.Name,
                PhoneNo = input.PhoneNo,
            };

            this.data.Dealers.Add(dealer);
            this.data.SaveChanges();

            return this.RedirectToAction(nameof(CarsController.Add), "Cars");
        }
    }
}
