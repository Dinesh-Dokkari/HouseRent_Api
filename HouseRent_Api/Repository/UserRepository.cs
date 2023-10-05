using AutoMapper;
using HouseRent_Api.Data;
using HouseRent_Api.IRepository;
using HouseRent_Api.Models;
using HouseRent_Api.Models.DTO;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HouseRent_Api.Repository
{
    public class UserRepository : IUserRepository
    {
        private ApplicationDbcontext _db;

        private IMapper _mapper;

        private string secretKey;
        public UserRepository(ApplicationDbcontext db, IMapper mapper,IConfiguration configuration)
        {
            _db = db;
            _mapper = mapper;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");

        }
        public bool IsUniqueUser(string username)
        {
            var user = _db.LocalUsers.FirstOrDefault(x=>x.UserName == username);
            if(user == null)
            {
                return true;
            }
            return false;

        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = _db.LocalUsers.FirstOrDefault(u=>u.UserName.ToLower() == loginRequestDto.UserName.ToLower() 
            && u.Password == loginRequestDto.Password);

            if(user == null)
            {
                return new LoginResponseDto()
                {
                    Token = "",
                    User = null
                };
            }
            //if user was found generate JWT Token

            var tokenhandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,user.Id.ToString()),
                    new Claim(ClaimTypes.Role,user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };


            var token = tokenhandler.CreateToken(tokenDescriptor);

            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                Token = tokenhandler.WriteToken(token),
                User = user

            };
            return loginResponseDto;
        }

        public async Task<LocalUser> Register(RegisterationRequestDto RegisterationrequestDto)
        {
            //LocalUser user = new LocalUser();

            //    new()
            //{
            //    UserName = RegisterationrequestDto.UserName,
            //    Password = RegisterationrequestDto.Password,
            //    Name = RegisterationrequestDto.Name,
            //    Role = RegisterationrequestDto.Role,
            //};
            var user = _mapper.Map<LocalUser>(RegisterationrequestDto);
            _db.LocalUsers.Add(user);
            await _db.SaveChangesAsync();
            user.Password = "";
            return user;



        }
    }
}
