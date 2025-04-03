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
    public class IndexModel : PageModel
    {
        private readonly IOrderComboService _comboOrderService;

        public IndexModel(IOrderComboService comboOrderService)
        {
            _comboOrderService = comboOrderService;
        }

        // Change the type of OrderCombo to List<OrderCombo> instead of IList<OrderCombo>
        public List<OrderCombo> OrderCombo { get; set; } = default!;

        // OnGetAsync is the method that will run when the page is accessed
        public async Task OnGetAsync()
        {
            // Fetch all OrderCombos asynchronously and assign them to the OrderCombo property
            OrderCombo = await _comboOrderService.GetAllOrderCombosAsync();
        }
    }
}
