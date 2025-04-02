using Microsoft.AspNetCore.Components;
using PRN222.Assignment.Repositories.Entities;
using PRN222.Assignment.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRN222.Assignment.Blazor.Client.Components.Pages
{
    public class OrderBase : ComponentBase
    {
        [Inject]
        protected IClientOrderService ClientOrderService { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected OrderStateService OrderStateService { get; set; }

        protected List<Category> categories = new List<Category>();
        protected List<MilkTeaProduct> products = new List<MilkTeaProduct>();
        protected List<ProductSize> productSizes = new List<ProductSize>();
        protected List<Topping> availableToppings = new List<Topping>();
        protected List<int> selectedToppingIds = new List<int>();
        protected string specialInstructions = string.Empty;

        protected int selectedCategoryId = 0;
        protected MilkTeaProduct selectedProduct;
        protected int selectedProductSizeId = 0;
        protected int quantity = 1;

        // Order data - using fully qualified type name
        protected PRN222.Assignment.Repositories.Entities.Order currentOrder = new PRN222.Assignment.Repositories.Entities.Order
        {
            AccountId = 2,
            OrderDate = DateTime.Now,
            Status = "Pending",
            PaymentStatus = "Pending",
            PaymentMethod = "Cash", // Valid value from check constraint
            DeliveryAddress = "",
            Subtotal = 0,
            Tax = 0,
            DeliveryFee = 0,
            TotalAmount = 0
        };

        protected List<OrderItem> orderItems = new List<OrderItem>();
        protected decimal subtotal = 0;

        protected override async Task OnInitializedAsync()
        {
            // Load categories
            var allCategories = await ClientOrderService.GetAllCategoriesAsync();
            categories = allCategories.ToList();

            // Load available toppings
            availableToppings = (await ClientOrderService.GetAllAvailableToppingsAsync()).ToList();

            if (categories.Any())
            {
                selectedCategoryId = categories.First().CategoryId;
                await LoadProductsByCategory(selectedCategoryId);
            }

            // Check if there's existing order data in state
            var orderData = OrderStateService.GetCurrentOrderData();
            if (orderData != null)
            {
                orderItems = orderData.OrderItems;
                currentOrder = orderData.Order;
                subtotal = currentOrder.Subtotal;
            }
        }

        protected async Task SelectCategory(int categoryId)
        {
            selectedCategoryId = categoryId;
            await LoadProductsByCategory(categoryId);
        }

        protected async Task LoadProductsByCategory(int categoryId)
        {
            products = (await ClientOrderService.GetMilkTeaProductsByCategoryAsync(categoryId)).ToList();
        }

        protected async Task SelectProduct(MilkTeaProduct product)
        {
            selectedProduct = product;
            selectedProductSizeId = 0;
            quantity = 1;
            selectedToppingIds.Clear();
            specialInstructions = string.Empty;

            // Load product sizes
            productSizes = (await ClientOrderService.GetProductSizesByProductIdAsync(product.ProductId)).ToList();

            // Load size details for each product size
            foreach (var productSize in productSizes)
            {
                productSize.Size = await ClientOrderService.GetSizeByIdAsync(productSize.SizeId);
            }
        }

        protected void CancelSelection()
        {
            selectedProduct = null;
            selectedProductSizeId = 0;
            productSizes.Clear();
            selectedToppingIds.Clear();
            specialInstructions = string.Empty;
        }

        protected void ToggleTopping(int toppingId, object isChecked)
        {
            if (isChecked is bool checkedValue)
            {
                if (checkedValue && !selectedToppingIds.Contains(toppingId))
                {
                    selectedToppingIds.Add(toppingId);
                }
                else if (!checkedValue && selectedToppingIds.Contains(toppingId))
                {
                    selectedToppingIds.Remove(toppingId);
                }
            }
        }

        protected bool IsToppingSelected(int toppingId)
        {
            return selectedToppingIds.Contains(toppingId);
        }

        protected async Task AddToOrder()
        {
            if (selectedProductSizeId == 0 || quantity < 1)
                return;

            var productSize = productSizes.FirstOrDefault(ps => ps.ProductSizeId == selectedProductSizeId);
            if (productSize == null)
                return;

            // Calculate the subtotal including toppings
            decimal itemSubtotal = await ClientOrderService.CalculateOrderItemSubtotalAsync(
                selectedProductSizeId,
                quantity,
                selectedToppingIds);

            // If calculation fails, use manual calculation
            if (itemSubtotal <= 0)
            {
                // Calculate base price
                itemSubtotal = quantity * productSize.Price;

                // Add topping prices
                foreach (var toppingId in selectedToppingIds)
                {
                    var topping = availableToppings.FirstOrDefault(t => t.ToppingId == toppingId);
                    if (topping != null)
                    {
                        itemSubtotal += quantity * topping.Price;
                    }
                }
            }

            // Create new order item
            var orderItem = new OrderItem
            {
                OrderId = 0, // Temporary, will be set when order is created
                ProductSizeId = selectedProductSizeId,
                ProductSize = productSize, // For display purposes
                Quantity = quantity,
                UnitPrice = productSize.Price,
                Subtotal = itemSubtotal,
                SpecialInstructions = string.IsNullOrWhiteSpace(specialInstructions) ? null : specialInstructions,
                OrderItemToppings = new List<OrderItemTopping>()
            };

            // Add toppings to the order item
            foreach (var toppingId in selectedToppingIds)
            {
                var topping = availableToppings.FirstOrDefault(t => t.ToppingId == toppingId);
                if (topping != null)
                {
                    orderItem.OrderItemToppings.Add(new OrderItemTopping
                    {
                        OrderItemId = 0, // Temporary, will be set when orderItem is created
                        ToppingId = topping.ToppingId,
                        Topping = topping, // For display purposes
                        Price = topping.Price
                    });
                }
            }

            orderItems.Add(orderItem);

            // Update subtotal
            subtotal += (decimal)(orderItem.Subtotal ?? 0);

            // Reset selection
            CancelSelection();
        }

        protected void ProceedToCheckout()
        {
            if (!orderItems.Any())
                return;

            // Update order with current subtotal
            currentOrder.Subtotal = subtotal;

            // Save order data to state service
            OrderStateService.SetOrderData(currentOrder, orderItems);

            // Navigate to checkout page
            NavigationManager.NavigateTo("/checkout");
        }
    }
}