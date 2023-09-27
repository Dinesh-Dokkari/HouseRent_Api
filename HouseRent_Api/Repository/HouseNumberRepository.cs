using HouseRent_Api.Data;
using HouseRent_Api.IRepository;
using HouseRent_Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace HouseRent_Api.Repository
{
    public class HouseNumberRepository : Repository<HouseNumber>, IHouseNumberRepository
    {
        private readonly ApplicationDbcontext _db;
        public HouseNumberRepository(ApplicationDbcontext db) : base(db)
        {
            _db = db;

        }

        public async Task<HouseNumber> UpdateAsync(HouseNumber entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.HouseNumbers.Update(entity);
            await _db.SaveChangesAsync();

            return entity;


        }
    }
}
