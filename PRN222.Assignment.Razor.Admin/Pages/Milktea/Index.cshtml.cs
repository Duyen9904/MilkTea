using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        // Property to hold the list of milk tea products
        public IList<MilkTeaProduct> MilkTeaProduct { get; set; } = default!;

        // Asynchronous method to fetch milk tea products
        public async Task OnGetAsync()
        {
            // Fetching the milk tea products using the service asynchronously
            MilkTeaProduct = (await _milkTeaProductService.GetMilkTeaProductsWithCategoryAsync()).ToList();
        }
    }
}
