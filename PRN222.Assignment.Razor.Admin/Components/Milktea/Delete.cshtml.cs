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
    public class DeleteModel : PageModel
    {
        private readonly IMilkTeaProductService _milkTeaProductService;

        public DeleteModel(IMilkTeaProductService milkTeaProductService)
        {
            _milkTeaProductService = milkTeaProductService;
        }

        [BindProperty]
        public MilkTeaProduct MilkTeaProduct { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Using service to get the milk tea product by id
            MilkTeaProduct = _milkTeaProductService.GetById(id.Value);

            if (MilkTeaProduct == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Using service to get the milk tea product by id and delete
            MilkTeaProduct = _milkTeaProductService.GetById(id.Value);

            if (MilkTeaProduct != null)
            {
                _milkTeaProductService.DeleteMilkTeaProduct(MilkTeaProduct);
            }

            return RedirectToPage("./Index");
        }
    }
}
