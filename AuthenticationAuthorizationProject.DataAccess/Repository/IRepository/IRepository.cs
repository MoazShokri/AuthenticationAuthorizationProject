using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationAuthorizationProject.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<List<T>> GetAll(Expression<Func<T, bool>> filter);
        Task<T> GetFristOrDefault(Expression<Func<T, bool>> filter);
        Task<T> GetMsg(Expression<Func<T, bool>> filter);
        Task<T> Get(Expression<Func<T, bool>> filter, bool Tracking = true);
        Task<T> Add(T entity);
        Task<T> Delete(T entity);
        void Update(T entity);

    }
}
