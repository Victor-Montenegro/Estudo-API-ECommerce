using Estudo.TRIMANIA.Domain.Enums;

namespace Estudo.TRIMANIA.Domain.Commands.Carts
{
    public class CreateShoppingCartResponse
    {
        public int OrderId { get; set; }
        public decimal TotalValue { get; set; }
        public EOrderStatus Status { get; set; }
        public IEnumerable<CartItemResponse> Items { get; set; }

        public CreateShoppingCartResponse()
        {
            Items = new List<CartItemResponse>();
        }
    }
}