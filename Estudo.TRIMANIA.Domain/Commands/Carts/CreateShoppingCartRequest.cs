using Estudo.TRIMANIA.Domain.Commands.Bases;
using MediatR;

namespace Estudo.TRIMANIA.Domain.Commands.Carts
{
    public class CreateShoppingCartRequest : AuthorizeBaseRequest, IRequest<CreateShoppingCartResponse>
    {
        public List<CartItemRequest> Items { get; set; }

        public CreateShoppingCartRequest()
        {
            Items = new List<CartItemRequest>();
        }
    }
}
