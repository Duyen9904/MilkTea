using System;
using System.Collections.Generic;

namespace PRN222.Assignment.Repositories.Entities;

public partial class Size
{
    public int SizeId { get; set; }

    public string SizeName { get; set; } = null!;

    public decimal PriceModifier { get; set; }

    public virtual ICollection<ProductSize> ProductSizes { get; set; } = new List<ProductSize>();
}
