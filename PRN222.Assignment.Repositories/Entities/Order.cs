using System;
using System.Collections.Generic;

namespace PRN222.Assignment.Repositories.Entities;

public partial class Order
{
    public int OrderId { get; set; }

    public int AccountId { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal Subtotal { get; set; }

    public decimal Tax { get; set; }

    public decimal DeliveryFee { get; set; }

    public decimal TotalAmount { get; set; }

    public string Status { get; set; } = null!;

    public string PaymentMethod { get; set; } = null!;

    public string PaymentStatus { get; set; } = null!;

    public string DeliveryAddress { get; set; } = null!;

    public string? Notes { get; set; }

    public int? ProcessedBy { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<OrderCombo> OrderCombos { get; set; } = new List<OrderCombo>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Account? ProcessedByNavigation { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
