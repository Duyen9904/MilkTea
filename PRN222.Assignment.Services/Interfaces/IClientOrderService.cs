using PRN222.Assignment.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PRN222.Assignment.Services.Interfaces
{
    public interface IClientOrderService
    {
        // Get all available milk tea products
        Task<IEnumerable<MilkTeaProduct>> GetAllAvailableMilkTeaProductsAsync();

        // Get milk tea products by category
        Task<IEnumerable<MilkTeaProduct>> GetMilkTeaProductsByCategoryAsync(int categoryId);

        // Get product details
        Task<MilkTeaProduct> GetMilkTeaProductByIdAsync(int productId);

        // Get available sizes for a specific product
        Task<IEnumerable<ProductSize>> GetProductSizesByProductIdAsync(int productId);

        // Get size details
        Task<Size> GetSizeByIdAsync(int sizeId);

        // Get all available toppings
        Task<IEnumerable<Topping>> GetAllAvailableToppingsAsync();

        // Get topping details
        Task<Topping> GetToppingByIdAsync(int toppingId);

        // Create order in database
        Task<Order> CreateOrderAsync(Order order);

        // Create order items in database
        Task<IEnumerable<OrderItem>> CreateOrderItemsAsync(IEnumerable<OrderItem> orderItems);

        // Create order item toppings in database
        Task<IEnumerable<OrderItemTopping>> CreateOrderItemToppingsAsync(IEnumerable<OrderItemTopping> orderItemToppings);

        // Calculate subtotal for an order item with its toppings
        Task<decimal> CalculateOrderItemSubtotalAsync(int productSizeId, int quantity, IEnumerable<int> toppingIds);

        // Calculate total amount for an order
        Task<decimal> CalculateOrderTotalAsync(IEnumerable<OrderItem> orderItems, decimal deliveryFee = 0, decimal taxRate = 0.1m);

        // Get complete order details
        Task<Order> GetOrderWithDetailsAsync(int orderId);

        // Get all categories
        Task<IEnumerable<Category>> GetAllCategoriesAsync();

        // Update order status
        Task<bool> UpdateOrderStatusAsync(int orderId, string status);

        // Update payment status
        Task<bool> UpdatePaymentStatusAsync(int orderId, string paymentStatus);

        Task<IEnumerable<Combo>> GetAllCombosAsync();
        // Add these methods to IClientOrderService.cs
        Task<IEnumerable<ComboItem>> GetComboItemsByComboIdAsync(int comboId);

        Task<ProductSize> GetProductSizeByIdAsync(int productSizeId);
    }
}