using PRN222.Assignment.Repositories.Entities;
using System.Collections.Generic;

namespace PRN222.Assignment.Blazor.Client.Components.Services
{
    public class OrderStateService
    {
        private OrderData currentOrderData;

        public OrderData GetCurrentOrderData()
        {
            return currentOrderData;
        }

        public void SetOrderData(Order order, List<OrderItem> orderItems)
        {
            currentOrderData = new OrderData
            {
                Order = order,
                OrderItems = orderItems
            };
        }

        public void ClearOrderData()
        {
            currentOrderData = null;
        }
    }

    public class OrderData
    {
        public Order Order { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}