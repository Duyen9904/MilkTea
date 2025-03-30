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
    public class ComboItemService : IComboItemService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ComboItemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<ComboItem> GetComboItems(
            Expression<Func<ComboItem, bool>> filter = null,
            Func<IQueryable<ComboItem>, IOrderedQueryable<ComboItem>> orderBy = null,
            int? pageIndex = null,
            int? pageSize = null,
            params Expression<Func<ComboItem, object>>[] includes)
        {
            return _unitOfWork.ComboItems.GetAll(filter, orderBy, pageIndex, pageSize, includes);
        }

        public async Task<IEnumerable<ComboItem>> GetComboItemsAsync(
            Expression<Func<ComboItem, bool>> filter = null,
            Func<IQueryable<ComboItem>, IOrderedQueryable<ComboItem>> orderBy = null,
            int? pageIndex = null,
            int? pageSize = null,
            params Expression<Func<ComboItem, object>>[] includes)
        {
            return await _unitOfWork.ComboItems.GetAllAsync(filter, orderBy, pageIndex, pageSize, includes);
        }

        public ComboItem GetById(int id)
        {
            return _unitOfWork.ComboItems.GetById(id);
        }

        public async Task<ComboItem> GetByIdAsync(int id)
        {
            return await _unitOfWork.ComboItems.GetByIdAsync(id);
        }

        public void Add(ComboItem comboItem)
        {
            _unitOfWork.ComboItems.Add(comboItem);
            _unitOfWork.SaveAsync().GetAwaiter().GetResult();
        }

        public async Task AddAsync(ComboItem comboItem)
        {
            await _unitOfWork.ComboItems.AddAsync(comboItem);
            await _unitOfWork.SaveAsync();
        }

        public void Update(ComboItem comboItem)
        {
            _unitOfWork.ComboItems.Update(comboItem);
            _unitOfWork.SaveAsync().GetAwaiter().GetResult();
        }

        public async Task UpdateAsync(ComboItem comboItem)
        {
            await _unitOfWork.ComboItems.UpdateAsync(comboItem);
            await _unitOfWork.SaveAsync();
        }

        public void Delete(int id)
        {
            var comboItem = _unitOfWork.ComboItems.GetById(id);
            if (comboItem != null)
            {
                _unitOfWork.ComboItems.Delete(comboItem);
                _unitOfWork.SaveAsync().GetAwaiter().GetResult();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var comboItem = await _unitOfWork.ComboItems.GetByIdAsync(id);
            if (comboItem != null)
            {
                await _unitOfWork.ComboItems.DeleteAsync(comboItem);
                await _unitOfWork.SaveAsync();
            }
        }

        public IEnumerable<ComboItem> GetComboItemsByComboId(int comboId)
        {
            return _unitOfWork.ComboItems.GetAll(
                ci => ci.ComboId == comboId,
                null, null, null,
                ci => ci.ProductSize,
                ci => ci.ProductSize.Product,
                ci => ci.ProductSize.Size);
        }

        public async Task<IEnumerable<ComboItem>> GetComboItemsByComboIdAsync(int comboId)
        {
            return await _unitOfWork.ComboItems.GetAllAsync(
                ci => ci.ComboId == comboId,
                null, null, null,
                ci => ci.ProductSize,
                ci => ci.ProductSize.Product,
                ci => ci.ProductSize.Size);
        }
    }
}
