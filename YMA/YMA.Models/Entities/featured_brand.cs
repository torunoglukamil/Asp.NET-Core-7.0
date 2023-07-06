using System;
using System.Collections.Generic;

namespace YMA.Entities.Entities;

public partial class featured_brand
{
    public int? brand_id { get; set; }

    public int? order_counter { get; set; }

    public int id { get; set; }

    public virtual brand? brand { get; set; }
}
