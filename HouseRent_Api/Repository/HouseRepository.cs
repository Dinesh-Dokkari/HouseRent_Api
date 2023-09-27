using HouseRent_Api.Data;
using HouseRent_Api.IRepository;
using HouseRent_Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace HouseRent_Api.Repository
{
    public class HouseRepository : Repository<House>, IHouseRepository  
    {
        private readonly ApplicationDbcontext _db;
        public HouseRepository(ApplicationDbcontext db) : base(db)
        { 
            _db = db;

        }
        
        public async Task<House> UpdateAsync(House entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.Houses.Update(entity);
            await _db.SaveChangesAsync();

            return entity;


        }
    }
}
