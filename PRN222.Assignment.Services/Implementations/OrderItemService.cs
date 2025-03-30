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
    public class OrderItemService : IOrderItemService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderItemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<OrderItem> GetOrderItems(
            Expression<Func<OrderItem, bool>> filter = null,
            Func<IQueryable<OrderItem>, IOrderedQueryable<OrderItem>> orderBy = null,
            int? pageIndex = null,
            int? pageSize = null,
            params Expression<Func<OrderItem, object>>[] includes)
        {
            return _unitOfWork.OrderItems.GetAll(filter, orderBy, pageIndex, pageSize, includes);
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItemsAsync(
            Expression<Func<OrderItem, bool>> filter = null,
            Func<IQueryable<OrderItem>, IOrderedQueryable<OrderItem>> orderBy = null,
            int? pageIndex = null,
            int? pageSize = null,
            params Expression<Func<OrderItem, object>>[] includes)
        {
            return await _unitOfWork.OrderItems.GetAllAsync(filter, orderBy, pageIndex, pageSize, includes);
        }

        public OrderItem GetById(int id)
        {
            return _unitOfWork.OrderItems.GetById(id);
        }

        public async Task<OrderItem> GetByIdAsync(int id)
        {
            return await _unitOfWork.OrderItems.GetByIdAsync(id);
        }

        public void Add(OrderItem orderItem)
        {
            _unitOfWork.OrderItems.Add(orderItem);
            _unitOfWork.SaveAsync().GetAwaiter().GetResult();
        }

        public async Task AddAsync(OrderItem orderItem)
        {
            await _unitOfWork.OrderItems.AddAsync(orderItem);
            await _unitOfWork.SaveAsync();
        }

        public void Update(OrderItem orderItem)
        {
            _unitOfWork.OrderItems.Update(orderItem);
            _unitOfWork.SaveAsync().GetAwaiter().GetResult();
        }

        public async Task UpdateAsync(OrderItem orderItem)
        {
            await _unitOfWork.OrderItems.UpdateAsync(orderItem);
            await _unitOfWork.SaveAsync();
        }

        public void Delete(int id)
        {
            var orderItem = _unitOfWork.OrderItems.GetById(id);
            if (orderItem != null)
            {
                _unitOfWork.OrderItems.Delete(orderItem);
                _unitOfWork.SaveAsync().GetAwaiter().GetResult();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var orderItem = await _unitOfWork.OrderItems.GetByIdAsync(id);
            if (orderItem != null)
            {
                await _unitOfWork.OrderItems.DeleteAsync(orderItem);
                await _unitOfWork.SaveAsync();
            }
        }

        public IEnumerable<OrderItem> GetOrderItemsByOrderId(int orderId)
        {
            return _unitOfWork.OrderItems.GetAll(
                oi => oi.OrderId == orderId,
                null, null, null,
                oi => oi.ProductSize,
                oi => oi.ProductSize.Product,
                oi => oi.ProductSize.Size,
                oi => oi.OrderItemToppings);
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId)
        {
            return await _unitOfWork.OrderItems.GetAllAsync(
                oi => oi.OrderId == orderId,
                null, null, null,
                oi => oi.ProductSize,
                oi => oi.ProductSize.Product,
                oi => oi.ProductSize.Size,
                oi => oi.OrderItemToppings);
        }
    }
}
