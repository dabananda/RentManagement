using RentManagement.Api.DTOs;

namespace RentManagement.Api.Interfaces
{
    public interface IAuthService
    {
        Task<Tuple<bool, string>> RegisterUserAsync(RegisterDto model);
        Task<Tuple<bool, string, string>> LoginUserAsync(LoginDto model);
        Task<bool> ConfirmEmailAsync(string userId, string token);
    }
}
