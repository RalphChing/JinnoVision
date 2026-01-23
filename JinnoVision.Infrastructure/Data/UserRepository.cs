using JinnoVision.Domain.Entities;
using JinnoVision.Domain.Interfaces;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

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
            using (IDbConnection conn = _connectionFactory.CreateConnection())
            {
                await ((SqlConnection)conn).OpenAsync();

                const string sql = @"
                    SELECT TOP 1 UserId, Username, Password, IsActive
                    FROM Users
                    WHERE Username = @Username
                      AND Password = @Password
                      AND IsActive = 1;
                ";

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;

                    var pUser = cmd.CreateParameter();
                    pUser.ParameterName = "@Username";
                    pUser.Value = username;
                    cmd.Parameters.Add(pUser);

                    var pPass = cmd.CreateParameter();
                    pPass.ParameterName = "@Password";
                    pPass.Value = password;
                    cmd.Parameters.Add(pPass);

                    using (var reader = await ((SqlCommand)cmd).ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new User
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("UserId")),
                                Username = reader.GetString(reader.GetOrdinal("Username")),
                                Password = reader.GetString(reader.GetOrdinal("Password")),
                                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
                            };
                        }
                    }
                }
            }

            return null;
        }
    }
}
