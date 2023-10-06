using HappyHouse.Models;
using HappyHouse.Services.IServices;
using HappyHouse_Utility;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace HappyHouse.Services
{
    public class BaseService : IBaseService
    {
        public APIresponse responseModel { get; set; }

        public HttpClient client { get; set; }

        public IHttpClientFactory _httpClient { get; set; }
        public BaseService(IHttpClientFactory httpClient)
        {
            this.responseModel = new();
            _httpClient = httpClient;
        }
        public async Task<T> SendAsync<T>(APIRequest apiRequest)
        {
            try
            {
                var client = _httpClient.CreateClient("MagicAPI");
               
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);
                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                        Encoding.UTF8, "application/json");
                }
                switch (apiRequest.ApiType)
                {
                    case Static_Details.Api_Type.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case Static_Details.Api_Type.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case Static_Details.Api_Type.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;

                }

                HttpResponseMessage apiResponse = null;

                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiRequest.Token);

                if (!string.IsNullOrEmpty(apiRequest.Token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiRequest.Token);
                }

                apiResponse = await client.SendAsync(message);
                

                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                //try
                //{
                //    APIResponse ApiResponse = JsonConvert.DeserializeObject<APIResponse>(apiContent);
                //    if (ApiResponse != null && (apiResponse.StatusCode == System.Net.HttpStatusCode.BadRequest
                //        || apiResponse.StatusCode == System.Net.HttpStatusCode.NotFound))
                //    {
                //        ApiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                //        ApiResponse.IsSuccess = false;
                //        var res = JsonConvert.SerializeObject(ApiResponse);
                //        var returnObj = JsonConvert.DeserializeObject<T>(res);
                //        return returnObj;
                //    }
                //}
                //catch (Exception e)
                //{
                //    var exceptionResponse = JsonConvert.DeserializeObject<T>(apiContent);
                //    return exceptionResponse;
                //}
                var APIResponse = JsonConvert.DeserializeObject<T>(apiContent);
                return APIResponse;

            }
            catch (Exception e)
            {
                var dto = new APIresponse
                {
                    ErrorMessages = new List<string> { Convert.ToString(e.Message) },
                    IsSuccess = false
                };
                var res = JsonConvert.SerializeObject(dto);
                var APIResponse = JsonConvert.DeserializeObject<T>(res);
                return APIResponse;
            }
        }
    }
}
