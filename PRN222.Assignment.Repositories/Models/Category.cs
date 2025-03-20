using System;
using System.Collections.Generic;

namespace PRN222.Assignment.Repositories.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<MilkTeaProduct> MilkTeaProducts { get; set; } = new List<MilkTeaProduct>();
}
