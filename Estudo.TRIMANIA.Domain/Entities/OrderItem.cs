

namespace Estudo.TRIMANIA.Domain.Entities
{
    public class OrderItem : EntityBase
    {
        public int OrderId { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }
        public int product_id { get; private set; }

        public decimal CalculateProductAmount()
            => Quantity * Price;

        public void SetPrice(decimal price)
        {
            Price = price;
        }
    }
}
