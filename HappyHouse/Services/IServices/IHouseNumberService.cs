using HappyHouse.Models;

namespace HappyHouse.Services.IServices
{
    public interface IHouseNumberService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(HouseNumberCreateDto dto);
        Task<T> UpdateAsync<T>(HouseNumberUpdateDto dto);
        Task<T> DeleteAsync<T>(int id);
    }
}