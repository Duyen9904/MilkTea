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

namespace PRN222.Assignment.Razor.Admin.Components.Milktea
{
    public class EditModel : PageModel
    {
        private readonly IMilkTeaProductService _milkTeaProductService;
        private readonly ICategoryService _categoryService;

        public EditModel(IMilkTeaProductService milkTeaProductService, ICategoryService categoryService)
        {
            _milkTeaProductService = milkTeaProductService;
            _categoryService = categoryService;
        }

        [BindProperty]
        public MilkTeaProduct MilkTeaProduct { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Use the service to get the MilkTeaProduct by id
            MilkTeaProduct = _milkTeaProductService.GetById(id.Value);

            if (MilkTeaProduct == null)
            {
                return NotFound();
            }

            // Use CategoryService to get the list of categories for the dropdown
            var categories = await _categoryService.GetCategoriesAsync();
            ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                 _milkTeaProductService.UpdateMilkTeaProduct(MilkTeaProduct);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MilkTeaProductExists(MilkTeaProduct.ProductId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MilkTeaProductExists(int id)
        {
            return _milkTeaProductService.GetById(id) != null;
        }
    }
}
