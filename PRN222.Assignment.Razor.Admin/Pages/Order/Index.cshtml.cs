// IndexModel.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN222.Assignment.Repositories.Entities;
using PRN222.Assignment.Services.Interfaces;

namespace PRN222.Assignment.Razor.Admin.Pages.Order
{
    public class IndexModel : PageModel
    {
        private readonly IOrderService _orderService;

        public IndexModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IList<PRN222.Assignment.Repositories.Entities.Order> Order { get; set; } = default!;

        // Search parameters
        [BindProperty(SupportsGet = true)]
        public string? SearchOrderDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchStatus { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchPaymentStatus { get; set; }

        // Pagination properties
        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 5;

        public int TotalPages { get; set; }

        public async Task OnGetAsync()
        {
            // Create filter expression
            Expression<Func<PRN222.Assignment.Repositories.Entities.Order, bool>> filter = o =>
                (string.IsNullOrEmpty(SearchStatus) || o.Status.Contains(SearchStatus)) &&
                (string.IsNullOrEmpty(SearchPaymentStatus) || o.PaymentStatus.Contains(SearchPaymentStatus));

            // Get total count for pagination
            var totalRecords = await _orderService.GetOrdersCount(filter);

            // Get orders with pagination
            Order = (await _orderService.GetAllOrdersPagination(
                filter: filter,
                orderBy: q => q.OrderByDescending(o => o.OrderDate),
                pageIndex: PageIndex,
                pageSize: PageSize
            )).ToList();

            // Calculate total pages
            TotalPages = (int)Math.Ceiling(totalRecords / (double)PageSize);
        }
    }
}