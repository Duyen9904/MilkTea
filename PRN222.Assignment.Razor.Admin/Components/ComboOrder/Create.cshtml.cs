using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PRN222.Assignment.Repositories.Entities;
using PRN222.Assignment.Services.ComboOrders;

namespace PRN222.Assignment.Razor.Admin.Pages.ComboOrder
{
    public class CreateModel : PageModel
    {
        private readonly IComboOrderService _comboOrderService;
        private readonly MilkTeaShopContext _context;

        public CreateModel(IComboOrderService comboOrderService, MilkTeaShopContext context)
        {
            _comboOrderService = comboOrderService;
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["ComboId"] = new SelectList(_context.Combos, "ComboId", "ComboName");
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "DeliveryAddress");
            return Page();
        }

        [BindProperty]
        public OrderCombo OrderCombo { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _comboOrderService.AddOrderCombo(OrderCombo);
            await Task.CompletedTask;
            return RedirectToPage("./Index");
        }
    }

}