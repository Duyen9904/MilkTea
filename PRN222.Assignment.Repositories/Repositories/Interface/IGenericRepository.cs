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
        Task<(List<T> Items, int TotalCount)> GetAsync(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            int? page = null,
            int? pageSize = null,
            Func<IQueryable<T>, IQueryable<T>>? include = null
        );

        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate);
    }
}
