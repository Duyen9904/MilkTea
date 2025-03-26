using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PRN222.Assignment.Repositories.Entities;
using PRN222.Assignment.Services.ComboOrders;

namespace PRN222.Assignment.Razor.Admin.Pages.ComboOrder
{
    public class EditModel : PageModel
    {
        private readonly IComboOrderService _comboOrderService;
        private readonly MilkTeaShopContext _context;

        public EditModel(IComboOrderService comboOrderService, MilkTeaShopContext context)
        {
            _comboOrderService = comboOrderService;
            _context = context;
        }

        [BindProperty]
        public OrderCombo OrderCombo { get; set; } = default!;

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OrderCombo = _comboOrderService.GetOrderComboById(id.Value);  // Use service to get OrderCombo by ID

            if (OrderCombo == null)
            {
                return NotFound();
            }

            ViewData["ComboId"] = new SelectList(_context.Combos, "ComboId", "ComboName");
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "DeliveryAddress");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _comboOrderService.UpdateOrderCombo(OrderCombo); 
            await Task.CompletedTask;
            return RedirectToPage("./Index");

        }
    }
}
