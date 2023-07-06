using System;
using System.Collections.Generic;

namespace YMA.Entities.Entities;

public partial class featured_category
{
    public int? category_id { get; set; }

    public int? order_counter { get; set; }

    public int id { get; set; }

    public virtual category? category { get; set; }
}
