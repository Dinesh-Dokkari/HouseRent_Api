using HappyHouse.Models;
using HappyHouse.Models.DTO;
using HappyHouse.Services.IServices;
using HappyHouse_Utility;
using Humanizer;

namespace HappyHouse.Services
{
    public class AuthService : BaseService,IAuthService
    {
        private readonly IHttpClientFactory _ClientFactory;
        private string HouseUrl;
        public AuthService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {

            _ClientFactory = clientFactory;
            HouseUrl = configuration.GetValue<string>("ServiceUrls:HouseRent_Api");

        }
        public Task<T> LoginAsync<T>(LoginRequestDto objToLogin)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = Static_Details.Api_Type.POST,
                Data = objToLogin,
                Url = HouseUrl + "/api/UsersAuth/Login"

            });
        }

        public Task<T> RegisterAsync<T>(RegisterationRequestDto objToRegister)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = Static_Details.Api_Type.POST,
                Data = objToRegister,
                Url = HouseUrl + "/api/UsersAuth/Register"

            });


        }
    }
}
