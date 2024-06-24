using System;
using System.Collections.Generic;

namespace KhumaloCraft.Models;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public string? Username { get; set; }

    public int ProductId { get; set; }

    public int Availability { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public string ProductName { get; set; } = null!;

    public int Price { get; set; }

    public virtual Product Product { get; set; } = null!;
}
