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
    public class OrderItemToppingService : IOrderItemToppingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderItemToppingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<OrderItemTopping> GetOrderItemToppings(
            Expression<Func<OrderItemTopping, bool>> filter = null,
            Func<IQueryable<OrderItemTopping>, IOrderedQueryable<OrderItemTopping>> orderBy = null,
            int? pageIndex = null,
            int? pageSize = null,
            params Expression<Func<OrderItemTopping, object>>[] includes)
        {
            return _unitOfWork.OrderItemsToppings.GetAll(filter, orderBy, pageIndex, pageSize, includes);
        }

        public async Task<IEnumerable<OrderItemTopping>> GetOrderItemToppingsAsync(
            Expression<Func<OrderItemTopping, bool>> filter = null,
            Func<IQueryable<OrderItemTopping>, IOrderedQueryable<OrderItemTopping>> orderBy = null,
            int? pageIndex = null,
            int? pageSize = null,
            params Expression<Func<OrderItemTopping, object>>[] includes)
        {
            return await _unitOfWork.OrderItemsToppings.GetAllAsync(filter, orderBy, pageIndex, pageSize, includes);
        }

        public OrderItemTopping GetById(int id)
        {
            return _unitOfWork.OrderItemsToppings.GetById(id);
        }

        public async Task<OrderItemTopping> GetByIdAsync(int id)
        {
            return await _unitOfWork.OrderItemsToppings.GetByIdAsync(id);
        }

        public void Add(OrderItemTopping orderItemTopping)
        {
            _unitOfWork.OrderItemsToppings.Add(orderItemTopping);
            _unitOfWork.SaveAsync().GetAwaiter().GetResult();
        }

        public async Task AddAsync(OrderItemTopping orderItemTopping)
        {
            await _unitOfWork.OrderItemsToppings.AddAsync(orderItemTopping);
            await _unitOfWork.SaveAsync();
        }

        public void Update(OrderItemTopping orderItemTopping)
        {
            _unitOfWork.OrderItemsToppings.Update(orderItemTopping);
            _unitOfWork.SaveAsync().GetAwaiter().GetResult();
        }

        public async Task UpdateAsync(OrderItemTopping orderItemTopping)
        {
            await _unitOfWork.OrderItemsToppings.UpdateAsync(orderItemTopping);
            await _unitOfWork.SaveAsync();
        }

        public void Delete(int id)
        {
            var orderItemTopping = _unitOfWork.OrderItemsToppings.GetById(id);
            if (orderItemTopping != null)
            {
                _unitOfWork.OrderItemsToppings.Delete(orderItemTopping);
                _unitOfWork.SaveAsync().GetAwaiter().GetResult();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var orderItemTopping = await _unitOfWork.OrderItemsToppings.GetByIdAsync(id);
            if (orderItemTopping != null)
            {
                await _unitOfWork.OrderItemsToppings.DeleteAsync(orderItemTopping);
                await _unitOfWork.SaveAsync();
            }
        }

        public IEnumerable<OrderItemTopping> GetToppingsByOrderItemId(int orderItemId)
        {
            return _unitOfWork.OrderItemsToppings.GetAll(
                oit => oit.OrderItemId == orderItemId,
                null, null, null,
                oit => oit.Topping);
        }

        public async Task<IEnumerable<OrderItemTopping>> GetToppingsByOrderItemIdAsync(int orderItemId)
        {
            return await _unitOfWork.OrderItemsToppings.GetAllAsync(
                oit => oit.OrderItemId == orderItemId,
                null, null, null,
                oit => oit.Topping);
        }
    }
}
