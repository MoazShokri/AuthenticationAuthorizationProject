using AuthenticationAuthorizationProject.DataAccess.Data;
using AuthenticationAuthorizationProject.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationAuthorizationProject.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        public DbSet<T> _dbset;

        public Repository(ApplicationDbContext db)
        {
            this._db = db;
            this._dbset = db.Set<T>();
        }
        
        public async Task<List<T>> GetAll()
        {
            return await _dbset.ToListAsync();

        }
        public async Task<List<T>> GetAll(Expression<Func<T, bool>> filter)
        {
            return await _dbset.Where(filter).ToListAsync();
        }

        public async Task<T> Add(T entity)
        {
            await _dbset.AddAsync(entity);
            return entity;
        }

        public async Task<T> Delete(T entity)
        {
            _dbset.Remove(entity);
            return entity;
        }


        public async Task<T> GetFristOrDefault(Expression<Func<T, bool>> filter)
        {
            return await _dbset.FirstOrDefaultAsync(filter);
        }
        public async Task<T> GetMsg(Expression<Func<T, bool>> filter)
        {
            return await _dbset.Where(filter).FirstOrDefaultAsync(filter);
        }

        public virtual void Update(T entity)
        {
            _dbset.Update(entity);
        }
        public async Task<T> Get(Expression<Func<T, bool>> filter, bool Tracking = true)
        {
            IQueryable<T> query = _dbset;
            if (!Tracking)
            {
                query = query.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync(filter);
        }
    }
}
