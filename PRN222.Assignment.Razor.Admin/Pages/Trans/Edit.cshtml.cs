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

namespace PRN222.Assignment.Razor.Admin.Pages.Trans
{
    public class EditModel : PageModel
    {
        private readonly PRN222.Assignment.Repositories.Entities.MilkTeaShopContext _context;
        private readonly ITransactionService _transactionService;

        public EditModel(PRN222.Assignment.Repositories.Entities.MilkTeaShopContext context, ITransactionService transactionService)
        {
            _context = context;
            _transactionService = transactionService;
        }

        [BindProperty]
        public Transaction Transaction { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _transactionService.GetByIdAsync(id.Value);
            if (transaction == null)
            {
                return NotFound();
            }
            Transaction = transaction;
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "DeliveryAddress");
            ViewData["ProcessedBy"] = new SelectList(_context.Accounts, "AccountId", "Address");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _transactionService.UpdateAsync(Transaction);
            }
            catch (DbUpdateConcurrencyException)
            {
                var exists = await _transactionService.GetByIdAsync(Transaction.TransactionId) != null;
                if (!exists)
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
    }
}