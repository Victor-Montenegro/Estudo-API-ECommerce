using Estudo.TRIMANIA.Domain.Enums;
using Estudo.TRIMANIA.Domain.Interfaces;

namespace Estudo.TRIMANIA.Domain.Entities
{
    public class Order : EntityBase, IAggregateRoot
    {
        public User? User { get;  set; }
        public int UserId { get; private set; }
        public decimal total_value { get; private set ; }
        public EOrderStatus Status { get; private set ; }
        public DateTime? cancel_date { get; private set ; }
        public List<OrderItem>? Items { get; private set ; }
        public DateTime? finished_date { get; private set ; }

        public void AddItems(OrderItem orderItem)
        {
            if(Items == null)
                Items = new List<OrderItem>();

            Items.Add(orderItem);
        }

        public void InitNewOrder(int userId, List<OrderItem> items)
        {
            Items = items;
            UserId = userId;
            Status = EOrderStatus.Progress;

            CalculateAmountOrder();
        }

        public void CalculateAmountOrder()
        {
            total_value = Items.Sum(s => s.CalculateProductAmount());
        }

        public void ItemsUpdate(List<OrderItem> orderItems)
        {
            InitNewOrder(UserId, orderItems);
        }
    }
}
