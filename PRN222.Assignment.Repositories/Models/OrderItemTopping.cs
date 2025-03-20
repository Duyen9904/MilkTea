using System;
using System.Collections.Generic;

namespace PRN222.Assignment.Repositories.Models;

public partial class OrderItemTopping
{
    public int OrderItemToppingId { get; set; }

    public int OrderItemId { get; set; }

    public int ToppingId { get; set; }

    public decimal Price { get; set; }

    public virtual OrderItem OrderItem { get; set; } = null!;

    public virtual Topping Topping { get; set; } = null!;
}
