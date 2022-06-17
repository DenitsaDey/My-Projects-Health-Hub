namespace WebApplicationTemplate.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Car;

    public class Car
    {
        //guid is slower in the db but it prevents the id to be guessed(foreach-ed) in the url as sequenced numbers
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(CarBrandMaxLength)]
        public string Brand { get; set; }

        [Required]
        [MaxLength(CarModelMaxLength)]
        public string Model { get; set; }

        [Required]
        [MaxLength(CarDescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public string ImgUrl { get; set; }

        public int Year { get; set; }

        public string CategoryId { get; set; }

        //"virual" for lazy loading
        public virtual Category Category { get; set; }

        public string DealerId { get; set; }

        public virtual Dealer Dealer { get; set; }
    }
}
