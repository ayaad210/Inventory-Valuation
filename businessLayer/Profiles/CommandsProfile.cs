
using AutoMapper;
using businessLayer.Dtos.Item;
using businessLayer.Dtos.Order;
using businessLayer.Dtos.Request;
using DataAccessLayer.Models;

namespace businessLayer.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            // Source -> Target
            CreateMap<Item, ItemReadDto>();
            CreateMap<ItemCreateDto, Item>();

            CreateMap<RequestCreateDto, Request>();
            CreateMap< Request, RequestCreateDto> ();

            CreateMap<OrderCreateDto,  Order>();
            CreateMap<Order, OrderCreateDto>();



        }
    }
}