using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using WebApplicationTemplate.Data;
using WebApplicationTemplate.Data.Models;
using WebApplicationTemplate.Infrastructure;
using WebApplicationTemplate.Models.Cars;
using WebApplicationTemplate.Services.Cars;
using WebApplicationTemplate.Services.Dealers;

namespace WebApplicationTemplate.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarsService carsService;
        private readonly IDealersService dealersService;
        private readonly IMapper mapper;

        public CarsController(ICarsService carsService,
            IDealersService dealersService,
            IMapper mapper)
        {
            this.carsService = carsService;
            this.dealersService = dealersService;
            this.mapper = mapper;
        }

        public IActionResult All([FromQuery] AllCarsQueryModel query) // instead of taking each param separately like (string brand, string searchTerm, CarSorting sorting)
                                                                      //the issue is that the classes won't bind automatically when there is a GET request, that's why we have to add attribute [FromQuery]
        {
            var queryResult = this.carsService.All(
                query.Brand,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                query.CarsPerPage);

            var carBrands = this.carsService.AllCarBrands();

            query.Brands = carBrands;
            query.Cars = queryResult.Cars;
            query.TotalCars = queryResult.TotalCars;

            return this.View(query);
        }

        [Authorize]
        public IActionResult Mine()
        {
            var myCars = this.carsService.ByUser(this.User.GetId());
            /*or the id could be taken from the userManager
            var user = await this.userManager.GetUserAsync(this.HttpContext.User);
            var userId = await this.userManager.GetUserIdAsync(user);
            */

            return this.View(myCars);
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.dealersService.IsDealer(this.User.GetId()))
            {
                //TODo TempData
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            return this.View(new CarInputModel
            {
                Categories = this.carsService.GetCarCategories()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(CarInputModel input)
        {
            if (!this.dealersService.IsDealer(this.User.GetId()))
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }
            //always do server side validation of client input as they can manipulate the html (validation for correct dropdown options)
            //manually add error to the ModelState (key is the name of the property) before the validation check
            if (!this.carsService.CategoryExists(input.CategoryId))
            {
                this.ModelState.AddModelError(nameof(input.CategoryId), "Category does not exist!");
            }

            if (!ModelState.IsValid) //(input)model validation and returning the form with the input if info is missing and input is not valid
            {
                input.Categories = this.carsService.GetCarCategories(); //we have to give value to the categories, as the view returns the form but without the categories list
                return this.View(input);
            }

            //dealerId needed in order to assign the car to the dealer when creating it
            var dealerId = this.dealersService.GetId(this.User.GetId());

            this.carsService.Add(input, dealerId);

            //when post method, always Redirect. If we return View, then the user may submit the form again and dublicate the data in the database
            //when redirecting to the same controller, only state which action, i.e RedirectToAction("All")
            //to avoid hardcoding the actionName, use RedirectToAction(nameof(All));
            return this.RedirectToAction("All", "Cars"); // => goes to this.RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Edit(string carid)
        {

            if (!this.dealersService.IsDealer(this.User.GetId()))
            {
                //TODo TempData
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            var car = this.carsService.Details(carid);
            if(car.UserId != this.User.GetId())
            {
                return Unauthorized(); //if the car is owned by a dealer that is different from the currently logged dealer
            }

            var carForm = this.mapper.Map<CarInputModel>(car);
            carForm.Categories = this.carsService.GetCarCategories();
            return this.View(carForm);
            //previously without autoMapper:
            //return this.View(new CarInputModel
            //{
            //    Brand = car.Brand,
            //    Model = car.Model,
            //    Description = car.Description,
            //    ImgUrl = car.ImgUrl,
            //    Year = car.Year,
            //    CategoryId = car.Category,
            //    Categories = this.carsService.GetCarCategories()
            //});
            
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(string carId, CarInputModel input)
        {
            if (!this.dealersService.IsDealer(this.User.GetId()))
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }
            //always do server side validation of client input as they can manipulate the html (validation for correct dropdown options)
            //manually add error to the ModelState (key is the name of the property) before the validation check
            if (!this.carsService.CategoryExists(input.CategoryId))
            {
                this.ModelState.AddModelError(nameof(input.CategoryId), "Category does not exist!");
            }

            if (!ModelState.IsValid) //(input)model validation and returning the form with the input if info is missing and input is not valid
            {
                input.Categories = this.carsService.GetCarCategories(); //we have to give value to the categories, as the view returns the form but without the categories list
                return this.View(input);
            }

            /*
             var car = this.carsService.Details(carid);
            if(car.UserId != this.User.GetId())
            {
                return Unauthorized(); //if the car is owned by a dealer that is different from the currently logged dealer
            }
            another way to do this validation is by creating a bool method in the service:
             */

            if (!this.carsService.IsByDealer(carId, this.dealersService.GetId(this.User.GetId())))
            {
                return BadRequest();
            }

            this.carsService.Edit(carId, input);

            return RedirectToAction(nameof(Mine), "Cars");
        }

    }
}
