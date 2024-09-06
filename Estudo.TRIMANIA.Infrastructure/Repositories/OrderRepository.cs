using Dapper;
using Estudo.TRIMANIA.Domain.Entities;
using Estudo.TRIMANIA.Domain.Enums;
using Estudo.TRIMANIA.Infrastructure.Database;
using System.Data;

namespace Estudo.TRIMANIA.Infrastructure.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(IDbConnection connection, TrimaniaContext context) : base(connection, context)
        {
        }

        public async Task<Order?> GetOrderById(int orderId)
        {
            string query = @"SELECT 
                    o.Id, 
                    o.UserId, 
                    o.Status, 
                    o.UpdatedAt, 
                    o.DeletedAt, 
                    o.total_value, 
                    o.cancel_date, 
                    o.finished_date,
                    o.creation_date, 
                    oi.Id, 
                    oi.Price,
                    oi.OrderId, 
                    oi.Quantity, 
                    oi.UpdatedAt, 
                    oi.DeletedAt, 
                    oi.product_id, 
                    oi.creation_date,
                    u.Id, 
                    u.CPF, 
                    u.Name, 
                    u.Email, 
                    u.[Login], 
                    u.Password,
                    u.Birthday, 
                    u.UpdatedAt, 
                    u.DeletedAt, 
                    u.creation_date, 
                    u.Identification 
                FROM 
                    [Order] o
                INNER JOIN
                    [User] u on u.Id = o.UserId
                INNER JOIN
                    OrderItem oi on oi.OrderId = o.Id
                WHERE 
                    o.Id = @OrderId AND
                    oi.DeletedAt is null AND 
                    o.DeletedAt is null AND 
                    u.DeletedAt is null;";

            var lookUp = new Dictionary<int, Order>();

            await _connection.QueryAsync<Order, OrderItem, User, Order>(query, (order, orderItem, user) =>
            {

                lookUp.TryGetValue(order.Id, out var value);

                if (value is not null)
                    value.AddItems(orderItem);

                if (value is null)
                {
                    order.User = user;
                    order.AddItems(orderItem);

                    lookUp.Add(order.Id, order);
                }

                return order;
            }, new { orderId });

            var order = lookUp.FirstOrDefault().Value;

            return order;
        }

        public async Task<bool> UserAreThereOrders(int userId)
        {
            string? query = @"SELECT TOP(1) Id FROM [Order] where UserId = @UserId";

            var orderId = await _connection.ExecuteScalarAsync<int>(query, new { userId });

            return orderId > 0;
        }

        public async Task<Order?> GetOrderByUserIdAndStatus(int userId, EOrderStatus orderStatus)
        {
            string query = @"SELECT 
                    o.Id, 
                    o.UserId, 
                    o.Status, 
                    o.UpdatedAt, 
                    o.DeletedAt, 
                    o.total_value, 
                    o.cancel_date, 
                    o.finished_date,
                    o.creation_date, 
                    oi.Id, 
                    oi.Price,
                    oi.OrderId, 
                    oi.Quantity, 
                    oi.UpdatedAt, 
                    oi.DeletedAt, 
                    oi.product_id, 
                    oi.creation_date,
                    u.Id, 
                    u.CPF, 
                    u.Name, 
                    u.Email, 
                    u.[Login], 
                    u.Password,
                    u.Birthday, 
                    u.UpdatedAt, 
                    u.DeletedAt, 
                    u.creation_date, 
                    u.Identification 
                FROM 
                    [Order] o
                INNER JOIN
                    [User] u on u.Id = o.UserId
                INNER JOIN
                    OrderItem oi on oi.OrderId = o.Id
                WHERE 
                    o.UserId = @UserId AND
                    o.Status = @OrderStatus AND
                    oi.DeletedAt is null AND 
                    o.DeletedAt is null AND 
                    u.DeletedAt is null;";

            var lookUp = new Dictionary<int, Order>();

            await _connection.QueryAsync<Order, OrderItem, User, Order>(query, (order, orderItem, user) =>
            {

                lookUp.TryGetValue(order.Id, out var value);

                if (value is not null)
                    value.AddItems(orderItem);

                if (value is null)
                {
                    order.User = user;
                    order.AddItems(orderItem);

                    lookUp.Add(order.Id, order);
                }

                return order;
            }, new { userId, orderStatus });

            var order = lookUp.FirstOrDefault().Value;

            return order;
        }

        public async Task DeleteItemsBatch(Order order, bool unitOfWork = true)
        {
            await DeleteAggregateBatch(order.Items, unitOfWork);
        }
    }

    public interface IOrderRepository : IRepositoryBase<Order>
    {
        Task<Order?> GetOrderById(int orderId);
        Task<bool> UserAreThereOrders(int userId);
        Task<Order?> GetOrderByUserIdAndStatus(int userId, EOrderStatus orderStatus);
        Task DeleteItemsBatch(Order order, bool unitOfWork = true);
    }
}
