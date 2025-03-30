using PRN222.Assignment.Repositories.Repositories.Interface;
using PRN222.Assignment.Repositories.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Linq.Expressions;
using PRN222.Assignment.Services.Interfaces;

namespace PRN222.Assignment.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Order>> GetAllOrders()
        {
            var orders = await _unitOfWork.Orders.GetAllAsync(
                filter: o => o.OrderDate >= DateTime.UtcNow.AddDays(-7), // Lọc đơn hàng trong 7 ngày gần nhất
                orderBy: null, // Không sắp xếp
                pageIndex: 1, // Trang đầu tiên
                pageSize: 10, // Lấy 3 đơn hàng mỗi trang,
                o => o.Account,
                o => o.OrderCombos,
                o => o.OrderCombos.Select(oc => oc.Combo),
                o => o.OrderItems,
                o => o.OrderItems.Select(oi => oi.ProductSize),
                o => o.OrderItems.Select(oi => oi.OrderItemToppings),
                o => o.OrderItems.Select(oi => oi.OrderItemToppings.Select(oit => oit.Topping))
            );

            return orders.ToList();
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order), "Order cannot be null");
            }

            try
            {
                // Set default order status
                order.Status = "Pending";
                order.PaymentStatus = "Unpaid";
                order.OrderDate = DateTime.UtcNow;

                // Calculate total order amount
                order.Subtotal = CalculateSubtotal(order);
                order.Tax = CalculateTax(order.Subtotal);
                order.DeliveryFee = CalculateDeliveryFee(order);
                order.TotalAmount = order.Subtotal + order.Tax + order.DeliveryFee;

                // Add order to database
                await _unitOfWork.Orders.AddAsync(order);
                await _unitOfWork.SaveAsync(); // ✅ EF Core will handle transactions automatically

                return order;
            }
            catch (Exception)
            {
                throw; // ✅ Ensure the exception is propagated for logging/debugging
            }
        }

        public async Task<Order> GetOrderByAccountIdAsync(int accountId)
        {
            var orders = await _unitOfWork.Orders.GetAllAsync(
                filter: o => o.AccountId == accountId,
                includes: new Expression<Func<Order, object>>[]
                {
            o => o.OrderItems,
            o => o.OrderItems.Select(oi => oi.ProductSize),
            o => o.OrderItems.Select(oi => oi.OrderItemToppings),
            o => o.OrderItems.Select(oi => oi.OrderItemToppings.Select(oit => oit.Topping)),
            o => o.OrderCombos,
            o => o.OrderCombos.Select(oc => oc.Combo)
                }
            );

            var firstOrder = orders.FirstOrDefault();
            if (firstOrder == null)
            {
                throw new DataException($"Order for account with ID {accountId} not found");
            }
            return firstOrder;
        }

        private decimal CalculateSubtotal(Order order)
        {
            decimal subtotal = 0;
            if (order.OrderItems != null)
            {
                subtotal += order.OrderItems.Sum(oi => oi.Quantity * oi.UnitPrice);
            }
            if (order.OrderCombos != null)
            {
                subtotal += order.OrderCombos.Sum(oc => oc.Quantity * oc.UnitPrice);
            }
            return subtotal;
        }

        private decimal CalculateTax(decimal subtotal) => subtotal * 0.1m; // 10% tax
        private decimal CalculateDeliveryFee(Order order) => 5.0m; // Flat $5 delivery fee

        public async Task<Order> UpdateOrderAsync(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order), "Order cannot be null");
            }
            try
            {
                // Calculate total order amount
                order.Subtotal = CalculateSubtotal(order);
                order.Tax = CalculateTax(order.Subtotal);
                order.DeliveryFee = CalculateDeliveryFee(order);
                order.TotalAmount = order.Subtotal + order.Tax + order.DeliveryFee;
                // Update order in database
                _unitOfWork.Orders.Update(order);
                await _unitOfWork.SaveAsync(); // ✅ EF Core will handle transactions automatically
                return order;
            }
            catch (Exception)
            {
                throw; // ✅ Ensure the exception is propagated for logging/debugging
            }
        }

        public async Task<Order> UpdateOrderStatusAsync(int orderId, string status)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
            if (order == null)
            {
                throw new DataException($"Order with ID {orderId} not found");
            }
            order.Status = status;
            _unitOfWork.Orders.Update(order);
            await _unitOfWork.SaveAsync(); // ✅ EF Core will handle transactions automatically
            return order;
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
            if (order == null)
            {
                throw new DataException($"Order with ID {orderId} not found");
            }
            return order;
        }

        public async Task<bool> DeleteOrderAsync(int orderId)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
            if (order == null)
            {
                throw new DataException($"Order with ID {orderId} not found");
            }
            _unitOfWork.Orders.Delete(order);
            await _unitOfWork.SaveAsync(); // ✅ EF Core will handle transactions automatically
            return true;
        }
    }
}

