using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN222.Assignment.Repositories.Entities;
using PRN222.Assignment.Services.Interfaces;

namespace PRN222.Assignment.Razor.Admin.Components.Milktea
{
    public class IndexModel : PageModel
    {
        private readonly IMilkTeaProductService _milkTeaProductService;

        public IndexModel(IMilkTeaProductService milkTeaProductService)
        {
            _milkTeaProductService = milkTeaProductService;
        }

        public IList<MilkTeaProduct> MilkTeaProduct { get; set; } = default!;

        public async Task OnGetAsync()
        {
            // Fetching the milk tea products using the service
            MilkTeaProduct = _milkTeaProductService.GetMilkTeaProducts();
            await Task.CompletedTask;
        }
    }
}
