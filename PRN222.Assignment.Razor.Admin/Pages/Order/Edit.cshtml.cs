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

namespace PRN222.Assignment.Razor.Admin.Pages.Order
{
    public class EditModel : PageModel
    {
        private readonly IOrderService  _context;
        private readonly MilkTeaShopContext _milkTeaShopContext;

        public EditModel(IOrderService context, MilkTeaShopContext milkTeaShopContext)
        {
            _milkTeaShopContext = milkTeaShopContext;
            _context = context;
        }

        [BindProperty]
        public PRN222.Assignment.Repositories.Entities.Order Order { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order =  await _context.GetOrderByIdAsync(id.Value);
            if (order == null)
            {
                return NotFound();
            }
            Order = order;
           ViewData["AccountId"] = new SelectList(_milkTeaShopContext.Accounts, "AccountId", "Address");
           ViewData["ProcessedBy"] = new SelectList(_milkTeaShopContext.Accounts, "AccountId", "Address");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _context.UpdateOrderAsync(Order);
            await Task.CompletedTask;
            return RedirectToPage("./Index");

        }
    }
}
