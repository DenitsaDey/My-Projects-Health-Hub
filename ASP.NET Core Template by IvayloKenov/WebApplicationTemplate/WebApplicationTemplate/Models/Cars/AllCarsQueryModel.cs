namespace WebApplicationTemplate.Models.Cars
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AllCarsQueryModel
    {
        public int CarsPerPage { get; set; } = 2;

        public int CurrentPage { get; set; } = 1; //if it has no value it starts from one so that in the carsQuery.Skip(query.CurrentPage - 1) does not give negative number

        public int TotalCars { get; set; } // needed to calculate when to disable the next page button in the view

        public string Brand { get; set; }

        public IEnumerable<string> Brands { get; set; }

        public string SearchTerm { get; set; }

        public CarSorting Sorting { get; set; }

        public IEnumerable<CarSummaryViewModel> Cars { get; set; }
    }
}
