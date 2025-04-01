using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRN222.Assignment.Services.Dtos.Dashboard;

namespace PRN222.Assignment.Services.Interfaces
{
    public interface IDashboardService
    {
        // Sales overview metrics
        Task<int> GetTotalCustomersCountAsync(DateTime? fromDate = null, DateTime? toDate = null);
        Task<int> GetTotalOrdersCountAsync(DateTime? fromDate = null, DateTime? toDate = null);
        Task<decimal> GetTotalRevenueAsync(DateTime? fromDate = null, DateTime? toDate = null);
        Task<decimal> GetAverageOrderValueAsync(DateTime? fromDate = null, DateTime? toDate = null);

        // Product analytics
        Task<List<ComboSalesDto>> GetTopCombosByQuantityAsync(int count = 5, DateTime? fromDate = null, DateTime? toDate = null);
        Task<List<ProductSalesDto>> GetTopProductsByQuantityAsync(int count = 5, DateTime? fromDate = null, DateTime? toDate = null);
        Task<Dictionary<string, int>> GetProductSalesBySizeAsync(DateTime? fromDate = null, DateTime? toDate = null);

        // Order analytics
        Task<int> GetTotalCombosCountAsync(DateTime? fromDate = null, DateTime? toDate = null);
        Task<int> GetTotalMilkTeasCountAsync(DateTime? fromDate = null, DateTime? toDate = null);
        Task<decimal> GetAverageTransactionPerItemAsync(DateTime? fromDate = null, DateTime? toDate = null);

        // Time-based analytics
        Task<Dictionary<string, decimal>> GetRevenueTrendAsync(string period = "month", int count = 6);
        Task<Dictionary<string, int>> GetOrdersTrendAsync(string period = "month", int count = 6);
    }

}
