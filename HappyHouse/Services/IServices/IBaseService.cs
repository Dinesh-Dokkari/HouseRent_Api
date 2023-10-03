using HappyHouse.Models;

namespace HappyHouse.Services.IServices
{
    public interface IBaseService
    {
        APIresponse responseModel { get; set; }

        Task<T> SendAsync<T>(APIRequest apiRequest);
    }
}
