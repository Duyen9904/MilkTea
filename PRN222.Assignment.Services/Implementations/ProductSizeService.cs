using PRN222.Assignment.Repositories.Entities;
using PRN222.Assignment.Repositories.Repositories.Interface;
using PRN222.Assignment.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Assignment.Services.Implementations
{
    public class ProductSizeService : IProductSizeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductSizeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<ProductSize> GetProductSizes(
            Expression<Func<ProductSize, bool>> filter = null,
            Func<IQueryable<ProductSize>, IOrderedQueryable<ProductSize>> orderBy = null,
            int? pageIndex = null,
            int? pageSize = null,
            params Expression<Func<ProductSize, object>>[] includes)
        {
            return _unitOfWork.ProductSizes.GetAll(filter, orderBy, pageIndex, pageSize, ps => ps.Size);
        }

        public async Task<IEnumerable<ProductSize>> GetProductSizesAsync(
            Expression<Func<ProductSize, bool>> filter = null,
            Func<IQueryable<ProductSize>, IOrderedQueryable<ProductSize>> orderBy = null,
            int? pageIndex = null,
            int? pageSize = null,
            params Expression<Func<ProductSize, object>>[] includes)
        {
            return await _unitOfWork.ProductSizes.GetAllAsync(filter, orderBy, pageIndex, pageSize, ps => ps.Size);
        }

        public ProductSize GetById(int id)
        {
            return _unitOfWork.ProductSizes.GetById(id);
        }

        public async Task<ProductSize> GetByIdAsync(int id)
        {
            return await _unitOfWork.ProductSizes.GetByIdAsync(id);
        }

        public void Add(ProductSize productSize)
        {
            _unitOfWork.ProductSizes.Add(productSize);
            _unitOfWork.SaveAsync().GetAwaiter().GetResult();
        }

        public async Task AddAsync(ProductSize productSize)
        {
            await _unitOfWork.ProductSizes.AddAsync(productSize);
            await _unitOfWork.SaveAsync();
        }

        public void Update(ProductSize productSize)
        {
            _unitOfWork.ProductSizes.Update(productSize);
            _unitOfWork.SaveAsync().GetAwaiter().GetResult();
        }

        public async Task UpdateAsync(ProductSize productSize)
        {
            await _unitOfWork.ProductSizes.UpdateAsync(productSize);
            await _unitOfWork.SaveAsync();
        }

        public void Delete(int id)
        {
            var productSize = _unitOfWork.ProductSizes.GetById(id);
            if (productSize != null)
            {
                _unitOfWork.ProductSizes.Delete(productSize);
                _unitOfWork.SaveAsync().GetAwaiter().GetResult();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var productSize = await _unitOfWork.ProductSizes.GetByIdAsync(id);
            if (productSize != null)
            {
                await _unitOfWork.ProductSizes.DeleteAsync(productSize);
                await _unitOfWork.SaveAsync();
            }
        }

        public IEnumerable<ProductSize> GetProductSizesByProductId(int productId)
        {
            return _unitOfWork.ProductSizes.GetAll(
                ps => ps.ProductId == productId,
                null, null, null,
                ps => ps.Product,
                ps => ps.Size);
        }

        public async Task<IEnumerable<ProductSize>> GetProductSizesByProductIdAsync(int productId)
        {
            return await _unitOfWork.ProductSizes.GetAllAsync(
                ps => ps.ProductId == productId,
                null, null, null,
                ps => ps.Product,
                ps => ps.Size);
        }

        public ProductSize GetByProductAndSizeId(int productId, int sizeId)
        {
            return _unitOfWork.ProductSizes.GetAll(
                ps => ps.ProductId == productId && ps.SizeId == sizeId,
                null, null, null,
                ps => ps.Product,
                ps => ps.Size).FirstOrDefault();
        }

        public async Task<ProductSize> GetByProductAndSizeIdAsync(int productId, int sizeId)
        {
            var result = await _unitOfWork.ProductSizes.GetAllAsync(
                ps => ps.ProductId == productId && ps.SizeId == sizeId,
                null, null, null,
                ps => ps.Product,
                ps => ps.Size);

            return result.FirstOrDefault();
        }
    }
}
