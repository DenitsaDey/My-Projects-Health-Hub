namespace WebApplicationTemplate.Infrastructure
{
    using AutoMapper;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using WebApplicationTemplate.Data.Models;
    using WebApplicationTemplate.Models.Cars;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<CarInputModel, Car>()
                .ReverseMap(); //optional if needed
            this.CreateMap<Car, CarDetailsViewModel>()
                .ForMember(c => c.UserId, cfg => cfg.MapFrom(c => c.Dealer.UserId));
        }
    }
}
