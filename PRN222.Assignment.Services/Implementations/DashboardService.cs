using PRN222.Assignment.Repositories.Entities;
using PRN222.Assignment.Repositories.Repositories.Interface;
using PRN222.Assignment.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using PRN222.Assignment.Services.Dtos.Dashboard;

namespace PRN222.Assignment.Services.Implementations
{
    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DashboardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Sales Overview Metrics

        public async Task<int> GetTotalCustomersCountAsync(DateTime? fromDate = null, DateTime? toDate = null)
        {
            // Query to get distinct customers who have placed orders
            var query = await _unitOfWork.Orders.GetAllAsync(
                filter: o => (fromDate == null || o.OrderDate >= fromDate) &&
                             (toDate == null || o.OrderDate <= toDate)
            );

            return query.Select(o => o.AccountId).Distinct().Count();
        }

        public async Task<int> GetTotalOrdersCountAsync(DateTime? fromDate = null, DateTime? toDate = null)
        {
            var query = await _unitOfWork.Orders.GetAllAsync(
                filter: o => (fromDate == null || o.OrderDate >= fromDate) &&
                             (toDate == null || o.OrderDate <= toDate)
            );

            return query.Count();
        }

        public async Task<decimal> GetTotalRevenueAsync(DateTime? fromDate = null, DateTime? toDate = null)
        {
            var query = await _unitOfWork.Orders.GetAllAsync(
                filter: o => (fromDate == null || o.OrderDate >= fromDate) &&
                             (toDate == null || o.OrderDate <= toDate) &&
                             o.Status != "Cancelled"
            );

            return query.Sum(o => o.TotalAmount);
        }

        public async Task<decimal> GetAverageOrderValueAsync(DateTime? fromDate = null, DateTime? toDate = null)
        {
            var orders = await _unitOfWork.Orders.GetAllAsync(
                filter: o => (fromDate == null || o.OrderDate >= fromDate) &&
                             (toDate == null || o.OrderDate <= toDate) &&
                             o.Status != "Cancelled"
            );

            if (!orders.Any())
                return 0;

            return orders.Average(o => o.TotalAmount);
        }

        #endregion

        #region Product Analytics

        public async Task<List<ComboSalesDto>> GetTopCombosByQuantityAsync(int count = 5, DateTime? fromDate = null, DateTime? toDate = null)
        {
            // First get order IDs that match the date criteria
            var orderIds = new List<int>();
            if (fromDate != null || toDate != null)
            {
                var filteredOrders = await _unitOfWork.Orders.GetAllAsync(
                    filter: o => (fromDate == null || o.OrderDate >= fromDate) &&
                                 (toDate == null || o.OrderDate <= toDate) &&
                                 o.Status != "Cancelled"
                );
                orderIds = filteredOrders.Select(o => o.OrderId).ToList();
            }

            // Get all order combos (optionally filtered by order IDs)
            var orderCombos = await _unitOfWork.OrderCombos.GetAllAsync(
                filter: orderIds.Any() ? oc => orderIds.Contains(oc.OrderId) : null,
                includes: new Expression<Func<OrderCombo, object>>[] { oc => oc.Combo }
            );

            // Group and calculate total quantity and revenue
            var comboSales = orderCombos
                .GroupBy(oc => new { oc.ComboId, oc.Combo.ComboName })
                .Select(g => new ComboSalesDto
                {
                    ComboId = g.Key.ComboId,
                    ComboName = g.Key.ComboName,
                    QuantitySold = g.Sum(oc => oc.Quantity),
                    TotalRevenue = g.Sum(oc => oc.Quantity * oc.UnitPrice)
                })
                .OrderByDescending(c => c.QuantitySold)
                .Take(count)
                .ToList();

            return comboSales;
        }

        public async Task<List<ProductSalesDto>> GetTopProductsByQuantityAsync(int count = 5, DateTime? fromDate = null, DateTime? toDate = null)
        {
            // First get order IDs that match the date criteria
            var orderIds = new List<int>();
            if (fromDate != null || toDate != null)
            {
                var filteredOrders = await _unitOfWork.Orders.GetAllAsync(
                    filter: o => (fromDate == null || o.OrderDate >= fromDate) &&
                                 (toDate == null || o.OrderDate <= toDate) &&
                                 o.Status != "Cancelled"
                );
                orderIds = filteredOrders.Select(o => o.OrderId).ToList();
            }

            // Get all order items (optionally filtered by order IDs)
            var orderItems = await _unitOfWork.OrderItems.GetAllAsync(
                filter: orderIds.Any() ? oi => orderIds.Contains(oi.OrderId) : null,
                includes: new Expression<Func<OrderItem, object>>[] {
                oi => oi.ProductSize,
                oi => oi.ProductSize.Product
                }
            );

            // Group and calculate total quantity and revenue
            var productSales = orderItems
                .GroupBy(oi => new { oi.ProductSize.ProductId, oi.ProductSize.Product.ProductName })
                .Select(g => new ProductSalesDto
                {
                    ProductId = g.Key.ProductId,
                    ProductName = g.Key.ProductName,
                    QuantitySold = g.Sum(oi => oi.Quantity),
                    TotalRevenue = g.Sum(oi => oi.Subtotal ?? 0)
                })
                .OrderByDescending(p => p.QuantitySold)
                .Take(count)
                .ToList();

            return productSales;
        }

        public async Task<Dictionary<string, int>> GetProductSalesBySizeAsync(DateTime? fromDate = null, DateTime? toDate = null)
        {
            // First get order IDs that match the date criteria
            var orderIds = new List<int>();
            if (fromDate != null || toDate != null)
            {
                var filteredOrders = await _unitOfWork.Orders.GetAllAsync(
                    filter: o => (fromDate == null || o.OrderDate >= fromDate) &&
                                 (toDate == null || o.OrderDate <= toDate) &&
                                 o.Status != "Cancelled"
                );
                orderIds = filteredOrders.Select(o => o.OrderId).ToList();
            }

            // Get all order items (optionally filtered by order IDs)
            var orderItems = await _unitOfWork.OrderItems.GetAllAsync(
                filter: orderIds.Any() ? oi => orderIds.Contains(oi.OrderId) : null,
                includes: new Expression<Func<OrderItem, object>>[] {
                oi => oi.ProductSize,
                oi => oi.ProductSize.Size
                }
            );

            // Group by size and calculate total quantity
            var sizeQuantities = orderItems
                .GroupBy(oi => oi.ProductSize.Size.SizeName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Sum(oi => oi.Quantity)
                );

            return sizeQuantities;
        }

        #endregion

        #region Order Analytics

        public async Task<int> GetTotalCombosCountAsync(DateTime? fromDate = null, DateTime? toDate = null)
        {
            // First get order IDs that match the date criteria
            var orderIds = new List<int>();
            if (fromDate != null || toDate != null)
            {
                var filteredOrders = await _unitOfWork.Orders.GetAllAsync(
                    filter: o => (fromDate == null || o.OrderDate >= fromDate) &&
                                 (toDate == null || o.OrderDate <= toDate) &&
                                 o.Status != "Cancelled"
                );
                orderIds = filteredOrders.Select(o => o.OrderId).ToList();
            }

            // Get all order combos (optionally filtered by order IDs)
            var orderCombos = await _unitOfWork.OrderCombos.GetAllAsync(
                filter: orderIds.Any() ? oc => orderIds.Contains(oc.OrderId) : null
            );

            return orderCombos.Sum(oc => oc.Quantity);
        }

        public async Task<int> GetTotalMilkTeasCountAsync(DateTime? fromDate = null, DateTime? toDate = null)
        {
            // First get order IDs that match the date criteria
            var orderIds = new List<int>();
            if (fromDate != null || toDate != null)
            {
                var filteredOrders = await _unitOfWork.Orders.GetAllAsync(
                    filter: o => (fromDate == null || o.OrderDate >= fromDate) &&
                                 (toDate == null || o.OrderDate <= toDate) &&
                                 o.Status != "Cancelled"
                );
                orderIds = filteredOrders.Select(o => o.OrderId).ToList();
            }

            // Get all order items (optionally filtered by order IDs)
            var orderItems = await _unitOfWork.OrderItems.GetAllAsync(
                filter: orderIds.Any() ? oi => orderIds.Contains(oi.OrderId) : null
            );

            return orderItems.Sum(oi => oi.Quantity);
        }

        public async Task<decimal> GetAverageTransactionPerItemAsync(DateTime? fromDate = null, DateTime? toDate = null)
        {
            // Get orders within date range
            var orders = await _unitOfWork.Orders.GetAllAsync(
                filter: o => (fromDate == null || o.OrderDate >= fromDate) &&
                             (toDate == null || o.OrderDate <= toDate) &&
                             o.Status != "Cancelled"
            );

            if (!orders.Any())
                return 0;

            var orderIds = orders.Select(o => o.OrderId).ToList();

            // Get item counts
            var orderItems = await _unitOfWork.OrderItems.GetAllAsync(
                filter: oi => orderIds.Contains(oi.OrderId)
            );

            var orderCombos = await _unitOfWork.OrderCombos.GetAllAsync(
                filter: oc => orderIds.Contains(oc.OrderId)
            );

            int totalItemsCount = orderItems.Sum(oi => oi.Quantity) + orderCombos.Sum(oc => oc.Quantity);

            if (totalItemsCount == 0)
                return 0;

            decimal totalRevenue = orders.Sum(o => o.TotalAmount);

            return totalRevenue / totalItemsCount;
        }

        #endregion

        #region Time-based Analytics

        public async Task<Dictionary<string, decimal>> GetRevenueTrendAsync(string period = "month", int count = 6)
        {
            // Get all orders
            var allOrders = await _unitOfWork.Orders.GetAllAsync(
                filter: o => o.Status != "Cancelled"
            );

            var groupedData = new Dictionary<string, decimal>();
            DateTime now = DateTime.Now;

            if (period.ToLower() == "day")
            {
                // Group by day for the last 'count' days
                for (int i = 0; i < count; i++)
                {
                    DateTime targetDate = now.AddDays(-i);
                    string key = targetDate.ToString("MM/dd");

                    decimal revenue = allOrders
                        .Where(o => o.OrderDate.Date == targetDate.Date)
                        .Sum(o => o.TotalAmount);

                    groupedData.Add(key, revenue);
                }
            }
            else if (period.ToLower() == "month")
            {
                // Group by month for the last 'count' months
                for (int i = 0; i < count; i++)
                {
                    DateTime targetMonth = now.AddMonths(-i);
                    string key = targetMonth.ToString("MMM yyyy");

                    decimal revenue = allOrders
                        .Where(o => o.OrderDate.Month == targetMonth.Month &&
                                   o.OrderDate.Year == targetMonth.Year)
                        .Sum(o => o.TotalAmount);

                    groupedData.Add(key, revenue);
                }
            }
            else if (period.ToLower() == "year")
            {
                // Group by year for the last 'count' years
                for (int i = 0; i < count; i++)
                {
                    int targetYear = now.Year - i;
                    string key = targetYear.ToString();

                    decimal revenue = allOrders
                        .Where(o => o.OrderDate.Year == targetYear)
                        .Sum(o => o.TotalAmount);

                    groupedData.Add(key, revenue);
                }
            }

            return groupedData
                .OrderBy(kvp => kvp.Key)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        public async Task<Dictionary<string, int>> GetOrdersTrendAsync(string period = "month", int count = 6)
        {
            // Get all orders
            var allOrders = await _unitOfWork.Orders.GetAllAsync();

            var groupedData = new Dictionary<string, int>();
            DateTime now = DateTime.Now;

            if (period.ToLower() == "day")
            {
                // Group by day for the last 'count' days
                for (int i = 0; i < count; i++)
                {
                    DateTime targetDate = now.AddDays(-i);
                    string key = targetDate.ToString("MM/dd");

                    int orderCount = allOrders
                        .Count(o => o.OrderDate.Date == targetDate.Date);

                    groupedData.Add(key, orderCount);
                }
            }
            else if (period.ToLower() == "month")
            {
                // Group by month for the last 'count' months
                for (int i = 0; i < count; i++)
                {
                    DateTime targetMonth = now.AddMonths(-i);
                    string key = targetMonth.ToString("MMM yyyy");

                    int orderCount = allOrders
                        .Count(o => o.OrderDate.Month == targetMonth.Month &&
                                   o.OrderDate.Year == targetMonth.Year);

                    groupedData.Add(key, orderCount);
                }
            }
            else if (period.ToLower() == "year")
            {
                // Group by year for the last 'count' years
                for (int i = 0; i < count; i++)
                {
                    int targetYear = now.Year - i;
                    string key = targetYear.ToString();

                    int orderCount = allOrders
                        .Count(o => o.OrderDate.Year == targetYear);

                    groupedData.Add(key, orderCount);
                }
            }

            return groupedData
                .OrderBy(kvp => kvp.Key)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        #endregion
    }

}
