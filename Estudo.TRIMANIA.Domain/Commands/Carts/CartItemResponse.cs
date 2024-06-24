namespace Estudo.TRIMANIA.Domain.Commands.Carts
{
    public class CartItemResponse
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int ProductId { get; set; }
    }
}