using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Assignment.Repositories.Repositories.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            int? pageIndex = null,
            int? pageSize = null,
            params Expression<Func<T, object>>[] includes);

        T GetById(object id);

        void Add(T entity);

        void Update(T entity);
        void Delete(T entity);

        List<T> Find(Expression<Func<T, bool>> predicate);

        Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            int? pageIndex = null,
            int? pageSize = null,
            params Expression<Func<T, object>>[] includes);
        Task<T> GetByIdAsync(object id);

        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate);
    }
}