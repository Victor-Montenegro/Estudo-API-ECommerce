using AutoMapper;
using Estudo.TRIMANIA.Domain.Commands.Carts;
using Estudo.TRIMANIA.Domain.Entities;

namespace Estudo.TRIMANIA.Domain.AutoMappers
{
    internal class OrderItemProfile : Profile
    {
        public OrderItemProfile()
        {
            CreateMap<CartItemRequest, OrderItem>()
                .ForMember(orderItem => orderItem.product_id, request => request.MapFrom(m => m.ProductId));
                ;

            CreateMap<OrderItem, CartItemResponse>()
                .ForMember(response => response.ProductId, orderItem => orderItem.MapFrom(m => m.product_id))
                ;
        }
    }
}
