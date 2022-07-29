using ESourcing.Ordering.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ESourcing.Ordering.Domain.Repositories.Base
{
    public interface IRepository<T> where T : Entity
    {
        Task<T> PostAsync(T entity);
        Task PutAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T> GetByIdAsync(int id);

        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate = null,
                                          Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                          string includeString = null,
                                          bool disableTracking = true);

    }
}
