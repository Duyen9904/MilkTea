using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PRN222.Assignment.Repositories.Entities;
using PRN222.Assignment.Services.Interfaces;

namespace PRN222.Assignment.Razor.Admin.Components.Milktea
{
    public class CreateModel : PageModel
    {
        private readonly IMilkTeaProductService _milkTeaProductService;
        private readonly ICategoryService _categoryService;

        public CreateModel(IMilkTeaProductService milkTeaProductService, ICategoryService categoryService)
        {
            _milkTeaProductService = milkTeaProductService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            // Use CategoryService to get the list of categories for the dropdown
            var categories = await _categoryService.GetCategoriesAsync();
            ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName");

            return Page();
        }

        [BindProperty]
        public MilkTeaProduct MilkTeaProduct { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Use the service to add the new product
             _milkTeaProductService.AddMilkTeaProduct(MilkTeaProduct);

            return RedirectToPage("./Index");
        }
    }
}
