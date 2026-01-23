using JinnoVision.App.Models;
using System.Threading.Tasks;

namespace JinnoVision.App.Interfaces
{
    public interface IAuthService
    {
        Task<AuthenticatedUser> LoginAsync(string username, string password);
    }
}
