using System;
using System.Collections.Generic;

namespace PRN222.Assignment.Repositories.Entities;

public partial class Topping
{
    public int ToppingId { get; set; }

    public string ToppingName { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public string? ImagePath { get; set; }

    public bool IsAvailable { get; set; }

    public virtual ICollection<OrderItemTopping> OrderItemToppings { get; set; } = new List<OrderItemTopping>();
}
