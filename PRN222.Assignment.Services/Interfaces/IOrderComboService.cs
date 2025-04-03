using PRN222.Assignment.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Assignment.Services.Interfaces
{
    public interface IOrderComboService
    {
        Task<List<OrderCombo>> GetAllOrderCombosAsync();

        Task<OrderCombo> CreateOrderComboAsync(OrderCombo orderCombo);

        Task<OrderCombo> GetOrderComboByIdAsync(int orderComboId);

        Task<OrderCombo> UpdateOrderComboAsync(OrderCombo orderCombo);

        Task<bool> DeleteOrderComboAsync(int orderComboId);
    }
}
