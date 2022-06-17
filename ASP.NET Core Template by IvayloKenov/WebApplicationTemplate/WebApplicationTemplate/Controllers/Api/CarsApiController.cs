namespace WebApplicationTemplate.Controllers.Api
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections;
    using System.Linq;
    using WebApplicationTemplate.Data;
    using WebApplicationTemplate.Data.Models;

    //it can also be named just CarsController
    [ApiController]
    [Route("api/[controller]")] // or ("api/cars") - it is compulsary to be specified
    public class CarsApiController : ControllerBase
    {
        private readonly ApplicationDbContext data;

        public CarsApiController(ApplicationDbContext data)
        {
            this.data = data;
        }

        [HttpGet]
        public IEnumerable GetCars() //instead of IActionResult we return data
        {
            return this.data.Cars.ToList();
        }

        [HttpGet]
        [Route("{id}")] // => 
        public IActionResult GetDetails(string id) // public object GetDetails(int id) => this.data.Cars.Find(id);
        {
            var car = this.data.Cars.Find(id);

            if(car == null)
            {
                return NotFound();
                /* to be able to use NotFound() we have to change the action 
                  from public object GetDetails(int id) => public IActionResult GetDetails(int id)
                but then we cannot return car; it has to be return Ok(car);
                to combine both we change the action to public IActionResult<Car> GetDetails(int id)
                this way NotFound() is valid and return car is also valid
                 */
            }

            return Ok(car);
        }

        [HttpPost]
        public IActionResult SaveCar(Car car) // for the sake of the demo we use directly the model from the database, normally this won't be the case, but using a model
        {
            //no need for ModelState.IsValid, as ASP.NET automatically checks the validity of the input for api
            return Ok();
        }
    }
}
