namespace WebApplicationTemplate.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.Dealer;

    public class Dealer
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(DealerNameMaxLength)]
        public string Name { get; set; }

        public string PhoneNo { get; set; }

        public string UserId { get; set; }

        public virtual IdentityUser User { get; set; }

        public IEnumerable<Car> CarsOwned { get; set; } = new HashSet<Car>();
    }
}
