using PRN222.Assignment.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Assignment.Services.ComboOrders
{
    public interface IComboOrderService
    {
        List<OrderCombo> GetOrderCombos();
        OrderCombo GetOrderComboById(int id);
        void AddOrderCombo(OrderCombo orderCombo);
        void UpdateOrderCombo(OrderCombo orderCombo);
        void DeleteOrderCombo(OrderCombo orderCombo);
    }
}
