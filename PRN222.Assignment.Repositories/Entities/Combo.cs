using System;
using System.Collections.Generic;

namespace PRN222.Assignment.Repositories.Entities;

public partial class Combo
{
    public int ComboId { get; set; }

    public string ComboName { get; set; } = null!;

    public string? Description { get; set; }

    public decimal TotalPrice { get; set; }

    public string? ImagePath { get; set; }

    public bool IsAvailable { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<ComboItem> ComboItems { get; set; } = new List<ComboItem>();

    public virtual ICollection<OrderCombo> OrderCombos { get; set; } = new List<OrderCombo>();
}
