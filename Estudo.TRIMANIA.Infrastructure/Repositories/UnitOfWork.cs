using Estudo.TRIMANIA.Infrastructure.Database;
using Microsoft.EntityFrameworkCore.Storage;

namespace Estudo.TRIMANIA.Infrastructure.Repositories
{
    public sealed class UnitOfWork : IUnitOfWork, IDisposable
    {
        private IDbContextTransaction? _transaction;
        private readonly TrimaniaContext _context;

        public IUserRepository UserRepository { get; }
        public IOrderRepository OrderRepository { get; }
        public IProductRepository ProductRepository { get; }

        public UnitOfWork(TrimaniaContext context,
            IUserRepository userRepository, 
            IOrderRepository orderRepository,
            IProductRepository productRepository)
        {
            _context = context;
            UserRepository = userRepository;
            OrderRepository = orderRepository;
            ProductRepository = productRepository;
        }

        public Task Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch (Exception)
            {
                RollBack();
                throw;
            }

            return Task.CompletedTask;
        }

        public Task RollBack()
        {
            _transaction?.Rollback();

            return Task.CompletedTask;
        }

        public Task BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _context?.Dispose();
            _transaction?.Dispose();
        }
    }

    public interface IUnitOfWork
    {
        public Task Commit();
        public Task RollBack();
        public Task BeginTransaction();

        public IUserRepository UserRepository { get; }
        public IOrderRepository OrderRepository { get; }
        public IProductRepository ProductRepository { get; }
    }
}
