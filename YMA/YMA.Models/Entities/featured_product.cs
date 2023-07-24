using System;
using System.Collections.Generic;

namespace YMA.Entities.Entities;

public partial class featured_product
{
    public string id { get; set; } = null!;

    public string? product_id { get; set; }

    public int? order_counter { get; set; }
}
