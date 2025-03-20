using System;
using System.Collections.Generic;

namespace PRN222.Assignment.Repositories.Models;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public int AccountId { get; set; }

    public decimal Amount { get; set; }

    public string TransactionType { get; set; } = null!;

    public string? Description { get; set; }

    public int? OrderId { get; set; }

    public DateTime TransactionDate { get; set; }

    public int? ProcessedBy { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual Order? Order { get; set; }

    public virtual Account? ProcessedByNavigation { get; set; }
}
