using AutoMapper;
using E_Commerce_Mvc.Models;
using E_Commerce_Shared.DTO;
using E_Commerce_Shared.Entity;

namespace E_Commerce_Mvc.MappingProcess
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order,OrderDTO>().ReverseMap();
            CreateMap<OrderDTO,OrderCheckoutModel>().ReverseMap();
            CreateMap<Star, StarDTO>().ReverseMap();
        }
    }
}
