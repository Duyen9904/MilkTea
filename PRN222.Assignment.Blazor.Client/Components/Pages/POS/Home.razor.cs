using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PRN222.Assignment.Repositories.Entities;
using PRN222.Assignment.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace PRN222.Assignment.Blazor.Client.Components.Pages
{
    public class HomeBase : ComponentBase
    {
        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected IClientOrderService ClientOrderService { get; set; }

        [Inject]
        protected AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        protected string Message { get; set; } = "No message";
        protected bool ShowLoginPrompt { get; set; } = false;

        protected void DidNothing()
        {
            Message = "Edited";
            Console.WriteLine("Did nothing!");
            StateHasChanged();
        }

        protected async Task CheckAuthAndCreateOrder()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            bool isAuthenticated = user?.Identity?.IsAuthenticated ?? false;
            bool isStaff = user.IsInRole("STAFF");
            bool isAdmin = user.IsInRole("ADMIN");

            if (isAuthenticated && (isStaff || isAdmin))
            {
                await CreateNewOrder();
            }
            else
            {
                // Show login prompt
                ShowLoginPrompt = true;
            }
        }

        protected async Task CreateNewOrder()
        {
            Console.WriteLine("CreateNewOrder method called!");
            // Create a new order with initial values
            var newOrder = new Order
            {
                AccountId = 2, // Hard-coded account ID as specified
                OrderDate = DateTime.Now,
                Status = "Pending",
                PaymentStatus = "Pending",
                PaymentMethod = "Cash", // Default
                DeliveryAddress = "", // Will be filled later
                Subtotal = 0,
                Tax = 0,
                DeliveryFee = 0,
                TotalAmount = 0
            };
            Console.WriteLine($"Order created: AccountId = {newOrder.AccountId}, Status = {newOrder.Status}");
            // Navigate to the order page
            NavigationManager.NavigateTo("/index");
        }

        protected void CloseLoginPrompt()
        {
            ShowLoginPrompt = false;
        }

        protected void NavigateToLogin()
        {
            // Navigate directly to login page
            NavigationManager.NavigateTo("/login");
        }
    }
}