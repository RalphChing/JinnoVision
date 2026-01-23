using JinnoVision.App.Models;
using JinnoVision.Domain.Interfaces;
using JinnoVision.App.Interfaces;
using System.Threading.Tasks;

namespace JinnoVision.App.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<AuthenticatedUser> LoginAsync(string username, string password)
        {
            // here you can do:
            // - validation
            // - hashing
            // - audit logs
            var user = await _userRepository.GetByCredentialsAsync(username, password);

            if (user == null)
                return null;

            return new AuthenticatedUser
            {
                Id = user.Id,
                Username = user.Username,
                Role = user.Role
            };
        }
    }
}
