using PRN222.Assignment.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Assignment.Services.Interfaces
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllOrders();

        Task<IEnumerable<Order>> GetAllOrdersPagination(
                   Expression<Func<Order, bool>> filter = null,
                   Func<IQueryable<Order>, IOrderedQueryable<Order>> orderBy = null,
                   int? pageIndex = null,
                   int? pageSize = null);

        Task<int> GetOrdersCount(Expression<Func<Order, bool>> filter = null);
        Task<Order> CreateOrderAsync(Order order);

        Task<Order> UpdateOrderAsync(Order order);

        Task<Order> GetOrderByIdAsync(int orderId);

        Task<Order> GetOrderByAccountIdAsync(int accountId);

        Task<Order> UpdateOrderStatusAsync(int orderId, string status);

        Task<bool> DeleteOrderAsync(int orderId);
    }
}
