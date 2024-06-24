using Estudo.TRIMANIA.Domain.Commands.Bases;
using MediatR;

namespace Estudo.TRIMANIA.Domain.Commands.Carts
{
    public class UpdateShoppingCartRequest : AuthorizeBaseRequest, IRequest<CreateShoppingCartResponse>
    {
        public int OrderId { get; set; }

        public List<CartItemRequest> Items { get; set; }

        public UpdateShoppingCartRequest()
        {
            Items = new List<CartItemRequest>();
        }
    }
}
