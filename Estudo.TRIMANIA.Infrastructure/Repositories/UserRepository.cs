using Dapper;
using Estudo.TRIMANIA.Domain.Entities;
using Estudo.TRIMANIA.Infrastructure.Database;
using System.Data;

namespace Estudo.TRIMANIA.Infrastructure.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(IDbConnection connection, TrimaniaContext context) : base(connection, context)
        {
        }

        public async Task<User?> GetUserByLogin(string? login)
        {
            string query = @"SELECT 
                                u.Id,
                                u.CPF,
                                u.Name,
                                u.Login,
                                u.Email,
                                u.Password,
                                u.Birthday,
                                u.UpdatedAt,
                                u.DeletedAt,
                                u.creation_date,
                                u.Identification
                            FROM
                                [User] u 
                             WHERE 
                                u.Login = @login;";

            var user = await _connection.QueryFirstOrDefaultAsync<User>(query, new { login });

            return user;
        }

        public async Task<IEnumerable<User>> GetUsersPagination(int page, int pageSize)
        {
            string query = $@"SELECT 
                                 u.Id,
                                u.CPF,
                                u.Name,
                                u.Login,
                                u.Email,
                                u.Password,
                                u.Birthday,
                                u.UpdatedAt,
                                u.DeletedAt,
                                u.creation_date,
                                u.Identification
                            FROM
                                [User] u 
                            ORDER BY 
                                u.Id 
                            OFFSET 
                                {page * pageSize} ROWS  
                            FETCH NEXT 
                                {pageSize} ROWS ONLY";

            var users = await _connection.QueryAsync<User>(query);

            return users;
        }

        public async Task<User?> GetUserByEmailOrNameOrLogin(string? filter)
        {
            string query = $@"SELECT 
                                 u.Id,
                                u.CPF,
                                u.Name,
                                u.Login,
                                u.Email,
                                u.Password,
                                u.Birthday,
                                u.UpdatedAt,
                                u.DeletedAt,
                                u.creation_date,
                                u.Identification
                            FROM
                                [User] u 
                            WHERE 
                                ( u.Name like('%{filter}%') OR 
                                u.Email like('%{filter}%') OR 
                                u.Login like('%{filter}%') ) AND
                                u.DeletedAt is null";

            var user = await _connection.QueryFirstOrDefaultAsync<User>(query);

            return user;
        }

        public async Task<User?> GetUserByIdentification(Guid identification)
        {
            string query = @"SELECT 
                                u.Id,
                                u.CPF,
                                u.Name,
                                u.Login,
                                u.Email,
                                u.Password,
                                u.Birthday,
                                u.UpdatedAt,
                                u.DeletedAt,
                                u.creation_date,
                                u.Identification,
                                a.Id,
                                a.City,
                                a.State,
                                a.Street,
                                a.UserId,
                                a.Number,
                                a.UpdatedAt,
                                a.DeletedAt,
                                a.Neighborhood,
                                a.creation_date
                            FROM
                                [User] u 
                            INNER JOIN
                                Address a ON a.UserId = u.Id
                            WHERE
                                u.Identification = @Identification AND 
                                u.DeletedAt is null;";

            var data = await _connection.QueryAsync<User, Address, User>(query, (user, address) => 
            { 
                user.SetAddress(address); 
                return user; 
            }, new {identification});

            var user = data.FirstOrDefault();

            return user;
        }
    }

    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<User?> GetUserByIdentification(Guid guid);
        Task<User?> GetUserByLogin(string? login);
        Task<User?> GetUserByEmailOrNameOrLogin(string? filter);
        Task<IEnumerable<User>> GetUsersPagination(int page, int pageSize);
    }
}
