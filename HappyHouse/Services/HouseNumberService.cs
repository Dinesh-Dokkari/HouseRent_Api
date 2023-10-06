using HappyHouse.Models;
using HappyHouse.Services.IServices;
using HappyHouse_Utility;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using static HappyHouse_Utility.Static_Details;

namespace HappyHouse.Services
{
    public class HouseNumberService : BaseService, IHouseNumberService
    {
        private readonly IHttpClientFactory _ClientFactory;
        private string HouseUrl;
        public HouseNumberService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {

            _ClientFactory = clientFactory;
            HouseUrl = configuration.GetValue<string>("ServiceUrls:HouseRent_Api");

        }
        public Task<T> CreateAsync<T>(HouseNumberCreateDto dto,string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = Static_Details.Api_Type.POST,
                Data = dto,
                Url = HouseUrl + "/api/HouseNumberAPI",
                Token = token

            });
        }

        public Task<T> DeleteAsync<T>(int HouseNo, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = Static_Details.Api_Type.DELETE,
                Url = HouseUrl + "/api/HouseNumberAPI/" + HouseNo,
                Token = token

            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = Static_Details.Api_Type.GET,
                Url = HouseUrl + "/api/HouseNumberAPI",
                Token = token

            });
        }

        public Task<T> GetAsync<T>(int HouseNo, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = Static_Details.Api_Type.GET,
                Url = HouseUrl + "/api/HouseNumberAPI/" + HouseNo,
                Token = token

            });
        }

        public Task<T> UpdateAsync<T>(HouseNumberUpdateDto dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = Static_Details.Api_Type.PUT,
                Data = dto,
                Url = HouseUrl + "/api/HouseNumberAPI/" + dto.HouseNo,
                Token = token


            });
        }
    }
}
