using System;
using System.Collections.Generic;

namespace YMA.Entities.Entities;

public partial class featured_category
{
    public string id { get; set; } = null!;

    public string? category_id { get; set; }

    public int? order_counter { get; set; }
}
