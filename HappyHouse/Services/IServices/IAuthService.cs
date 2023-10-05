using HappyHouse.Models;
using HappyHouse.Models.DTO;

namespace HappyHouse.Services.IServices
{
    public interface IAuthService
    {
        Task<T> LoginAsync<T>(LoginRequestDto objToLogin);

        Task<T> RegisterAsync<T>(RegisterationRequestDto objToRegister);
    }
}
