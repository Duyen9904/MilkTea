using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PRN222.Assignment.Repositories.Entities;
using PRN222.Assignment.Services.Interfaces;

namespace PRN222.Assignment.Razor.Admin.Pages.ComboOrder
{
    public class EditModel : PageModel
    {
        private readonly IOrderComboService _comboOrderService;
        private readonly MilkTeaShopContext _context;

        public EditModel(IOrderComboService comboOrderService, MilkTeaShopContext context)
        {
            _comboOrderService = comboOrderService;
            _context = context;
        }

        [BindProperty]
        public OrderCombo OrderCombo { get; set; } = default!;

        public async Task<IActionResult> OnGet(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OrderCombo = await _comboOrderService.GetOrderComboByIdAsync(id);  // Use service to get OrderCombo by ID

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

            await _comboOrderService.UpdateOrderComboAsync(OrderCombo); 
            await Task.CompletedTask;
            return RedirectToPage("./Index");

        }
    }
}
