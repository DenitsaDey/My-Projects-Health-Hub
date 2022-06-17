namespace WebApplicationTemplate.Models.Cars
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CarDetailsViewModel : CarSummaryViewModel
    {
        //this viewModel is created to be used when buttons View More, Edit, Delete are used and more details about the cars are needed
        public string Description { get; set; }

        public int Year { get; set; }

        public string UserId { get; set; } //needed when editing/deleting to make sure the logged user is th eone that is this particular car's dealer
    }
}
