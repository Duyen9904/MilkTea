using PRN222.Assignment.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Assignment.Services.Interfaces
{
    public interface IOrderItemService
    {
        IEnumerable<OrderItem> GetOrderItems(
            Expression<Func<OrderItem, bool>> filter = null,
            Func<IQueryable<OrderItem>, IOrderedQueryable<OrderItem>> orderBy = null,
            int? pageIndex = null,
            int? pageSize = null,
            params Expression<Func<OrderItem, object>>[] includes);
        Task<IEnumerable<OrderItem>> GetOrderItemsAsync(
            Expression<Func<OrderItem, bool>> filter = null,
            Func<IQueryable<OrderItem>, IOrderedQueryable<OrderItem>> orderBy = null,
            int? pageIndex = null,
            int? pageSize = null,
            params Expression<Func<OrderItem, object>>[] includes);
        OrderItem GetById(int id);
        Task<OrderItem> GetByIdAsync(int id);
        void Add(OrderItem orderItem);
        Task AddAsync(OrderItem orderItem);
        void Update(OrderItem orderItem);
        Task UpdateAsync(OrderItem orderItem);
        void Delete(int id);
        Task DeleteAsync(int id);
        IEnumerable<OrderItem> GetOrderItemsByOrderId(int orderId);
        Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId);
    }
}
