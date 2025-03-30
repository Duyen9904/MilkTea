using PRN222.Assignment.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Assignment.Services.Interfaces
{
    public interface IComboService
    {
        IEnumerable<Combo> GetCombos(
            Expression<Func<Combo, bool>> filter = null,
            Func<IQueryable<Combo>, IOrderedQueryable<Combo>> orderBy = null,
            int? pageIndex = null,
            int? pageSize = null,
            params Expression<Func<Combo, object>>[] includes);
        Task<IEnumerable<Combo>> GetCombosAsync(
            Expression<Func<Combo, bool>> filter = null,
            Func<IQueryable<Combo>, IOrderedQueryable<Combo>> orderBy = null,
            int? pageIndex = null,
            int? pageSize = null,
            params Expression<Func<Combo, object>>[] includes);
        Combo GetById(int id);
        Task<Combo> GetByIdAsync(int id);
        void Add(Combo combo);
        Task AddAsync(Combo combo);
        void Update(Combo combo);
        Task UpdateAsync(Combo combo);
        void Delete(int id);
        Task DeleteAsync(int id);
        IEnumerable<Combo> GetAvailableCombos();
        Task<IEnumerable<Combo>> GetAvailableCombosAsync();
    }

}
