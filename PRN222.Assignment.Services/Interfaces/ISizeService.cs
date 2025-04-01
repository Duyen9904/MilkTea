using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using PRN222.Assignment.Repositories.Entities;

namespace PRN222.Assignment.Services.Interfaces
{
    public interface ISizeService
    {
        IEnumerable<Size> GetAll(
            Expression<Func<Size, bool>> filter = null,
            Func<IQueryable<Size>, IOrderedQueryable<Size>> orderBy = null,
            int? pageIndex = null,
            int? pageSize = null,
            params Expression<Func<Size, object>>[] includes);
        Task<IEnumerable<Size>> GetAllAsync(
            Expression<Func<Size, bool>> filter = null,
            Func<IQueryable<Size>, IOrderedQueryable<Size>> orderBy = null,
            int? pageIndex = null,
            int? pageSize = null,
            params Expression<Func<Size, object>>[] includes);
        Size GetById(int id);
        Task<Size> GetByIdAsync(int id);
        void Add(Size size);
        Task AddAsync(Size size);
        void Update(Size size);
        Task UpdateAsync(Size size);
        void Delete(Size size);
        Task DeleteAsync(Size size);
        IEnumerable<Size> GetSizesWithProducts();
        Task<IEnumerable<Size>> GetSizesWithProductsAsync();
    }

}
