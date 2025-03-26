using PRN222.Assignment.Repositories.Entities;
using PRN222.Assignment.Repositories.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Assignment.Services.ComboOrders
{
    public class ComboOrderService : IComboOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ComboOrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Retrieves all order combos
        public List<OrderCombo> GetOrderCombos()
        {
            return _unitOfWork.OrderCombos.GetAll().ToList();
        }

        // Retrieves a specific order combo by its ID
        public OrderCombo GetOrderComboById(int id)
        {
            return _unitOfWork.OrderCombos.GetById(id);
        }

        // Adds a new order combo
        public void AddOrderCombo(OrderCombo orderCombo)
        {
            _unitOfWork.OrderCombos.Add(orderCombo);
            _unitOfWork.SaveAsync();  // Save changes synchronously
        }

        // Updates an existing order combo
        public void UpdateOrderCombo(OrderCombo orderCombo)
        {
            _unitOfWork.OrderCombos.Update(orderCombo);
            _unitOfWork.SaveAsync();  // Save changes synchronously
        }

        // Deletes an order combo
        public void DeleteOrderCombo(OrderCombo orderCombo)
        {
            _unitOfWork.OrderCombos.Delete(orderCombo);
            _unitOfWork.SaveAsync();  // Save changes synchronously
        }
    }
}
