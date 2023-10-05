using HappyHouse.Models.DTO;

namespace HappyHouse.Models
{
    public class LoginResponseDto
    {
        public UserDto User { get; set; }
        public string Token { get; set; }
    }
}
