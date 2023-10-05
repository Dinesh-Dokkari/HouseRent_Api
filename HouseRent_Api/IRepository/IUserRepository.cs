using HouseRent_Api.Models;
using HouseRent_Api.Models.DTO;

namespace HouseRent_Api.IRepository
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string username);

        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);

        Task<LocalUser> Register(RegisterationRequestDto RegisterationrequestDto);
    }
}
