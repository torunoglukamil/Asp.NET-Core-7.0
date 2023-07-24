using System;
using System.Collections.Generic;

namespace YMA.Entities.Entities;

public partial class favorite_product
{
    public string id { get; set; } = null!;

    public string? product_id { get; set; }

    public string? account_id { get; set; }

    public DateTime? create_date { get; set; }
}
