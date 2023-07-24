using System;
using System.Collections.Generic;

namespace YMA.Entities.Entities;

public partial class exchange
{
    public string id { get; set; } = null!;

    public string? currency { get; set; }

    public double? unit_price { get; set; }

    public double? exchange_ratio { get; set; }

    public double? exchange_amount { get; set; }

    public bool? is_increased { get; set; }

    public int? order_number { get; set; }

    public bool? is_disabled { get; set; }
}
