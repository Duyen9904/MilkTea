using System;
using System.Collections.Generic;

namespace PRN222.Assignment.Repositories.Models;

public partial class OrderItem
{
    public int OrderItemId { get; set; }

    public int OrderId { get; set; }

    public int ProductSizeId { get; set; }

    public int Quantity { get; set; }

    public string? SpecialInstructions { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal? Subtotal { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual ICollection<OrderItemTopping> OrderItemToppings { get; set; } = new List<OrderItemTopping>();

    public virtual ProductSize ProductSize { get; set; } = null!;
}
