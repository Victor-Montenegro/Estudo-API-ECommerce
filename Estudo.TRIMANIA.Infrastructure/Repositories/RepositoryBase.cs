using Estudo.TRIMANIA.Domain.Entities;
using Estudo.TRIMANIA.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace Estudo.TRIMANIA.Infrastructure.Repositories
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
        where TEntity : EntityBase
    {
        private DbSet<TEntity> _dbSet;
        private readonly TrimaniaContext _context;
        protected readonly IDbConnection _connection;

        protected RepositoryBase(IDbConnection connection, TrimaniaContext context)
        {
            _context = context;
            _connection = connection;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<TEntity?> Get(int id)
            => await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

        public async Task Insert(TEntity entity, bool unitOfWork = true)
        {
            IDbContextTransaction? transaction = _context.Database.CurrentTransaction;

            if (!unitOfWork)
                transaction = _context.Database.BeginTransaction();

            try
            {
                entity.UpdatedAt = DateTime.UtcNow;
                entity.creation_date = DateTime.UtcNow;

                await _dbSet.AddAsync(entity);

                await _context.SaveChangesAsync();

                if (!unitOfWork)
                    await transaction.CommitAsync();
            }
            catch (Exception)
            {
                if (!unitOfWork)
                    await transaction.RollbackAsync();

                throw;
            }
        }

        public async Task Update(TEntity entity, bool unitOfWork = true)
        {
            var transaction = _context.Database.CurrentTransaction;

            if (!unitOfWork)
                transaction = _context.Database.BeginTransaction();

            try
            {
                entity.UpdatedAt = DateTime.UtcNow;

                _dbSet.Update(entity);

                await _context.SaveChangesAsync();

                if (!unitOfWork)
                    await transaction.CommitAsync();
            }
            catch (Exception)
            {
                if (!unitOfWork)
                    await transaction.RollbackAsync();

                throw;
            }
        }

        public async Task Delete(TEntity entity, bool unitOfWork = true)
        {
            var transaction = _context.Database.CurrentTransaction;

            if (!unitOfWork)
                transaction = _context.Database.BeginTransaction();

            try
            {
                entity.DeletedAt = DateTime.UtcNow;

                _dbSet.Update(entity);

                await _context.SaveChangesAsync();

                if (!unitOfWork)
                    await transaction.CommitAsync();
            }
            catch (Exception)
            {
                if (!unitOfWork)
                    await transaction.RollbackAsync();

                throw;
            }
        }

        public async Task DeleteBatch(IEnumerable<TEntity> entities, bool unitOfWork = true)
        {
            var transaction = _context.Database.CurrentTransaction;

            if (!unitOfWork)
                transaction = _context.Database.BeginTransaction();

            try
            {
                foreach (var entity in entities)
                    entity.DeletedAt = DateTime.UtcNow;

                _dbSet.UpdateRange(entities);

                await _context.SaveChangesAsync();

                if (!unitOfWork)
                    await transaction.CommitAsync();
            }
            catch (Exception)
            {
                if (!unitOfWork)
                    await transaction.RollbackAsync();

                throw;
            }
        }

        public async Task UpdateBatch(IEnumerable<TEntity> entities, bool unitOfWork = true)
        {
            var transaction = _context.Database.CurrentTransaction;

            if (!unitOfWork)
                transaction = _context.Database.BeginTransaction();

            try
            {
                foreach (var entity in entities)
                    entity.DeletedAt = DateTime.UtcNow;

                _dbSet.UpdateRange(entities);

                await _context.SaveChangesAsync();

                if (!unitOfWork)
                    await transaction.CommitAsync();
            }
            catch (Exception)
            {
                if (!unitOfWork)
                    await transaction.RollbackAsync();

                throw;
            }
        }

        public async Task InsertBatch(IEnumerable<TEntity> entities, bool unitOfWork = true)
        {
            IDbContextTransaction? transaction = _context.Database.CurrentTransaction;

            if (!unitOfWork)
                transaction = _context.Database.BeginTransaction();

            try
            {

                foreach (var entity in entities)
                {
                    entity.UpdatedAt = DateTime.UtcNow;
                    entity.creation_date = DateTime.UtcNow;
                }

                await _dbSet.AddRangeAsync(entities);

                await _context.SaveChangesAsync();

                if (!unitOfWork)
                    await transaction.CommitAsync();
            }
            catch (Exception)
            {
                if (!unitOfWork)
                    await transaction.RollbackAsync();

                throw;
            }
        }
    }

    public interface IRepositoryBase<TEntity>
        where TEntity : EntityBase
    {
        public Task<TEntity?> Get(int id);
        public Task Insert(TEntity entity, bool unitOfWork = true);
        public Task Update(TEntity entity, bool unitOfWork = true);
        public Task Delete(TEntity entity, bool unitOfWork = true);
        public Task DeleteBatch(IEnumerable<TEntity> entities, bool unitOfWork = true);
        public Task UpdateBatch(IEnumerable<TEntity> entities, bool unitOfWork = true);
        public Task InsertBatch(IEnumerable<TEntity> entities, bool unitOfWork = true);
    }
}
