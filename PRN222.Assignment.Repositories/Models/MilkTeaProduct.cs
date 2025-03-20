using System;
using System.Collections.Generic;

namespace PRN222.Assignment.Repositories.Models;

public partial class MilkTeaProduct
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public int CategoryId { get; set; }

    public string? Description { get; set; }

    public decimal BasePrice { get; set; }

    public string? ImagePath { get; set; }

    public bool IsAvailable { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime LastModified { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<ProductSize> ProductSizes { get; set; } = new List<ProductSize>();
}
