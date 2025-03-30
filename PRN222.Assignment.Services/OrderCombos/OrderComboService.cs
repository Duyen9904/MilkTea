using PRN222.Assignment.Repositories.Entities;
using PRN222.Assignment.Repositories.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Assignment.Services.OrderCombos
{
    public class OrderComboService : IOrderComboService
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderComboService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<OrderCombo>> GetAllOrderCombos()
        {
            var orderCombos = await _unitOfWork.OrderCombos.GetAllAsync();
            return orderCombos.ToList(); // Convert IEnumerable<OrderCombo> to List<OrderCombo>
        }

        public async Task<OrderCombo> CreateOrderComboAsync(OrderCombo orderCombo)
        {
            if (orderCombo == null)
            {
                throw new ArgumentNullException(nameof(orderCombo), "OrderCombo cannot be null.");
            }

            if (orderCombo.OrderId <= 0 || orderCombo.ComboId <= 0 || orderCombo.Quantity <= 0)
            {
                throw new ArgumentException("Invalid OrderCombo data. OrderId, ComboId, and Quantity must be greater than zero.");
            }

            // Ensure that unit price is valid
            if (orderCombo.UnitPrice < 0)
            {
                throw new ArgumentException("UnitPrice cannot be negative.");
            }

            // Add the new OrderCombo to the database
            await _unitOfWork.OrderCombos.AddAsync(orderCombo);
            await _unitOfWork.SaveAsync(); // Ensure changes are saved

            return orderCombo;
        }

        public async Task<OrderCombo> GetOrderComboByIdAsync(int orderComboId)
        {
            if (orderComboId <= 0)
            {
                throw new ArgumentException("OrderComboId must be greater than zero.");
            }
            // Retrieve the OrderCombo from the database
            var orderCombo = await _unitOfWork.OrderCombos.GetByIdAsync(orderComboId);
            if (orderCombo == null)
            {
                throw new KeyNotFoundException($"OrderCombo with ID {orderComboId} not found.");
            }
            return orderCombo;
        }

        public async Task<OrderCombo> UpdateOrderComboAsync(OrderCombo orderCombo)
        {
            if (orderCombo == null)
            {
                throw new ArgumentNullException(nameof(orderCombo), "OrderCombo cannot be null.");
            }
            if (orderCombo.OrderComboId <= 0 || orderCombo.OrderId <= 0 || orderCombo.ComboId <= 0 || orderCombo.Quantity <= 0)
            {
                throw new ArgumentException("Invalid OrderCombo data. OrderComboId, OrderId, ComboId, and Quantity must be greater than zero.");
            }
            // Ensure that unit price is valid
            if (orderCombo.UnitPrice < 0)
            {
                throw new ArgumentException("UnitPrice cannot be negative.");
            }
            // Update the OrderCombo in the database
            _unitOfWork.OrderCombos.Update(orderCombo);
            await _unitOfWork.SaveAsync(); // Ensure changes are saved
            return orderCombo;
        }

        public async Task<bool> DeleteOrderComboAsync(int orderComboId)
        {
            if (orderComboId <= 0)
            {
                throw new ArgumentException("OrderComboId must be greater than zero.");
            }
            // Retrieve the OrderCombo from the database
            var orderCombo = await _unitOfWork.OrderCombos.GetByIdAsync(orderComboId);
            if (orderCombo == null)
            {
                throw new KeyNotFoundException($"OrderCombo with ID {orderComboId} not found.");
            }
            // Delete the OrderCombo from the database
            _unitOfWork.OrderCombos.Delete(orderCombo);
            await _unitOfWork.SaveAsync(); // Ensure changes are saved
            return true;
        }
    }
}
