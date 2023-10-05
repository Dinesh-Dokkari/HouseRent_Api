using HappyHouse.Models;
using HappyHouse.Services.IServices;
using HappyHouse_Utility;
using Microsoft.AspNetCore.Mvc;
using static HappyHouse_Utility.Static_Details;

namespace HappyHouse.Services
{
    public class HouseService : BaseService, IHouseService
    {
        private readonly IHttpClientFactory _ClientFactory;
        private string HouseUrl;
        public HouseService(IHttpClientFactory clientFactory,IConfiguration configuration) : base(clientFactory)
        {

            _ClientFactory = clientFactory;
            HouseUrl = configuration.GetValue<string>("ServiceUrls:HouseRent_Api");

        }
        public Task<T> CreateAsync<T>(HouseCreateDto dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = Static_Details.Api_Type.POST,
                Data = dto,
                Url = HouseUrl + "/api/HouseAPI"

            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = Static_Details.Api_Type.DELETE,
                Url = HouseUrl + "/api/HouseAPI/" + id

            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = Static_Details.Api_Type.GET,
                Url = HouseUrl + "/api/HouseAPI",
               

            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = Static_Details.Api_Type.GET,
                Url = HouseUrl + "/api/HouseAPI/"+id

            });
        }

        public Task<T> UpdateAsync<T>(HouseUpdateDto dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = Static_Details.Api_Type.PUT,
                Data = dto,
                Url = HouseUrl + "/api/HouseAPI/"+dto.Id


            });
        }
    }
}
