using System.Threading.Tasks;

namespace JinnoVision.Domain.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Returns a user if username/password match and user is active; otherwise null.
        /// </summary>
        Task<Entities.User> GetByCredentialsAsync(string username, string password);
    }
}
