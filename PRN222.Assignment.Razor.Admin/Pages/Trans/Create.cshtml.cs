using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PRN222.Assignment.Repositories.Entities;
using PRN222.Assignment.Services.Interfaces;

namespace PRN222.Assignment.Razor.Admin.Pages.Trans
{
    public class CreateModel : PageModel
    {
        private readonly PRN222.Assignment.Repositories.Entities.MilkTeaShopContext _context;
        private readonly ITransactionService _transactionService;

        public CreateModel(PRN222.Assignment.Repositories.Entities.MilkTeaShopContext context, ITransactionService transactionService)
        {
            _context = context;
            _transactionService = transactionService;
        }

        public IActionResult OnGet()
        {
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "DeliveryAddress");
            ViewData["ProcessedBy"] = new SelectList(_context.Accounts, "AccountId", "Address");
            return Page();
        }

        [BindProperty]
        public Transaction Transaction { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _transactionService.AddAsync(Transaction);

            return RedirectToPage("./Index");
        }
    }
}
