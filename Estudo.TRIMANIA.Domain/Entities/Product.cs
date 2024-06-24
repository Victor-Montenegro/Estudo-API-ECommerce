using Estudo.TRIMANIA.Domain.Interfaces;

namespace Estudo.TRIMANIA.Domain.Entities
{
    public class Product : EntityBase, IAggregateRoot
    {
        public string? Name { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }
        public string? Description { get; private set; }

        public bool IsThereStock(int quantity)
            => Quantity < quantity;
    }
}
