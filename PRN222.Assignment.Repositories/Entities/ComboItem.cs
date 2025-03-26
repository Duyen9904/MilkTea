using System;
using System.Collections.Generic;

namespace PRN222.Assignment.Repositories.Entities;

public partial class ComboItem
{
    public int ComboItemId { get; set; }

    public int ComboId { get; set; }

    public int ProductSizeId { get; set; }

    public int Quantity { get; set; }

    public virtual Combo Combo { get; set; } = null!;

    public virtual ProductSize ProductSize { get; set; } = null!;
}
