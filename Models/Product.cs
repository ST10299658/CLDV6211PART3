using System;
using System.Collections.Generic;

namespace KhumaloCraft.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public int Price { get; set; }

    public string Category { get; set; } = null!;

    public int Availability { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
