using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using PRN222.Assignment.Repositories.Entities;
using PRN222.Assignment.Repositories.Repositories.Interface;
using PRN222.Assignment.Services.Interfaces;

namespace PRN222.Assignment.Services.Implementations
{
    public class MilkTeaProductService : IMilkTeaProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MilkTeaProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<MilkTeaProduct> GetAll(
            Expression<Func<MilkTeaProduct, bool>> filter = null,
            Func<IQueryable<MilkTeaProduct>, IOrderedQueryable<MilkTeaProduct>> orderBy = null,
            int? pageIndex = null,
            int? pageSize = null,
            params Expression<Func<MilkTeaProduct, object>>[] includes)
        {
            return _unitOfWork.MilkTeaProducts.GetAll(filter, orderBy, pageIndex, pageSize, includes);
        }

        public async Task<IEnumerable<MilkTeaProduct>> GetAllAsync(
            Expression<Func<MilkTeaProduct, bool>> filter = null,
            Func<IQueryable<MilkTeaProduct>, IOrderedQueryable<MilkTeaProduct>> orderBy = null,
            int? pageIndex = null,
            int? pageSize = null,
            params Expression<Func<MilkTeaProduct, object>>[] includes)
        {
            return await _unitOfWork.MilkTeaProducts.GetAllAsync(filter, orderBy, pageIndex, pageSize, includes);
        }

        public MilkTeaProduct GetById(int id)
        {
            return _unitOfWork.MilkTeaProducts.GetById(id);
        }

        public async Task<MilkTeaProduct> GetByIdAsync(int id)
        {
            return await _unitOfWork.MilkTeaProducts.GetByIdAsync(id);
        }

        public void AddMilkTeaProduct(MilkTeaProduct milkTeaProduct)
        {
            _unitOfWork.MilkTeaProducts.Add(milkTeaProduct);
            _unitOfWork.SaveAsync().GetAwaiter().GetResult();
        }

        public async Task AddMilkTeaProductAsync(MilkTeaProduct milkTeaProduct)
        {
            await _unitOfWork.MilkTeaProducts.AddAsync(milkTeaProduct);
            await _unitOfWork.SaveAsync();
        }

        public void UpdateMilkTeaProduct(MilkTeaProduct milkTeaProduct)
        {
            _unitOfWork.MilkTeaProducts.Update(milkTeaProduct);
            _unitOfWork.SaveAsync().GetAwaiter().GetResult();
        }

        public async Task UpdateMilkTeaProductAsync(MilkTeaProduct milkTeaProduct)
        {
            await _unitOfWork.MilkTeaProducts.UpdateAsync(milkTeaProduct);
            await _unitOfWork.SaveAsync();
        }

        public void DeleteMilkTeaProduct(MilkTeaProduct milkTeaProduct)
        {
            _unitOfWork.MilkTeaProducts.Delete(milkTeaProduct);
            _unitOfWork.SaveAsync().GetAwaiter().GetResult();
        }

        public async Task DeleteMilkTeaProductAsync(MilkTeaProduct milkTeaProduct)
        {
            await _unitOfWork.MilkTeaProducts.DeleteAsync(milkTeaProduct);
            await _unitOfWork.SaveAsync();
        }

        public IEnumerable<MilkTeaProduct> GetMilkTeaProductsWithCategory(
            Expression<Func<MilkTeaProduct, bool>> filter = null,
            Func<IQueryable<MilkTeaProduct>, IOrderedQueryable<MilkTeaProduct>> orderBy = null,
            int? pageIndex = null,
            int? pageSize = null)
        {
            return _unitOfWork.MilkTeaProducts.GetAll(
                filter,
                orderBy,
                pageIndex,
                pageSize,
                p => p.Category);
        }

        public async Task<IEnumerable<MilkTeaProduct>> GetMilkTeaProductsWithCategoryAsync(
            Expression<Func<MilkTeaProduct, bool>> filter = null,
            Func<IQueryable<MilkTeaProduct>, IOrderedQueryable<MilkTeaProduct>> orderBy = null,
            int? pageIndex = null,
            int? pageSize = null)
        {
            return await _unitOfWork.MilkTeaProducts.GetAllAsync(
                filter,
                orderBy,
                pageIndex,
                pageSize,
                p => p.Category);
        }

        //public Task<List<MilkTeaProduct>> Search(string productName, string description, string category)
        //{
        //    var query = _unitOfWork.MilkTeaProducts.GetAll(
        //        filter: m => (string.IsNullOrEmpty(productName) || m.ProductName.Contains(productName)) &&
        //                     (string.IsNullOrEmpty(description) || m.Description.Contains(description)) &&
        //                     (string.IsNullOrEmpty(category) || m.Category.CategoryName.Contains(category)),
        //        orderBy: q => q.OrderBy(m => m.ProductName)
        //    ).ToListAsync();

        //    return query;
        //}
    }
}
