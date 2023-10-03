using static HappyHouse_Utility.Static_Details;

namespace HappyHouse.Models
{
    public class APIRequest
    {
        public Api_Type ApiType { get; set; } = Api_Type.GET;
        public string Url { get; set; } 
        public object Data { get; set; }
    }
}
