using System;
using System.Collections.Generic;

namespace YMA.Entities.Entities;

public partial class featured_product
{
    public int id { get; set; }

    public int? product_id { get; set; }

    public int? order_counter { get; set; }

    public virtual product? product { get; set; }
}
