namespace WebApplicationTemplate.Models.Cars
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CarSummaryViewModel
    {
        public string Id { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string ImgUrl { get; set; }

        public string Category { get; set; }
    }
}
