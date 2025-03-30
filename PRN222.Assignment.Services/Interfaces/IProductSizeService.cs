using PRN222.Assignment.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Assignment.Services.Interfaces
{
    public interface IProductSizeService
    {
        IEnumerable<ProductSize> GetProductSizes(
            Expression<Func<ProductSize, bool>> filter = null,
            Func<IQueryable<ProductSize>, IOrderedQueryable<ProductSize>> orderBy = null,
            int? pageIndex = null,
            int? pageSize = null,
            params Expression<Func<ProductSize, object>>[] includes);
        Task<IEnumerable<ProductSize>> GetProductSizesAsync(
            Expression<Func<ProductSize, bool>> filter = null,
            Func<IQueryable<ProductSize>, IOrderedQueryable<ProductSize>> orderBy = null,
            int? pageIndex = null,
            int? pageSize = null,
            params Expression<Func<ProductSize, object>>[] includes);
        ProductSize GetById(int id);
        Task<ProductSize> GetByIdAsync(int id);
        void Add(ProductSize productSize);
        Task AddAsync(ProductSize productSize);
        void Update(ProductSize productSize);
        Task UpdateAsync(ProductSize productSize);
        void Delete(int id);
        Task DeleteAsync(int id);
        IEnumerable<ProductSize> GetProductSizesByProductId(int productId);
        Task<IEnumerable<ProductSize>> GetProductSizesByProductIdAsync(int productId);
        ProductSize GetByProductAndSizeId(int productId, int sizeId);
        Task<ProductSize> GetByProductAndSizeIdAsync(int productId, int sizeId);
    }
}
