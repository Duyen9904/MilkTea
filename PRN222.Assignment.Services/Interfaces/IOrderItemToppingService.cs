using PRN222.Assignment.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Assignment.Services.Interfaces
{
    public interface IOrderItemToppingService
    {
        IEnumerable<OrderItemTopping> GetOrderItemToppings(
            Expression<Func<OrderItemTopping, bool>> filter = null,
            Func<IQueryable<OrderItemTopping>, IOrderedQueryable<OrderItemTopping>> orderBy = null,
            int? pageIndex = null,
            int? pageSize = null,
            params Expression<Func<OrderItemTopping, object>>[] includes);
        Task<IEnumerable<OrderItemTopping>> GetOrderItemToppingsAsync(
            Expression<Func<OrderItemTopping, bool>> filter = null,
            Func<IQueryable<OrderItemTopping>, IOrderedQueryable<OrderItemTopping>> orderBy = null,
            int? pageIndex = null,
            int? pageSize = null,
            params Expression<Func<OrderItemTopping, object>>[] includes);
        OrderItemTopping GetById(int id);
        Task<OrderItemTopping> GetByIdAsync(int id);
        void Add(OrderItemTopping orderItemTopping);
        Task AddAsync(OrderItemTopping orderItemTopping);
        void Update(OrderItemTopping orderItemTopping);
        Task UpdateAsync(OrderItemTopping orderItemTopping);
        void Delete(int id);
        Task DeleteAsync(int id);
        IEnumerable<OrderItemTopping> GetToppingsByOrderItemId(int orderItemId);
        Task<IEnumerable<OrderItemTopping>> GetToppingsByOrderItemIdAsync(int orderItemId);
    }

}
