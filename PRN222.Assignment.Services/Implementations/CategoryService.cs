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
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Category> GetCategories(
            Expression<Func<Category, bool>> filter = null,
            Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null,
            int? pageIndex = null,
            int? pageSize = null,
            params Expression<Func<Category, object>>[] includes)
        {
            return _unitOfWork.Categories.GetAll(filter, orderBy, pageIndex, pageSize, includes);
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync(
            Expression<Func<Category, bool>> filter = null,
            Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null,
            int? pageIndex = null,
            int? pageSize = null,
            params Expression<Func<Category, object>>[] includes)
        {
            return await _unitOfWork.Categories.GetAllAsync(filter, orderBy, pageIndex, pageSize, includes);
        }

        public Category GetById(int id)
        {
            return _unitOfWork.Categories.GetById(id);
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _unitOfWork.Categories.GetByIdAsync(id);
        }

        public void Add(Category category)
        {
            _unitOfWork.Categories.Add(category);
            _unitOfWork.SaveAsync().GetAwaiter().GetResult();
        }

        public async Task AddAsync(Category category)
        {
            await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.SaveAsync();
        }

        public void Update(Category category)
        {
            _unitOfWork.Categories.Update(category);
            _unitOfWork.SaveAsync().GetAwaiter().GetResult();
        }

        public async Task UpdateAsync(Category category)
        {
            await _unitOfWork.Categories.UpdateAsync(category);
            await _unitOfWork.SaveAsync();
        }

        public void Delete(int id)
        {
            var category = _unitOfWork.Categories.GetById(id);
            if (category != null)
            {
                _unitOfWork.Categories.Delete(category);
                _unitOfWork.SaveAsync().GetAwaiter().GetResult();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category != null)
            {
                await _unitOfWork.Categories.DeleteAsync(category);
                await _unitOfWork.SaveAsync();
            }
        }
    }
}
