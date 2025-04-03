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
    public class ClientOrderService : IClientOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClientOrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<MilkTeaProduct>> GetAllAvailableMilkTeaProductsAsync()
        {
            return await _unitOfWork.MilkTeaProducts.GetAllAsync(
                filter: p => p.IsAvailable,
                includes: p => p.Category);
        }

        public async Task<IEnumerable<MilkTeaProduct>> GetMilkTeaProductsByCategoryAsync(int categoryId)
        {
            var products = await _unitOfWork.MilkTeaProducts.GetAllAsync(
                filter: p => p.CategoryId == categoryId,
                includes: p => p.Category
            );

            return products;
        }

        public async Task<MilkTeaProduct> GetMilkTeaProductByIdAsync(int productId)
        {
            var products = await _unitOfWork.MilkTeaProducts.GetAllAsync(
                filter: p => p.ProductId == productId,
                includes:
                [
                    p => p.Category,
                    p => p.ProductSizes
                ]
            );

            return products.FirstOrDefault();
        }

        public async Task<IEnumerable<ProductSize>> GetProductSizesByProductIdAsync(int productId)
        {
            return await _unitOfWork.ProductSizes.GetAllAsync(
                filter: ps => ps.ProductId == productId,
                includes:
                [ps => ps.Size, ps => ps.Product]
                );
        }

        public async Task<Size> GetSizeByIdAsync(int sizeId)
        {
            return await _unitOfWork.Sizes.GetByIdAsync(sizeId);
        }

        public async Task<IEnumerable<Topping>> GetAllAvailableToppingsAsync()
        {
            return await _unitOfWork.Toppings.GetAllAsync(
                filter: t => t.IsAvailable);
        }

        public async Task<Topping> GetToppingByIdAsync(int toppingId)
        {
            return await _unitOfWork.Toppings.GetByIdAsync(toppingId);
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            await _unitOfWork.Orders.AddAsync(order);
            await _unitOfWork.SaveAsync();
            return order;
        }

        public async Task<IEnumerable<OrderItem>> CreateOrderItemsAsync(IEnumerable<OrderItem> orderItems)
        {
            foreach (var item in orderItems)
            {
                await _unitOfWork.OrderItems.AddAsync(item);
            }
            await _unitOfWork.SaveAsync();
            return orderItems;
        }

        public async Task<IEnumerable<OrderItemTopping>> CreateOrderItemToppingsAsync(IEnumerable<OrderItemTopping> orderItemToppings)
        {
            foreach (var topping in orderItemToppings)
            {
                await _unitOfWork.OrderItemsToppings.AddAsync(topping);
            }
            await _unitOfWork.SaveAsync();
            return orderItemToppings;
        }

        public async Task<decimal> CalculateOrderItemSubtotalAsync(int productSizeId, int quantity, IEnumerable<int> toppingIds)
        {
            decimal subtotal = 0;

            // Get product size price
            var productSize = await _unitOfWork.ProductSizes.GetByIdAsync(productSizeId);
            if (productSize != null)
            {
                subtotal += productSize.Price * quantity;
            }

            // Add toppings price
            if (toppingIds != null && toppingIds.Any())
            {
                foreach (var toppingId in toppingIds)
                {
                    var topping = await _unitOfWork.Toppings.GetByIdAsync(toppingId);
                    if (topping != null)
                    {
                        subtotal += topping.Price * quantity;
                    }
                }
            }

            return subtotal;
        }

        public async Task<decimal> CalculateOrderTotalAsync(IEnumerable<OrderItem> orderItems, decimal deliveryFee = 0, decimal taxRate = 0.1m)
        {
            decimal subtotal = 0;

            foreach (var item in orderItems)
            {
                // Calculate item price (including toppings)
                decimal itemTotal = item.UnitPrice * item.Quantity;

                // Add topping prices if available
                var toppings = await _unitOfWork.OrderItemsToppings.GetAllAsync(
                    filter: t => t.OrderItemId == item.OrderItemId);

                foreach (var topping in toppings)
                {
                    itemTotal += topping.Price * item.Quantity;
                }

                subtotal += itemTotal;
            }

            decimal tax = subtotal * taxRate;
            decimal total = subtotal + tax + deliveryFee;

            return total;
        }

        public async Task<Order> GetOrderWithDetailsAsync(int orderId)
        {
            var orders = await _unitOfWork.Orders.GetAllAsync(
                filter: o => o.OrderId == orderId,
                includes: [o => o.OrderItems, o => o.Account]);

            var order = orders.FirstOrDefault();

            if (order != null)
            {
                // Load order items with product size details
                foreach (var item in order.OrderItems)
                {
                    // Load product size with product and size details
                    var productSizes = await _unitOfWork.ProductSizes.GetAllAsync(
                        filter: ps => ps.ProductSizeId == item.ProductSizeId,
                        includes: [ps => ps.Product, ps => ps.Size]);

                    item.ProductSize = productSizes.FirstOrDefault();

                    // Load toppings for each order item
                    var toppings = await _unitOfWork.OrderItemsToppings.GetAllAsync(
                        filter: t => t.OrderItemId == item.OrderItemId,
                        includes: t => t.Topping);

                    foreach (var topping in toppings)
                    {
                        item.OrderItemToppings.Add(topping);
                    }
                }
            }

            return order;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _unitOfWork.Categories.GetAllAsync();
        }

        public async Task<bool> UpdateOrderStatusAsync(int orderId, string status)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
            if (order == null)
                return false;

            order.Status = status;
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> UpdatePaymentStatusAsync(int orderId, string paymentStatus)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
            if (order == null)
                return false;

            order.PaymentStatus = paymentStatus;
            await _unitOfWork.SaveAsync();
            return true;
        }

        public Task<IEnumerable<Combo>> GetAllCombosAsync()
        {
            return _unitOfWork.Combos.GetAllAsync();
        }

        public async Task<IEnumerable<ComboItem>> GetComboItemsByComboIdAsync(int comboId)
        {
            try
            {
                // Use the repository pattern through UnitOfWork to get combo items
                var comboItems = await _unitOfWork.ComboItems.GetAllAsync(
                    filter: ci => ci.ComboId == comboId
                );

                return comboItems;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving combo items: {ex.Message}");
                return new List<ComboItem>();
            }
        }

        // Implement the second required method
        public async Task<ProductSize> GetProductSizeByIdAsync(int productSizeId)
        {
            try
            {
                // Get the product size with all its related entities
                var productSize = await _unitOfWork.ProductSizes.GetByIdAsync(productSizeId);

                if (productSize != null)
                {
                    // Load the related product and size information
                    productSize.Product = await _unitOfWork.MilkTeaProducts.GetByIdAsync(productSize.ProductId);
                    productSize.Size = await _unitOfWork.Sizes.GetByIdAsync(productSize.SizeId);
                }

                return productSize;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving product size: {ex.Message}");
                return null;
            }
        }
    }
}
