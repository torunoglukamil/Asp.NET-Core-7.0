using System;
using System.Collections.Generic;

namespace YMA.Entities.Entities;

public partial class featured_brand
{
    public string id { get; set; } = null!;

    public string? brand_id { get; set; }

    public int? order_counter { get; set; }
}
