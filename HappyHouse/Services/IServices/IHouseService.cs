using HappyHouse.Models;

namespace HappyHouse.Services.IServices
{
    public interface IHouseService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(HouseCreateDto dto, string token);
        Task<T> UpdateAsync<T>(HouseUpdateDto dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
    }
}
