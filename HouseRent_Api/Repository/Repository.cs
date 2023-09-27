using HouseRent_Api.Data;
using HouseRent_Api.IRepository;
using HouseRent_Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HouseRent_Api.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbcontext _db;
        internal DbSet<T> _dbSet;
        public Repository(ApplicationDbcontext db)
        {
            _db = db;
            this._dbSet = _db.Set<T>();

        }
        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await SaveAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true)
        {
            IQueryable<T> query = _dbSet;

            if (!tracked)
            {
                query = query.AsNoTracking();

            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();

        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();

        }

        public async Task RemoveAsync(T entity)
        {
            _dbSet.Remove(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

    }
}
