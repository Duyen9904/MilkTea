using PRN222.Assignment.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Assignment.Services.Interfaces
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllOrders();

        Task<Order> CreateOrderAsync(Order order);

        Task<Order> UpdateOrderAsync(Order order);

        Task<Order> GetOrderByIdAsync(int orderId);

        Task<Order> GetOrderByAccountIdAsync(int accountId);

        Task<Order> UpdateOrderStatusAsync(int orderId, string status);

        Task<bool> DeleteOrderAsync(int orderId);
    }
}
