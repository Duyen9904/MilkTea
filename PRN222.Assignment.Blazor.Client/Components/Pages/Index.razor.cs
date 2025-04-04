using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PRN222.Assignment.Blazor.Client.Components.Services;
using PRN222.Assignment.Repositories.Entities;
using PRN222.Assignment.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PRN222.Assignment.Blazor.Client.Components.Pages
{
    public class IndexBase : ComponentBase
    {
        [Inject]
        protected IClientOrderService ClientOrderService { get; set; }

        [Inject] IComboService ComboService { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected OrderStateService OrderStateService { get; set; }

        [Inject]
        protected AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        protected List<Category> categories = new List<Category>();
        protected List<MilkTeaProduct> products = new List<MilkTeaProduct>();
        protected List<MilkTeaProduct> filteredProducts = new List<MilkTeaProduct>();
        protected List<ProductSize> productSizes = new List<ProductSize>();
        protected List<Topping> availableToppings = new List<Topping>();
        protected List<Topping> displayedToppings = new List<Topping>();
        protected List<Combo> availableCombos = new List<Combo>();
        protected List<int> selectedToppingIds = new List<int>();
        protected string specialInstructions = string.Empty;

        protected int selectedCategoryId = 0;
        protected bool showToppingsTab = false;
        protected bool showCombosTab = false;
        protected MilkTeaProduct selectedProduct;
        protected int selectedProductSizeId = 0;
        protected int quantity = 1;
        protected bool showPaymentModal = false;
        protected bool showSuccessModal = false;
        protected int confirmedOrderId = 0;
        protected string searchText = string.Empty;

        protected Order currentOrder;

        protected List<OrderItem> orderItems = new List<OrderItem>();

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            currentOrder = await InitializeOrder(user);
            // Load categories
            var allCategories = await ClientOrderService.GetAllCategoriesAsync();
            categories = allCategories.ToList();

            // Load available toppings
            availableToppings = (await ClientOrderService.GetAllAvailableToppingsAsync()).ToList();

            // Load available combos
            try
            {
                availableCombos = (await ClientOrderService.GetAllCombosAsync())
                    .Where(c => c.IsAvailable)
                    .ToList();
            }
            catch
            {
                // Handle if combos service is not implemented yet
                availableCombos = new List<Combo>();
            }

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

                // Make sure the account ID is still set to the current user
                var accountId = GetAccountIdFromUser(user);
                if (accountId > 0)
                {
                    currentOrder.AccountId = accountId;
                }

                await CalculateOrderTotals();
            }
        }

        private async Task<Order> InitializeOrder(ClaimsPrincipal user)
        {
            var accountId = GetAccountIdFromUser(user);

            return new Order
            {
                AccountId = accountId > 0 ? accountId : 1, // Fallback if user not found
                OrderDate = DateTime.Now,
                Status = "Pending",
                PaymentStatus = "Pending",
                PaymentMethod = "Cash",
                DeliveryAddress = "Table 01",
                Subtotal = 0,
                Tax = 0,
                DeliveryFee = 0, // Default delivery fee
                TotalAmount = 0
            };
        }

        private int GetAccountIdFromUser(ClaimsPrincipal user)
        {
            if (user.Identity.IsAuthenticated)
            {
                var accountIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
                if (accountIdClaim != null && int.TryParse(accountIdClaim.Value, out int accountId))
                {
                    return accountId;
                }
            }
            return 0;
        }
        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                Console.WriteLine("[DEBUG] Component first render complete");
            }
        }

        protected override void OnParametersSet()
        {
            Console.WriteLine("[DEBUG] OnParametersSet called");
            base.OnParametersSet();
        }


        protected async Task SelectCategory(int categoryId)
        {
            Console.WriteLine($"SelectCategory called with categoryId select category: {categoryId}");

            // Only reload products if category actually changed
            if (selectedCategoryId != categoryId)
            {
                selectedCategoryId = categoryId;
                showToppingsTab = false;
                showCombosTab = false;

                Console.WriteLine($"Loading products for category: {categoryId}");
                await Task.Delay(500);
                await LoadProductsByCategory(categoryId);
            }

            StateHasChanged(); // Ensure UI update
        }


        protected async Task ShowToppings()
        {
            selectedCategoryId = 0;
            showToppingsTab = true;
            showCombosTab = false;
            displayedToppings = availableToppings.Where(t => t.IsAvailable).ToList();
            filteredProducts = new List<MilkTeaProduct>();
            await Task.Delay(500);

            StateHasChanged(); // Explicitly trigger UI update
        }

        protected async Task ShowCombos()
        {
            selectedCategoryId = 0;
            showToppingsTab = false;
            showCombosTab = true;
            filteredProducts = new List<MilkTeaProduct>();
            await Task.Delay(500);

            StateHasChanged(); // Explicitly trigger UI update
        }

        protected async Task LoadProductsByCategory(int categoryId)
        {
            Console.WriteLine($"LoadProductsByCategory called for categoryId: {categoryId}");

            try
            {
                await Task.Delay(500);
                products = (await ClientOrderService.GetMilkTeaProductsByCategoryAsync(categoryId)).ToList();
                Console.WriteLine($"Received {products.Count} products from API");

                filteredProducts = products.Where(p => p.IsAvailable).ToList();
                Console.WriteLine($"Filtered to {filteredProducts.Count} available products");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading products: {ex.Message}");
                products = new List<MilkTeaProduct>();
                filteredProducts = new List<MilkTeaProduct>();
            }
        }
        protected async Task HandleCategoryClick(int categoryId)
        {
            Console.WriteLine($"[DEBUG] Tab clicked for category ID: {categoryId}");

            // Update state
            selectedCategoryId = categoryId;
            showToppingsTab = false;
            showCombosTab = false;

            // Force UI update
            StateHasChanged();

            // Explicitly call the service to get products
            try
            {
                var productsResult = await ClientOrderService.GetMilkTeaProductsByCategoryAsync(categoryId);
                products = productsResult.ToList();
                filteredProducts = products.Where(p => p.IsAvailable).ToList();

                Console.WriteLine($"[DEBUG] Retrieved {products.Count} products for category {categoryId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DEBUG] Error fetching products: {ex.Message}");
                products = new List<MilkTeaProduct>();
                filteredProducts = new List<MilkTeaProduct>();
            }

            // Update UI again after data load
            StateHasChanged();
        }

        protected void ApplySearch()
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                if (selectedCategoryId > 0)
                {
                    filteredProducts = products.Where(p => p.IsAvailable).ToList();
                }
                else if (showToppingsTab)
                {
                    displayedToppings = availableToppings.Where(t => t.IsAvailable).ToList();
                }
            }
            else
            {
                string searchLower = searchText.ToLower();

                if (selectedCategoryId > 0)
                {
                    filteredProducts = products
                        .Where(p => p.IsAvailable &&
                               (p.ProductName.ToLower().Contains(searchLower) ||
                                (p.Description != null && p.Description.ToLower().Contains(searchLower))))
                        .ToList();
                }
                else if (showToppingsTab)
                {
                    displayedToppings = availableToppings
                        .Where(t => t.IsAvailable &&
                               (t.ToppingName.ToLower().Contains(searchLower) ||
                                (t.Description != null && t.Description.ToLower().Contains(searchLower))))
                        .ToList();
                }
                else if (showCombosTab)
                {
                    availableCombos = availableCombos
                        .Where(c => c.IsAvailable &&
                               (c.ComboName.ToLower().Contains(searchLower) ||
                                (c.Description != null && c.Description.ToLower().Contains(searchLower))))
                        .ToList();
                }
            }
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

            if (productSizes.Any())
            {
                selectedProductSizeId = productSizes.First().ProductSizeId;
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

         protected async Task SelectTopping(Topping topping)
        {
            // Create a virtual order item for the topping
            var orderItem = new OrderItem
            {
                OrderId = 0,
                ProductSizeId = 0, // Not applicable for standalone topping
                Quantity = 1,
                UnitPrice = topping.Price,
                Subtotal = topping.Price,
                SpecialInstructions = null,
                OrderItemToppings = new List<OrderItemTopping>
                {
                    new OrderItemTopping
                    {
                        OrderItemId = 0,
                        ToppingId = topping.ToppingId,
                        Topping = topping,
                        Price = topping.Price
                    }
                }
            };
            
            // Add a custom ProductSize object for display purposes
            orderItem.ProductSize = new ProductSize
            {
                Product = new MilkTeaProduct
                {
                    ProductName = $"Topping: {topping.ToppingName}"
                },
                Size = new Size
                {
                    SizeName = "Regular"
                }
            };
            
            orderItems.Add(orderItem);
            await CalculateOrderTotals();
        }

        protected void SearchProducts()
        {
            ApplySearch();
        }

        protected async Task SelectCombo(Combo combo)
        {
            try
            {
                // Get all items in this combo
                var comboItems = await ClientOrderService.GetComboItemsByComboIdAsync(combo.ComboId);

                if (comboItems == null || !comboItems.Any())
                {
                    // Handle case where combo has no items
                    Console.WriteLine($"Warning: Combo {combo.ComboId} has no items");
                    return;
                }

                decimal calculatedComboPrice = 0;
                List<OrderItem> comboOrderItems = new List<OrderItem>();

                // Create order items for each product in the combo
                foreach (var comboItem in comboItems)
                {
                    // Load the product size details
                    var productSize = await ClientOrderService.GetProductSizeByIdAsync(comboItem.ProductSizeId);
                    if (productSize == null) continue;

                    // Load product details if not already loaded
                    if (productSize.Product == null)
                    {
                        productSize.Product = await ClientOrderService.GetMilkTeaProductByIdAsync(productSize.ProductId);
                    }

                    // Load size details if not already loaded
                    if (productSize.Size == null)
                    {
                        productSize.Size = await ClientOrderService.GetSizeByIdAsync(productSize.SizeId);
                    }

                    // Calculate the regular price for this item
                    decimal regularPrice = productSize.Price * comboItem.Quantity;
                    calculatedComboPrice += regularPrice;

                    // Create an order item for this combo item
                    var orderItem = new OrderItem
                    {
                        OrderId = 0, // Will be set when order is created
                        ProductSizeId = comboItem.ProductSizeId,
                        ProductSize = productSize,
                        Quantity = comboItem.Quantity,
                        UnitPrice = productSize.Price,
                        Subtotal = regularPrice,
                        SpecialInstructions = $"Part of Combo: {combo.ComboName}"
                    };

                    comboOrderItems.Add(orderItem);
                }

                // Calculate the discount amount
                decimal discount = calculatedComboPrice - combo.TotalPrice;

                if (comboOrderItems.Any())
                {
                    // Apply discount to the first item (or distribute it proportionally if needed)
                    if (discount > 0)
                    {
                        var firstItem = comboOrderItems.First();
                        firstItem.Subtotal -= discount;
                        firstItem.SpecialInstructions += $" (Combo Discount: {discount.ToString("C")})";
                    }

                    // Add all combo items to the order
                    foreach (var item in comboOrderItems)
                    {
                        orderItems.Add(item);
                    }

                    // Update order totals
                    await CalculateOrderTotals();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding combo to order: {ex.Message}");
            }
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
                UnitPrice = itemSubtotal / quantity,
                Subtotal = itemSubtotal,
                SpecialInstructions = string.IsNullOrWhiteSpace(specialInstructions) ? null : specialInstructions,
                OrderItemToppings = new List<OrderItemTopping>()
            };


            // Make sure product is loaded for display
            if (productSize.Product == null)
            {
                productSize.Product = selectedProduct;
            }

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

            // Update totals
            await CalculateOrderTotals();

            // Reset selection
            CancelSelection();
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
                
                currentOrder.DeliveryFee = 0;
                

                // Calculate total amount
                currentOrder.TotalAmount = currentOrder.Subtotal + currentOrder.Tax + currentOrder.DeliveryFee;
            }
        }

        protected void EditOrderItem(OrderItem item)
        {
            // Find and select the product
            var product = products.FirstOrDefault(p => p.ProductId == item.ProductSize.ProductId);
            if (product != null)
            {
                selectedProduct = product;
                selectedProductSizeId = item.ProductSizeId;
                quantity = item.Quantity;
                specialInstructions = item.SpecialInstructions ?? string.Empty;

                // Select toppings
                selectedToppingIds = item.OrderItemToppings.Select(t => t.ToppingId).ToList();

                // Remove the item from the order (it will be re-added)
                orderItems.Remove(item);

                // Load product sizes
                Task.Run(async () => {
                    productSizes = (await ClientOrderService.GetProductSizesByProductIdAsync(product.ProductId)).ToList();
                    foreach (var productSize in productSizes)
                    {
                        productSize.Size = await ClientOrderService.GetSizeByIdAsync(productSize.SizeId);
                    }
                    StateHasChanged();
                });
            }
        }

        protected async Task RemoveOrderItem(OrderItem item)
        {
            orderItems.Remove(item);
            await CalculateOrderTotals();
        }

        protected void CancelOrder()
        {
            orderItems.Clear();
            currentOrder = new Order
            {
                AccountId = 2,
                OrderDate = DateTime.Now,
                Status = "Pending",
                PaymentStatus = "Pending",
                PaymentMethod = "Cash",
                DeliveryAddress = "Room 01",
                Subtotal = 0,
                Tax = 0,
                DeliveryFee = 0,
                TotalAmount = 0
            };
        }

        protected void ProceedToPayment()
        {
            if (!orderItems.Any())
                return;

            showPaymentModal = true;
        }

        protected async Task PlaceOrder()
        {
            if (string.IsNullOrWhiteSpace(currentOrder.DeliveryAddress))
            {
                return;
            }
            try
            {
                // Set order date to current time
                currentOrder.OrderDate = DateTime.Now;

                // Ensure we have the current user's account ID
                var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
                var user = authState.User;
                var accountId = GetAccountIdFromUser(user);
                if (accountId > 0)
                {
                    currentOrder.AccountId = accountId;
                }

                // Create order in database
                var createdOrder = await ClientOrderService.CreateOrderAsync(currentOrder);

                if (createdOrder != null && createdOrder.OrderId > 0)
                {
                    // Create new order items with the order ID from createdOrder
                    var newOrderItems = orderItems.Select(item => new OrderItem
                    {
                        OrderId = createdOrder.OrderId,
                        ProductSizeId = item.ProductSizeId,
                        Quantity = item.Quantity,
                        SpecialInstructions = item.SpecialInstructions,
                        UnitPrice = item.UnitPrice
                    }).ToList();

                    // Create order items in database
                    var createdOrderItems = await ClientOrderService.CreateOrderItemsAsync(newOrderItems);

                    if (createdOrderItems != null && createdOrderItems.Any())
                    {
                        // Create toppings for each order item
                        var allOrderItemToppings = new List<OrderItemTopping>();

                        for (int i = 0; i < orderItems.Count; i++)
                        {
                            var originalItem = orderItems[i];
                            // Find corresponding created item
                            var createdItem = createdOrderItems.ElementAt(i);

                            if (createdItem != null && originalItem.OrderItemToppings.Any())
                            {
                                // Use a HashSet to track toppings and avoid duplicates
                                var addedToppingIds = new HashSet<int>();

                                foreach (var originalTopping in originalItem.OrderItemToppings)
                                {
                                    // Skip if we've already added this topping to this order item
                                    if (!addedToppingIds.Add(originalTopping.ToppingId))
                                    {
                                        continue; // Skip duplicate toppings
                                    }

                                    // Create new topping
                                    var newTopping = new OrderItemTopping
                                    {
                                        OrderItemId = createdItem.OrderItemId,
                                        ToppingId = originalTopping.ToppingId,
                                        Price = originalTopping.Price
                                    };

                                    allOrderItemToppings.Add(newTopping);
                                }
                            }
                        }

                        if (allOrderItemToppings.Any())
                        {
                            var result = await ClientOrderService.CreateOrderItemToppingsAsync(allOrderItemToppings);
                        }

                        // Order created successfully
                        confirmedOrderId = createdOrder.OrderId;
                        showPaymentModal = false;
                        showSuccessModal = true;

                        // Clear order state
                        OrderStateService.ClearOrderData();

                        // Reset the orderItems and prepare for a new order
                        orderItems = new List<OrderItem>();

                        // Initialize a new order with the current user
                        currentOrder = await InitializeOrder(user);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating order: {ex.Message}");
                // Could show error message here
            }
        }
        protected void CloseSuccessModal()
        {
            showSuccessModal = false;
            // Reset everything for a new order
            CancelOrder();
        }
    }
}