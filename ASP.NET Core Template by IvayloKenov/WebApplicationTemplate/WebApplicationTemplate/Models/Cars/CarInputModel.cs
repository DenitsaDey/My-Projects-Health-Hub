namespace WebApplicationTemplate.Models.Cars
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Car;

    public class CarInputModel
    {
        
        [Required]
        /*
        [MinLength(CarBrandMinLength)] when doing ModelState validation this returns an error 
        [MaxLength(CarBrandMaxLength)] that is not very readable for the user,
        for this reason better use a different attribute to validate the business logic, like bellow:
        */
        [StringLength(CarBrandMaxLength, MinimumLength = CarBrandMinLength,
            ErrorMessage = "Custom Message With {1}")] // {0} is the fiels name, {1} the first value in the brackets, i.e. max length
        public string Brand { get; set; }

        [Required]
        /* we can specifically say where to get the data for the model binding,
         * otherwise it automatically tries by itself
        [FromQuery]
        [FromRoute]
        [FromBody] for ajax requests
        [BindNever] used for security purposes to disregard the client input
        */
        [MinLength(CarModelMinLength)]
        [MaxLength(CarModelMaxLength)]
        public string Model { get; set; }

        [Required]
        [MinLength(CarDescriptionMinLength)]
        [MaxLength(CarDescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [Url]
        public string ImgUrl { get; set; }

        [Range(CarYearMinValue, CarYearMaxValue)]
        public int Year { get; set; }

        [Display(Name ="Category")]
        //this is the selected category
        public string CategoryId { get; set; }

        //this is a collection of all categories to be visualised in the drop down menu
        public IEnumerable<CarCategoryViewModel> Categories { get; set; }

        public bool CheckBoxExample { get; set; }
    }
}
