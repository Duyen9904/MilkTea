using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using PRN222.Assignment.Repositories.Entities;

namespace PRN222.Assignment.Services.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategories(
        Expression<Func<Category, bool>> filter = null,
        Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null,
        int? pageIndex = null,
        int? pageSize = null,
        params Expression<Func<Category, object>>[] includes);
        Task<IEnumerable<Category>> GetCategoriesAsync(
            Expression<Func<Category, bool>> filter = null,
            Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null,
            int? pageIndex = null,
            int? pageSize = null,
            params Expression<Func<Category, object>>[] includes);
        Category GetById(int id);
        Task<Category> GetByIdAsync(int id);
        void Add(Category category);
        Task AddAsync(Category category);
        void Update(Category category);
        Task UpdateAsync(Category category);
        void Delete(int id);
        Task DeleteAsync(int id);
    }
}
