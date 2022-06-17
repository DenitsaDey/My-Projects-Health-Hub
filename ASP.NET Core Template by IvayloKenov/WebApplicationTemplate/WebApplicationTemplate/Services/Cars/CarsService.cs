namespace WebApplicationTemplate.Services.Cars
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using WebApplicationTemplate.Data;
    using WebApplicationTemplate.Data.Models;
    using WebApplicationTemplate.Models.Cars;

    public class CarsService : ICarsService
    {
        private readonly ApplicationDbContext data;
        private readonly IMapper mapper;

        public CarsService(ApplicationDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public void Add(CarInputModel input, string dealerId)
        {
            var car = new Car
            {
                Brand = input.Brand,
                Model = input.Model,
                Description = input.Description,
                Year = input.Year,
                ImgUrl = input.ImgUrl,
                CategoryId = input.CategoryId,
                DealerId = dealerId,
            };

            this.data.Cars.Add(car);
            this.data.SaveChanges();
        }

        public bool Edit(string carId, CarInputModel input)
        {
            var carData = this.data.Cars.Find(carId); // first we need to get the existing data for this carId

            if(carData == null)
            {
                return false;
            }

            carData.Brand = input.Brand;
            carData.Model = input.Model;
            carData.Description = input.Description;
            carData.ImgUrl = input.ImgUrl;
            carData.Year = input.Year;
            carData.CategoryId = input.CategoryId;

            this.data.SaveChanges();

            return true;
        }

        public AllCarsQueryModel All(
            string brand, 
            string searchTerm, 
            CarSorting sorting,
            int carsPerPage,
            int currentPage) 
        {
            var carsQuery = this.data.Cars.AsQueryable(); //takes the request from the db for the cars

            if (!string.IsNullOrWhiteSpace(brand))
            {
                carsQuery = carsQuery.Where(c => c.Brand == brand);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm)) //filters the carsQuery
            {
                carsQuery = carsQuery.Where(c =>
                    c.Brand.ToLower().Contains(searchTerm.ToLower()) ||
                    c.Model.ToLower().Contains(searchTerm.ToLower()) ||
                    c.Description.ToLower().Contains(searchTerm.ToLower()));
            }

            carsQuery = sorting switch
            {
                CarSorting.Year => carsQuery.OrderByDescending(c => c.Year),
                CarSorting.BrandandModel => carsQuery.OrderBy(c => c.Brand).ThenBy(c => c.Model),
                CarSorting.DateCreated or _ => carsQuery.OrderByDescending(c => c.Id) //this is if car.Id was int, in Niki's Template c.CreatedOn
                //last condition is the one we choose as default, and for this it goes at the bottom with the default "_" case
            };

            var cars = carsQuery
                .Skip((currentPage - 1) * carsPerPage)
                .Take(carsPerPage)
                .Select(c => new CarSummaryViewModel
                {
                    Id = c.Id,
                    Brand = c.Brand,
                    Model = c.Model,
                    ImgUrl = c.ImgUrl,
                    Category = c.Category.Name
                })
                .ToList();

            var totalCars = carsQuery.Count(); //and not this.data.Cars.Count() as this will not reflect the only search results but all the cars in the database in genereal

            return new AllCarsQueryModel
            {
                TotalCars = totalCars,
                CurrentPage = currentPage,
                CarsPerPage = carsPerPage,
                Cars = cars
            };
        }

        public IEnumerable<string> AllCarBrands()
        {
            return this.data.Cars
                .Select(c => c.Brand)
                .Distinct()
                .ToList();
        }

        public IEnumerable<CarSummaryViewModel> ByUser(string userId)
        {
            //for easier demo we will not implement filtering & paging on the My Cars page
            var carsQuery = this.data.Cars.AsQueryable(); //takes the request from the db for the cars
            var cars = carsQuery
                .Where(c => c.Dealer.UserId == userId)
                .Select(c => new CarSummaryViewModel
                {
                    Id = c.Id, //Id is needed in order to be passed further for the Edit or Delete actions
                    Brand = c.Brand,
                    Model = c.Model,
                    ImgUrl = c.ImgUrl,
                    Category = c.Category.Name
                })
                .ToList();

            return cars;
        }

        public bool CategoryExists(string categoryId)
        {
            return this.data.Categories
                .Any(c => c.Id == categoryId);
        }

        public CarDetailsViewModel Details(string carId)
        {
            return this.data.Cars
                .Where(c => c.Id == carId)
                .ProjectTo<CarDetailsViewModel>(this.mapper.ConfigurationProvider)
                // select is replaced by ProjectTo
                //.Select(c => new CarDetailsViewModel
                //{
                //    Id = c.Id, //Id is needed in order to be passed further for the Edit or Delete actions
                //    Brand = c.Brand,
                //    Model = c.Model,
                //    ImgUrl = c.ImgUrl,
                //    Category = c.Category.Id,
                //    Description = c.Description,
                //    UserId = c.Dealer.UserId
                //})
                .FirstOrDefault();
        }

        public IEnumerable<CarCategoryViewModel> GetCarCategories()
        {
            var categories = this.data
                                .Categories
                                .Select(c => new CarCategoryViewModel
                                {
                                    Id = c.Id,
                                    Name = c.Name,
                                })
                                .ToList();
            return categories;
        }

        public bool IsByDealer(string carId, string dealerId)
        {
            return this.data.Cars
                .Any(c => c.Id == carId && c.DealerId == dealerId);
        }
    }

}
