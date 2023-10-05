using HouseRent_Api.IRepository;
using HouseRent_Api.Models;
using HouseRent_Api.Models.DTO;
using HouseRent_Api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace HouseRent_Api.Controllers
{
    [Route("api/UsersAuth")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserRepository _uRepo;

        protected APIresponse _response;
        public UsersController(IUserRepository uRepo)
        {
            _uRepo = uRepo;
            this._response = new();

            
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var loginResponse = await _uRepo.Login(model);
            if(loginResponse.User==null || string.IsNullOrEmpty(loginResponse.Token))
            {
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("UserName or Password is Incorrect");
                return BadRequest(_response);

            }
            _response.StatusCode = System.Net.HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = loginResponse;
            return Ok(_response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterationRequestDto model)
        {
            bool ifUserUnique = _uRepo.IsUniqueUser(model.UserName);

            if (!ifUserUnique)
            {
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("UserName already exists");
                return BadRequest(_response);
            }
            var user = await _uRepo.Register(model);

            if (user == null)
            {
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Error while Registering");
                return BadRequest(_response);

            }
            _response.IsSuccess = true;
            _response.StatusCode = System.Net.HttpStatusCode.OK;
            return Ok(_response.Result);
        }
    }
}
