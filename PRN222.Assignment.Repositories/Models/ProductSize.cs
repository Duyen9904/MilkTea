using System;
using System.Collections.Generic;

namespace PRN222.Assignment.Repositories.Models;

public partial class ProductSize
{
    public int ProductSizeId { get; set; }

    public int ProductId { get; set; }

    public int SizeId { get; set; }

    public decimal Price { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual MilkTeaProduct Product { get; set; } = null!;

    public virtual Size Size { get; set; } = null!;
}
