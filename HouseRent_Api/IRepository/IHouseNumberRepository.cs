using HouseRent_Api.Models;

namespace HouseRent_Api.IRepository
{
    public interface IHouseNumberRepository : IRepository<HouseNumber>
    {
        Task<HouseNumber> UpdateAsync(HouseNumber entity);
    }
}
