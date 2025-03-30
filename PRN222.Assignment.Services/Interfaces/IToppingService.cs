using PRN222.Assignment.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Assignment.Services.Interfaces
{
    public interface IToppingService
    {
        IEnumerable<Topping> GetAll(
            Expression<Func<Topping, bool>> filter = null,
            Func<IQueryable<Topping>, IOrderedQueryable<Topping>> orderBy = null,
            int? pageIndex = null,
            int? pageSize = null,
            params Expression<Func<Topping, object>>[] includes);
        Task<IEnumerable<Topping>> GetAllAsync(
            Expression<Func<Topping, bool>> filter = null,
            Func<IQueryable<Topping>, IOrderedQueryable<Topping>> orderBy = null,
            int? pageIndex = null,
            int? pageSize = null,
            params Expression<Func<Topping, object>>[] includes);
        Topping GetById(int id);
        Task<Topping> GetByIdAsync(int id);
        void Add(Topping topping);
        Task AddAsync(Topping topping);
        void Update(Topping topping);
        Task UpdateAsync(Topping topping);
        void Delete(Topping topping);
        Task DeleteAsync(Topping topping);
        IEnumerable<Topping> GetAvailableToppings();
        Task<IEnumerable<Topping>> GetAvailableToppingsAsync();
    }
}
