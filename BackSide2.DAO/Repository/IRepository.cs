using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Auga.DAO.Entities;

namespace Auga.DAO.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        //Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null);
        Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includes);

        Task<IQueryable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
        Task<T> GetByIdAsync(long id);
        Task<bool> ExistsByIdAsync(long id);
        Task<T> InsertAsync(T entity);
        Task<T> UpdateAsync(T entity);

        Task<T> RemoveAsync(T entity);
        Task RemoveManyAsync(ICollection<T> entity);
        //Task SaveChangesAsync();
    }
}