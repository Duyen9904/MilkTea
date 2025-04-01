using PRN222.Assignment.Repositories.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using PRN222.Assignment.Repositories.Entities;
using PRN222.Assignment.Services.Interfaces;

namespace PRN222.Assignment.Services.Implementations
{
    public class SizeService : ISizeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SizeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Size> GetAll(
            Expression<Func<Size, bool>> filter = null,
            Func<IQueryable<Size>, IOrderedQueryable<Size>> orderBy = null,
            int? pageIndex = null,
            int? pageSize = null,
            params Expression<Func<Size, object>>[] includes)
        {
            return _unitOfWork.Sizes.GetAll(filter, orderBy, pageIndex, pageSize, includes);
        }

        public async Task<IEnumerable<Size>> GetAllAsync(
            Expression<Func<Size, bool>> filter = null,
            Func<IQueryable<Size>, IOrderedQueryable<Size>> orderBy = null,
            int? pageIndex = null,
            int? pageSize = null,
            params Expression<Func<Size, object>>[] includes)
        {
            return await _unitOfWork.Sizes.GetAllAsync(filter, orderBy, pageIndex, pageSize, includes);
        }

        public Size GetById(int id)
        {
            return _unitOfWork.Sizes.GetById(id);
        }

        public async Task<Size> GetByIdAsync(int id)
        {
            return await _unitOfWork.Sizes.GetByIdAsync(id);
        }

        public void Add(Size size)
        {
            _unitOfWork.Sizes.Add(size);
            _unitOfWork.SaveAsync().GetAwaiter().GetResult();
        }

        public async Task AddAsync(Size size)
        {
            await _unitOfWork.Sizes.AddAsync(size);
            await _unitOfWork.SaveAsync();
        }

        public void Update(Size size)
        {
            _unitOfWork.Sizes.Update(size);
            _unitOfWork.SaveAsync().GetAwaiter().GetResult();
        }

        public async Task UpdateAsync(Size size)
        {
            await _unitOfWork.Sizes.UpdateAsync(size);
            await _unitOfWork.SaveAsync();
        }

        public void Delete(Size size)
        {
            _unitOfWork.Sizes.Delete(size);
            _unitOfWork.SaveAsync().GetAwaiter().GetResult();
        }

        public async Task DeleteAsync(Size size)
        {
            await _unitOfWork.Sizes.DeleteAsync(size);
            await _unitOfWork.SaveAsync();
        }

        public IEnumerable<Size> GetSizesWithProducts()
        {
            return _unitOfWork.Sizes.GetAll(
                null,
                q => q.OrderBy(s => s.PriceModifier),
                null,
                null,
                s => s.ProductSizes,
                s => s.ProductSizes.Select(ps => ps.Product)
            );
        }

        public async Task<IEnumerable<Size>> GetSizesWithProductsAsync()
        {
            return await _unitOfWork.Sizes.GetAllAsync(
                null,
                q => q.OrderBy(s => s.PriceModifier),
                null,
                null,
                s => s.ProductSizes,
                s => s.ProductSizes.Select(ps => ps.Product)
            );
        }
    }
}
