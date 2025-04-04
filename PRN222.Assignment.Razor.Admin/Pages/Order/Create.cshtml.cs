using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PRN222.Assignment.Repositories.Entities;
using PRN222.Assignment.Services.Interfaces;

namespace PRN222.Assignment.Razor.Admin.Pages.Order
{
    public class CreateModel : PageModel
    {
        private readonly MilkTeaShopContext _context;
        private readonly IOrderService _orderService;

        public CreateModel(MilkTeaShopContext context, IOrderService orderService)
        {
            _context = context;
            _orderService = orderService;
        }

        public IActionResult OnGet()
        {
        ViewData["AccountId"] = new SelectList(_context.Accounts, "AccountId", "Address");
        ViewData["ProcessedBy"] = new SelectList(_context.Accounts, "AccountId", "Address");
            return Page();
        }

        [BindProperty]
        public PRN222.Assignment.Repositories.Entities.Order Order { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _orderService.CreateOrderAsync(Order);
            await Task.CompletedTask;
            return RedirectToPage("./Index");
        }
    }
}
