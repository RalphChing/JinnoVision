using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace JinnoVision.Services
{
    public sealed class AuthResult
    {
        public bool Success { get; set; }
        public int CompanyId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string FailureReason { get; set; }
    }

    public class AuthService
    {
        private readonly string _cs;

        public AuthService()
        {
            _cs = ConfigurationManager.ConnectionStrings["JinnoDB"].ConnectionString;
        }

        public AuthResult Login(string companyName, string username, string passwordPlainText)
        {
            // Replace with real hashing verification later.
            // For now we treat PasswordHash = 0x010203 as "password" == "password".

            using (var conn = new SqlConnection(_cs))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                                SELECT TOP 1
                                    c.CompanyId,
                                    u.UserId,
                                    u.Username,
                                    u.PasswordHash,
                                    u.IsActive
                                FROM dbo.Companies c
                                JOIN dbo.Users u ON u.CompanyId = c.CompanyId
                                WHERE c.IsActive = 1
                                    AND u.IsDeleted = 0
                                    AND c.CompanyName = @CompanyName
                                    AND u.Username = @Username;
                                ";
                cmd.Parameters.Add("@CompanyName", SqlDbType.NVarChar, 150).Value = companyName;
                cmd.Parameters.Add("@Username", SqlDbType.NVarChar, 80).Value = username;

                conn.Open();
                using (var r = cmd.ExecuteReader())
                {
                    if (!r.Read())
                    {
                        return new AuthResult { Success = false, FailureReason = "Company/user not found." };
                    }

                    var companyId = r.GetInt32(0);
                    var userId = r.GetInt32(1);
                    var dbUsername = r.GetString(2);
                    var passwordHash = (byte[])r["PasswordHash"];
                    var isActive = (bool)r["IsActive"];

                    if (!isActive)
                    {
                        WriteAudit(companyId, userId, "LOGIN_FAILURE", 2, $"Login failed: user disabled ({dbUsername}).");
                        return new AuthResult { Success = false, FailureReason = "User is disabled." };
                    }

                    // DEMO verification: treat 'password' as valid if hash is 0x010203 (seed placeholder)
                    var ok = (passwordPlainText == "password") && passwordHash.Length == 3 &&
                             passwordHash[0] == 0x01 && passwordHash[1] == 0x02 && passwordHash[2] == 0x03;

                    if (!ok)
                    {
                        WriteAudit(companyId, userId, "LOGIN_FAILURE", 2, $"Login failed: invalid credentials ({dbUsername}).");
                        return new AuthResult { Success = false, FailureReason = "Invalid credentials." };
                    }

                    WriteAudit(companyId, userId, "LOGIN_SUCCESS", 1, $"User logged in ({dbUsername}).");

                    return new AuthResult
                    {
                        Success = true,
                        CompanyId = companyId,
                        UserId = userId,
                        Username = dbUsername
                    };
                }
            }
        }

        private void WriteAudit(int companyId, int? actorUserId, string eventType, byte severity, string message)
        {
            using (var conn = new SqlConnection(_cs))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                    INSERT INTO dbo.AuditEvents
                    (CompanyId, ActorUserId, EventType, Severity, SourceIp, MachineName, AppVersion, Message)
                    VALUES
                    (@CompanyId, @ActorUserId, @EventType, @Severity, NULL, @MachineName, @AppVersion, @Message);
                    ";
                cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = companyId;
                cmd.Parameters.Add("@ActorUserId", SqlDbType.Int).Value = (object)actorUserId ?? DBNull.Value;
                cmd.Parameters.Add("@EventType", SqlDbType.NVarChar, 60).Value = eventType;
                cmd.Parameters.Add("@Severity", SqlDbType.TinyInt).Value = severity;
                cmd.Parameters.Add("@MachineName", SqlDbType.NVarChar, 128).Value = Environment.MachineName;
                cmd.Parameters.Add("@AppVersion", SqlDbType.NVarChar, 32).Value = "0.1.0";
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 400).Value = message;

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
