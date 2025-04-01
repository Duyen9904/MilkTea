using PRN222.Assignment.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Assignment.Services.Interfaces
{
    public interface IComboItemService
    {
        IEnumerable<ComboItem> GetComboItems(
            Expression<Func<ComboItem, bool>> filter = null,
            Func<IQueryable<ComboItem>, IOrderedQueryable<ComboItem>> orderBy = null,
            int? pageIndex = null,
            int? pageSize = null,
            params Expression<Func<ComboItem, object>>[] includes);
        Task<IEnumerable<ComboItem>> GetComboItemsAsync(
            Expression<Func<ComboItem, bool>> filter = null,
            Func<IQueryable<ComboItem>, IOrderedQueryable<ComboItem>> orderBy = null,
            int? pageIndex = null,
            int? pageSize = null,
            params Expression<Func<ComboItem, object>>[] includes);
        ComboItem GetById(int id);
        Task<ComboItem> GetByIdAsync(int id);
        void Add(ComboItem comboItem);
        Task AddAsync(ComboItem comboItem);
        void Update(ComboItem comboItem);
        Task UpdateAsync(ComboItem comboItem);
        void Delete(int id);
        Task DeleteAsync(int id);
        IEnumerable<ComboItem> GetComboItemsByComboId(int comboId);
        Task<IEnumerable<ComboItem>> GetComboItemsByComboIdAsync(int comboId);
    }
}
