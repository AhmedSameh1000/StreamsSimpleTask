using SimpleTask.BAL.DTOs;
using SimpleTask.DAL.Domains;

namespace SimpleTask.BAL.Services.Interfaces
{
    public interface IAuthServices
    {
        Task<AuthModel> RegisterAsync(RegisterDto model);

        Task<AuthModel> LoginAsync(LogInDTo model);

        Task<string> GenerateToken(ApplicationUser user);
    }
}