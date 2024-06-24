using AutoMapper;
using Estudo.TRIMANIA.Domain.Commands.Carts;
using Estudo.TRIMANIA.Domain.Entities;

namespace Estudo.TRIMANIA.Domain.AutoMappers
{
    internal class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, CreateShoppingCartResponse>()
                .ForMember(response => response.OrderId, order => order.MapFrom(m => m.Id))
                .ForMember(response => response.Items, order => order.MapFrom(m => m.Items))
                .ForMember(response => response.TotalValue, order => order.MapFrom(m => m.total_value))
                ;
        }
    }
}
