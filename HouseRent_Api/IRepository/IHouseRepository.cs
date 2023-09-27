using HouseRent_Api.Models;
using System.Linq.Expressions;

namespace HouseRent_Api.IRepository
{
    public interface IHouseRepository : IRepository<House>
    { 
        Task<House> UpdateAsync(House entity);
    }
}
