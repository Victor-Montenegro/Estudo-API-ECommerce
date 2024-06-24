using Dapper;
using Estudo.TRIMANIA.Domain.Entities;
using Estudo.TRIMANIA.Infrastructure.Database;
using System.Data;

namespace Estudo.TRIMANIA.Infrastructure.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(IDbConnection connection, TrimaniaContext context) : base(connection, context)
        {
        }

        public async Task<Product?> GetProductById(int productId)
        {
            string query = @"SELECT 
                                Id, 
                                creation_date, 
                                UpdatedAt, 
                                DeletedAt, 
                                Name, 
                                Quantity, 
                                Price, 
                                Description
                            FROM 
                                Product p 
                            WHERE 
                                p.DeletedAt IS NULL AND
                                p.Id = @ProductId;";

            var product = await _connection.QueryFirstOrDefaultAsync<Product>(query, new { productId });

            return product;
        }
    }

    public interface IProductRepository
    {
        Task<Product?> GetProductById(int productId);
    }
}
