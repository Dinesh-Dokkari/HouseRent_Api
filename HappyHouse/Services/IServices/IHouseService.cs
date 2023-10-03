using HappyHouse.Models;

namespace HappyHouse.Services.IServices
{
    public interface IHouseService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(HouseCreateDto dto);
        Task<T> UpdateAsync<T>(HouseUpdateDto dto);
        Task<T> DeleteAsync<T>(int id);
    }
}
