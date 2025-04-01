using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN222.Assignment.Repositories.Entities;
using PRN222.Assignment.Services.Interfaces;


namespace PRN222.Assignment.Razor.Admin.Pages.ComboOrder
{
    public class DeleteModel : PageModel
    {
        private readonly IOrderComboService _comboOrderService;

        public DeleteModel(IOrderComboService comboOrderService)
        {
            _comboOrderService = comboOrderService;
        }

        [BindProperty]
        public OrderCombo OrderCombo { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OrderCombo = await _comboOrderService.GetOrderComboByIdAsync(id); 

            if (OrderCombo == null)
            {
                return NotFound();
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

            OrderCombo = await _comboOrderService.GetOrderComboByIdAsync(id);

            if (OrderCombo != null)
            {
                await _comboOrderService.CreateOrderComboAsync(OrderCombo);  // Use service to delete the OrderCombo
            }
            await Task.CompletedTask;
            return RedirectToPage("./Index");
        }
    }
}
