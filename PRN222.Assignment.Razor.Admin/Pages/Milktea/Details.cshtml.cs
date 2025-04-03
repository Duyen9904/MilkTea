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
    public class DetailsModel : PageModel
    {
        private readonly IMilkTeaProductService _milkTeaProductService;

        public DetailsModel(IMilkTeaProductService milkTeaProductService)
        {
            _milkTeaProductService = milkTeaProductService;
        }

        public MilkTeaProduct MilkTeaProduct { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Using the service to get the MilkTeaProduct by id
            MilkTeaProduct = _milkTeaProductService.GetById(id.Value);

            if (MilkTeaProduct == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
