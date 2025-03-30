using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN222.Assignment.Repositories.Entities;
using PRN222.Assignment.Services.ComboOrders;

namespace PRN222.Assignment.Razor.Admin.Pages.ComboOrder
{
    public class DeleteModel : PageModel
    {
        private readonly IComboOrderService _comboOrderService;

        public DeleteModel(IComboOrderService comboOrderService)
        {
            _comboOrderService = comboOrderService;
        }

        [BindProperty]
        public OrderCombo OrderCombo { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OrderCombo = _comboOrderService.GetOrderComboById(id.Value);  // Use service to get the OrderCombo by ID

            if (OrderCombo == null)
            {
                return NotFound();
            }
            await Task.CompletedTask;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OrderCombo = _comboOrderService.GetOrderComboById(id.Value);

            if (OrderCombo != null)
            {
                _comboOrderService.DeleteOrderCombo(OrderCombo);  // Use service to delete the OrderCombo
            }
            await Task.CompletedTask;
            return RedirectToPage("./Index");
        }
    }
}
