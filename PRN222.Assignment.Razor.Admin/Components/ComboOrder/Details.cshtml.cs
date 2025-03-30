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
    public class DetailsModel : PageModel
    {
        private readonly IComboOrderService _comboOrderService;

        public DetailsModel(IComboOrderService comboOrderService)
        {
            _comboOrderService = comboOrderService;
        }

        public OrderCombo OrderCombo { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
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
            await Task.CompletedTask;
            return Page();
        }
    }
}
