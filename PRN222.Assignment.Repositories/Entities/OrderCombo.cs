using System;
using System.Collections.Generic;

namespace PRN222.Assignment.Repositories.Entities;

public partial class OrderCombo
{
    public int OrderComboId { get; set; }

    public int OrderId { get; set; }

    public int ComboId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public virtual Combo Combo { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
