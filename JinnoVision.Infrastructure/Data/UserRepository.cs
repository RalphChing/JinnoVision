using System.Data;
using System.Threading.Tasks;
using Dapper;
using JinnoVision.Domain.Entities;
using JinnoVision.Domain.Interfaces;

namespace JinnoVision.Infrastructure.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DbConnectionFactory _connectionFactory;

        public UserRepository(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<User> GetByCredentialsAsync(string username, string password)
        {
            const string sql = @"
                SELECT TOP 1 
                    U.UserId AS Id,
                    U.Username,
                    U.Password,
                    U.IsActive,
                    R.RoleName AS Role
                FROM Users U
                LEFT JOIN UserRoles UR ON UR.UserId = U.UserId
                LEFT JOIN Roles R ON R.RoleId = UR.RoleId
                WHERE U.Username = @Username
                  AND U.Password = @Password
                  AND U.IsActive = 1;
            ";

            using (IDbConnection conn = _connectionFactory.CreateConnection())
            {
                return await conn.QueryFirstOrDefaultAsync<User>(
                    sql,
                    new { Username = username, Password = password }
                );
            }
        }
    }
}
