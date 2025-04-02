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

        protected List<Category> categories = new List<Category>();
        protected List<MilkTeaProduct> products = new List<MilkTeaProduct>();
        protected List<ProductSize> productSizes = new List<ProductSize>();
        protected int selectedCategoryId = 0;
        protected MilkTeaProduct selectedProduct;
        protected int selectedProductSizeId = 0;
        protected int quantity = 1;

        // Order data
        protected PRN222.Assignment.Repositories.Entities.Order currentOrder = new PRN222.Assignment.Repositories.Entities.Order
        {
            AccountId = 2,
            OrderDate = DateTime.Now,
            Status = "Pending",
            PaymentStatus = "Pending",
            PaymentMethod = "Cash",
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

            if (categories.Any())
            {
                selectedCategoryId = categories.First().CategoryId;
                await LoadProductsByCategory(selectedCategoryId);
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
        }

        protected async Task AddToOrder()
        {
            if (selectedProductSizeId == 0 || quantity < 1)
                return;

            var productSize = productSizes.FirstOrDefault(ps => ps.ProductSizeId == selectedProductSizeId);
            if (productSize == null)
                return;

            // Create new order item (not persisted to database yet)
            var orderItem = new OrderItem
            {
                OrderId = 0, // Temporary, will be set when order is created
                ProductSizeId = selectedProductSizeId,
                ProductSize = productSize, // For display purposes
                Quantity = quantity,
                UnitPrice = productSize.Price,
                Subtotal = productSize.Price * quantity
            };

            orderItems.Add(orderItem);

            // Update subtotal
            subtotal += (decimal)(orderItem.Subtotal ?? 0);

            // Reset selection
            CancelSelection();
        }
    }
}