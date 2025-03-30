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
    public class IndexModel : PageModel
    {
        private readonly IComboOrderService _comboOrderService;

        public IndexModel(IComboOrderService comboOrderService)
        {
            _comboOrderService = comboOrderService;
        }

        public IList<OrderCombo> OrderCombo { get; set; } = default!;

        public async Task OnGetAsync()
        {
            OrderCombo = _comboOrderService.GetOrderCombos(); 
            await Task.CompletedTask;
        }
    }
}
