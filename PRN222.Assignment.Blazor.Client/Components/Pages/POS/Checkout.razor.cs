using Microsoft.AspNetCore.Components;
using PRN222.Assignment.Blazor.Client.Components.Services;
using PRN222.Assignment.Repositories.Entities;
using PRN222.Assignment.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRN222.Assignment.Blazor.Client.Components.Pages
{
    public class CheckoutBase : ComponentBase
    {
        [Inject]
        protected IClientOrderService ClientOrderService { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected OrderStateService OrderStateService { get; set; }

        protected List<OrderItem> orderItems = new List<OrderItem>();
        protected PRN222.Assignment.Repositories.Entities.Order currentOrder = new PRN222.Assignment.Repositories.Entities.Order();
        protected bool showValidationErrors = false;
        protected bool isProcessing = false;
        protected bool showOrderSuccess = false;
        protected int confirmedOrderId = 0;

        protected override async Task OnInitializedAsync()
        {
            // Get order data from state service
            var orderData = OrderStateService.GetCurrentOrderData();
            if (orderData != null)
            {
                orderItems = orderData.OrderItems;
                currentOrder = orderData.Order;
            }
            else
            {
                // No order data available, initialize with defaults
                currentOrder = new PRN222.Assignment.Repositories.Entities.Order
                {
                    AccountId = 2, // Assuming a default user ID
                    OrderDate = DateTime.Now,
                    Status = "Pending",
                    PaymentStatus = "Pending",
                    PaymentMethod = "Cash", // Valid value from check constraint
                    DeliveryAddress = "",
                    Subtotal = 0,
                    Tax = 0,
                    DeliveryFee = 15000, // Default delivery fee (15,000 VND)
                    TotalAmount = 0
                };
            }

            // Calculate totals
            await CalculateOrderTotals();
        }

        protected async Task CalculateOrderTotals()
        {
            if (orderItems.Any())
            {
                // Calculate subtotal
                currentOrder.Subtotal = orderItems.Sum(item => item.Subtotal ?? 0);

                // Calculate tax (10%)
                currentOrder.Tax = currentOrder.Subtotal * 0.1m;

                // Delivery fee is set to default (15,000 VND) or could be calculated based on distance
                if (currentOrder.DeliveryFee <= 0)
                {
                    currentOrder.DeliveryFee = 15000;
                }

                // Calculate total amount
                currentOrder.TotalAmount = currentOrder.Subtotal + currentOrder.Tax + currentOrder.DeliveryFee;
            }
        }

        protected async Task PlaceOrder()
        {
            showValidationErrors = true;

            // Validate required fields
            if (string.IsNullOrWhiteSpace(currentOrder.DeliveryAddress))
            {
                return;
            }

            isProcessing = true;

            try
            {
                // Set order date to current time
                currentOrder.OrderDate = DateTime.Now;

                // Create order in database
                var createdOrder = await ClientOrderService.CreateOrderAsync(currentOrder);

                if (createdOrder != null && createdOrder.OrderId > 0)
                {
                    // Update order items with the new order ID
                    foreach (var item in orderItems)
                    {
                        item.OrderId = createdOrder.OrderId;
                    }

                    // Create order items in database
                    var createdOrderItems = await ClientOrderService.CreateOrderItemsAsync(orderItems);

                    if (createdOrderItems != null && createdOrderItems.Any())
                    {
                        // Create toppings for each order item
                        var allOrderItemToppings = new List<OrderItemTopping>();

                        foreach (var orderItem in orderItems)
                        {
                            var createdItem = createdOrderItems.FirstOrDefault(i =>
                                i.ProductSizeId == orderItem.ProductSizeId &&
                                i.OrderId == createdOrder.OrderId);

                            if (createdItem != null && orderItem.OrderItemToppings.Any())
                            {
                                foreach (var topping in orderItem.OrderItemToppings)
                                {
                                    topping.OrderItemId = createdItem.OrderItemId;
                                    allOrderItemToppings.Add(topping);
                                }
                            }
                        }

                        if (allOrderItemToppings.Any())
                        {
                            await ClientOrderService.CreateOrderItemToppingsAsync(allOrderItemToppings);
                        }

                        // Order created successfully
                        confirmedOrderId = createdOrder.OrderId;
                        showOrderSuccess = true;

                        // Clear order state
                        OrderStateService.ClearOrderData();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle error (could show error message to user)
                Console.WriteLine($"Error creating order: {ex.Message}");
            }
            finally
            {
                isProcessing = false;
                StateHasChanged();
            }
        }

        protected void CloseSuccessModal()
        {
            showOrderSuccess = false;
        }

        protected void GoToHomePage()
        {
            NavigationManager.NavigateTo("/");
        }
    }
}