namespace WebApplicationTemplate.Models.Dealers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants.Dealer;

    public class BecomeDealerInputModel
    {
        [Required]
        [StringLength(DealerNameMaxLength, MinimumLength = DealerNameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(PhoneNoMaxLength, MinimumLength = PhoneNoMinLength)]
        [Display(Name = "Phone number")]
        public string PhoneNo { get; set; }
    }
}
