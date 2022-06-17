namespace WebApplicationTemplate.Services.Cars
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using WebApplicationTemplate.Models.Cars;

    public interface ICarsService
    {
        AllCarsQueryModel All(
            string brand,
            string searchTerm,
            CarSorting sorting,
            int carsPerPage,
            int currentPage);

        CarDetailsViewModel Details(string carId);

        bool CategoryExists(string categoryId);

        IEnumerable<CarSummaryViewModel> ByUser(string userId);

        bool IsByDealer(string carId, string dealerId);

        IEnumerable<string> AllCarBrands();

        IEnumerable<CarCategoryViewModel> GetCarCategories();

        void Add(CarInputModel input, string dealerId);

        bool Edit(string carId, CarInputModel input);
    }
}
