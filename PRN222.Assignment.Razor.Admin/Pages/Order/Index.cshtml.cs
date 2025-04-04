using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN222.Assignment.Repositories.Entities;
using PRN222.Assignment.Services.Implementations;
using PRN222.Assignment.Services.Interfaces;

namespace PRN222.Assignment.Razor.Admin.Pages.Order
{
    public class IndexModel : PageModel
    {
        private readonly IOrderService _context;

        public IndexModel(IOrderService context)
        {
            _context = context;
        }

        public IList<PRN222.Assignment.Repositories.Entities.Order> Order { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Order = (await _context.GetAllOrders()).ToList();
        }
    }
}
