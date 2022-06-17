
namespace WebApplicationTemplate.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.Category;
    public class Category
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(CategoryNameMaxLength)]
        public string Name { get; set; }

        public virtual IEnumerable<Car> Cars { get; set; } = new HashSet<Car>();
    }
}
