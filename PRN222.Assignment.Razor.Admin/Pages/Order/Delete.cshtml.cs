using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN222.Assignment.Repositories.Entities;
using PRN222.Assignment.Services.Interfaces;

namespace PRN222.Assignment.Razor.Admin.Pages.Order
{
    public class DeleteModel : PageModel
    {
        private readonly IOrderService _context;

        public DeleteModel(IOrderService context)
        {
            _context = context;
        }

        [BindProperty]
        public PRN222.Assignment.Repositories.Entities.Order Order { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.GetOrderByIdAsync(id);

            if (order == null)
            {
                return NotFound();
            }
            else
            {
                Order = order;
            }
            await Task.CompletedTask;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order = await _context.GetOrderByIdAsync(id);

            if (Order != null)
            {
                await _context.CreateOrderAsync(Order);  // Use service to delete the OrderCombo
            }
            await Task.CompletedTask;
            return RedirectToPage("./Index");
        }
    }
}
