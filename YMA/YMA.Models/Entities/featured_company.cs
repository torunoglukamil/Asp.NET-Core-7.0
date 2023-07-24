using System;
using System.Collections.Generic;

namespace YMA.Entities.Entities;

public partial class featured_company
{
    public string id { get; set; } = null!;

    public string? company_id { get; set; }

    public int? order_counter { get; set; }
}
