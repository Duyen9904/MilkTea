using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PRN222.Assignment.Repositories.Entities;
using PRN222.Assignment.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRN222.Assignment.Blazor.Client.Components.Pages
{
    public class TransactionsBase : ComponentBase
    {
        [Inject]
        protected AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        protected IOrderService OrderService { get; set; }

        [Inject]
        protected IClientOrderService ClientOrderService { get; set; }

        protected List<Order> orders = new List<Order>();
        protected Order selectedOrder = null;
        protected bool isLoading = true;
        protected bool showCompleted = false;
        protected DateTime startDate = DateTime.Now.AddDays(-7);
        protected DateTime endDate = DateTime.Now;

        private string currentUserId;
        protected override async Task OnInitializedAsync()
        {
            await LoadOrders();
        }

        protected async Task LoadOrders()
        {
            isLoading = true;
            try
            {
                // Get all orders
                var allOrders = await OrderService.GetAllOrders();

                // Filter orders based on criteria
                orders = allOrders.Where(o =>
                    o.OrderDate >= startDate &&
                    o.OrderDate <= endDate.AddDays(1) &&
                    (!showCompleted || o.Status == "Completed"))
                    .OrderByDescending(o => o.OrderDate)
                    .ToList();

                // Ensure account data is loaded
                foreach (var order in orders)
                {
                    if (order.Account == null)
                    {
                        // You'll need to implement a method to get account info
                        // This is just a placeholder - you should replace with actual logic
                        order.Account = new Account
                        {
                            AccountId = order.AccountId,
                            FirstName = "Customer",
                            LastName = $"#{order.AccountId}",
                            Email = $"customer{order.AccountId}@example.com",
                            Phone = "N/A"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading orders: {ex.Message}");
                // Could show error message here
            }
            finally
            {
                isLoading = false;
                StateHasChanged();
            }
        }

        protected async Task FilterOrders()
        {
            await LoadOrders();
        }

        protected async Task RefreshData()
        {
            await LoadOrders();
        }

        protected string GetOrderItemSummary(Order order)
        {
            if (order.OrderItems == null || !order.OrderItems.Any())
            {
                return "No items";
            }

            int totalItems = order.OrderItems.Sum(i => i.Quantity);
            int uniqueItems = order.OrderItems.Count;

            return $"{totalItems} item{(totalItems != 1 ? "s" : "")} ({uniqueItems} unique)";
        }

        protected string GetStatusBadgeClass(string status)
        {
            return status switch
            {
                "Pending" => "bg-warning text-dark",
                "Processing" => "bg-info text-dark",
                "Completed" => "bg-success",
                "Delivered" => "bg-primary",
                "Cancelled" => "bg-danger",
                _ => "bg-secondary"
            };
        }

        protected string GetPaymentStatusBadgeClass(string paymentStatus)
        {
            return paymentStatus switch
            {
                "Pending" => "bg-warning text-dark",
                "Completed" => "bg-success",
                "Failed" => "bg-danger",
                "Refunded" => "bg-info",
                _ => "bg-secondary"
            };
        }

        protected async Task ViewOrderDetails(Order order)
        {
            // Always reload the full order with all details to ensure we have all related entities
            var fullOrder = await ClientOrderService.GetOrderWithDetailsAsync(order.OrderId);

            if (fullOrder != null)
            {
                selectedOrder = fullOrder;
            }
        }

        protected void CloseOrderDetails()
        {
            selectedOrder = null;
        }

        protected async Task UpdateOrderStatus(int orderId, string newStatus)
        {
            try
            {
                // Update order status
                var result = await ClientOrderService.UpdateOrderStatusAsync(orderId, newStatus);

                if (result)
                {
                    // If the selected order is being displayed, update its status
                    if (selectedOrder != null && selectedOrder.OrderId == orderId)
                    {
                        selectedOrder.Status = newStatus;
                    }

                    // Find and update the order in the list
                    var order = orders.FirstOrDefault(o => o.OrderId == orderId);
                    if (order != null)
                    {
                        order.Status = newStatus;
                    }
                }

                // Refresh data if needed
                await RefreshData();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating order status: {ex.Message}");
                // Could show error message here
            }
        }

        protected async Task CancelOrder(int orderId)
        {
            await UpdateOrderStatus(orderId, "Cancelled");
        }
    }
}