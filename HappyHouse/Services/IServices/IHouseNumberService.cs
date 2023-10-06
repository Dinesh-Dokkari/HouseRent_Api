using HappyHouse.Models;

namespace HappyHouse.Services.IServices
{
    public interface IHouseNumberService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(HouseNumberCreateDto dto, string token);
        Task<T> UpdateAsync<T>(HouseNumberUpdateDto dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
    }
}